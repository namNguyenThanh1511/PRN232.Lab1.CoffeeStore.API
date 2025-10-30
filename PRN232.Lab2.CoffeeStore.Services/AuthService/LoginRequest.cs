using System.ComponentModel.DataAnnotations;

namespace PRN232.Lab2.CoffeeStore.Services.AuthService
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email hoặc số điện thoại không được để trống")]
        [StringLength(255, ErrorMessage = "Độ dài không được vượt quá 255 ký tự")]
        public string KeyLogin { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải từ 6-100 ký tự")]
        public string Password { get; set; } = string.Empty;
    }
}
