using Microsoft.AspNetCore.Http;
using PRN232.Lab2.CoffeeStore.Repositories.Entities;
using System.Security.Claims;

namespace PRN232.Lab2.CoffeeStore.Services.UserService
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public (string userId, string userRole) GetCurrentUser()
        {
            var userId = GetUserId();
            var userRole = GetUserRole();

            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User not authenticated.");

            return (userId, userRole);
        }

        public string GetUserId()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }

        public string GetUserRole()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;
        }

        public bool IsAdmin()
        {
            return GetUserRole() == Role.Admin.ToString();
        }
    }
}
