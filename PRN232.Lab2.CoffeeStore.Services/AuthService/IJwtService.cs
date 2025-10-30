using PRN232.Lab2.CoffeeStore.Repositories.Entities;
using PRN232.Lab2.CoffeeStore.Services.Configuration;

namespace PRN232.Lab2.CoffeeStore.Services.AuthService
{
    public interface IJwtService
    {
        /// <summary>
        /// Tạo JWT access token
        /// </summary>
        string GenerateAccessToken(User user);

        /// <summary>
        /// Tạo refresh token
        /// </summary>
        Task<string> GenerateRefreshTokenAsync(Guid userId);

        /// <summary>
        /// Validate access token
        /// </summary>
        bool ValidateAccessToken(string token);

        /// <summary>
        /// Validate refresh token
        /// </summary>
        Task<bool> ValidateRefreshTokenAsync(string token);

        Task<TokenResponse> RefreshTokenAsync(string refreshToken);

        Task<bool> IsRevokeAsync(string refreshToken);
    }
}
