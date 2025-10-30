using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN232.Lab2.CoffeeStore.API.Models;
using PRN232.Lab2.CoffeeStore.Services.AuthService;
using PRN232.Lab2.CoffeeStore.Services.Configuration;
using System.Security.Claims;

namespace PRN232.Lab2.CoffeeStore.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse>> Register([FromBody] RegisterRequest request)
        {
            await _authService.RegisterAsync(request);
            return Created("Đăng ký thành công");

        }
        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<TokenResponse>>> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);
            return Ok(result, "Đăng nhập thành công");

        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<ApiResponse<TokenResponse>>> RefreshToken([FromBody] string refreshToken)
        {
            var result = await _authService.RefreshTokenAsync(refreshToken);
            return Ok(result, "Làm mới token thành công");
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<ActionResult<ApiResponse>> Logout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Lấy access token từ header
            var accessToken = Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");
            if (string.IsNullOrEmpty(accessToken))
                return Unauthorized("Không tìm thấy access token");

            // Giải mã access token để lấy thời gian hết hạn
            var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(accessToken);
            var expUnix = jwtToken.Payload.Exp;
            if (expUnix == null)
                return BadRequest("Access token không hợp lệ");

            var expiry = DateTimeOffset.FromUnixTimeSeconds((long)expUnix);
            var ttl = expiry - DateTimeOffset.UtcNow;
            if (ttl <= TimeSpan.Zero)
                return BadRequest("Access token đã hết hạn");

            // Gọi service để thu hồi refresh token và blacklist access token
            await _authService.LogoutAsync(userId, accessToken, ttl);

            return Ok("Đăng xuất thành công");
        }


    }
}
