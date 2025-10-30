using PRN232.Lab2.CoffeeStore.Repositories.Entities;
using PRN232.Lab2.CoffeeStore.Services.AuthService;
using PRN232.Lab2.CoffeeStore.Services.Models.User;

namespace PRN232.Lab2.CoffeeStore.Services.Mapper
{
    public static class UserMappingExtension
    {
        public static UserResponse ToUserResponse(User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsActive = user.IsActive,
                Role = user.Role.ToString()
            };
        }

        public static User ToUserEntity(this RegisterRequest request)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                IsActive = true, // New users are active by default
            };
        }

    }
}
