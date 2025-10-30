using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PRN232.Lab2.CoffeeStore.Repositories.Entities;
using PRN232.Lab2.CoffeeStore.Repositories.UnitOfWork;
using PRN232.Lab2.CoffeeStore.Services.CacheService;
using PRN232.Lab2.CoffeeStore.Services.Configuration;
using PRN232.Lab2.CoffeeStore.Services.Constants;
using PRN232.Lab2.CoffeeStore.Services.Exceptions;
using PRN232.Lab2.CoffeeStore.Services.Helper;
using PRN232.Lab2.CoffeeStore.Services.Mapper;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PRN232.Lab2.CoffeeStore.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtService _jwtService;
        private readonly JwtSettings _jwtSettings;
        private readonly IRedisService _redisService;

        public AuthService(IUnitOfWork unitOfWork, IPasswordHasher<User> passwordHasher, IJwtService jwtService, IOptions<JwtSettings> jwtSettings, IRedisService redisService)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _jwtSettings = jwtSettings.Value;
            _redisService = redisService;
        }

        public async Task<TokenResponse> LoginAsync(LoginRequest request)
        {
            // Validate and determine login type
            //check if input is email or phone number 

            var emailAttribute = new EmailAddressAttribute();
            var isEmail = emailAttribute.IsValid(request.KeyLogin);
            var isPhone = Regex.IsMatch(request.KeyLogin, IRegexStorage.PHONE_PATTERN);
            if (!isEmail && !isPhone)
            {
                throw new InvalidCredentialsException("Vui lòng nhập đúng định dạng email hoặc số điện thoại");
            }
            // Find user by email or phone
            User? user = null;
            if (isEmail)
            {
                user = await _unitOfWork.Users.FindOneAsync(u => u.Email == request.KeyLogin);
            }
            else if (isPhone)
            {
                var normalizedPhone = PhoneNumberHelper.NormalizePhoneNumber(request.KeyLogin);
                user = await _unitOfWork.Users.FindOneAsync(u => u.PhoneNumber == normalizedPhone);
            }
            // Check if user is active
            if (user == null)
                throw new NotFoundException("Tài khoản không tồn tại");
            if (!user.IsActive)
            {
                throw new InvalidCredentialsException("Tài khoản đã bị vô hiệu hóa");
            }

            // Verify password
            var verifyResult = _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);
            if (verifyResult == PasswordVerificationResult.Failed)
            {
                throw new InvalidCredentialsException("Mật khẩu không chính xác");
            }

            // Generate tokens
            var accessToken = _jwtService.GenerateAccessToken(user);
            var refreshToken = await _jwtService.GenerateRefreshTokenAsync(user.Id);
            // Save refresh token
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);
            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();

            return new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiry = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
                RefreshTokenExpiry = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays)
            };
        }



        public async Task RegisterAsync(RegisterRequest request)
        {
            // ✅ Check duplicates BEFORE inserting
            await ValidateUserUniquenessAsync(request);

            //format phone number
            if (!string.IsNullOrEmpty(request.PhoneNumber))
            {
                request.PhoneNumber = PhoneNumberHelper.NormalizePhoneNumber(request.PhoneNumber);
            }

            // Create and save user
            var userEntity = request.ToUserEntity();
            //convert string role to enum
            userEntity.Role = request.Role;
            userEntity.Password = _passwordHasher.HashPassword(userEntity, request.Password);
            userEntity.IsActive = true;
            await _unitOfWork.Users.AddAsync(userEntity);
            await _unitOfWork.SaveChangesAsync();
        }


        public Task<TokenResponse> RefreshTokenAsync(string refreshToken)
        {
            return _jwtService.RefreshTokenAsync(refreshToken);
        }

        public async Task LogoutAsync(string userId, string accessToken, TimeSpan accessTokenTtl)
        {
            // 1. Revoke refresh token in DB
            var user = await _unitOfWork.Users.GetByIdAsync(Guid.Parse(userId));
            if (user != null)
            {
                user.RefreshToken = null;
                user.IsRefreshTokenRevoked = true;
                _unitOfWork.Users.Update(user);
                await _unitOfWork.SaveChangesAsync();
            }

            // 2. Blacklist access token in Redis
            // Key: "blacklist:{accessToken}", Value: "revoked", Expiry: accessTokenTtl
            await _redisService.SetAsync($"blacklist:{accessToken}", "revoked", accessTokenTtl);
        }
        private async Task ValidateUserUniquenessAsync(RegisterRequest request)
        {
            // Check email
            var existingUserByEmail = await _unitOfWork.Users.FindOneAsync(u => u.Email == request.Email);
            if (existingUserByEmail != null)
            {
                throw new InvalidCredentialsException("Email đã tồn tại");
            }
            // Check phone number
            if (!string.IsNullOrEmpty(request.PhoneNumber))
            {
                var existingUserByPhone = await _unitOfWork.Users.FindOneAsync(u => u.PhoneNumber == request.PhoneNumber);
                if (existingUserByPhone != null)
                {
                    throw new InvalidCredentialsException("Số điện thoại đã tồn tại");
                }
            }
        }


    }
}
