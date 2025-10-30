using System.Net;

namespace PRN232.Lab2.CoffeeStore.Services.Exceptions
{
    public class InvalidCredentialsException : BaseDomainException
    {
        public InvalidCredentialsException(string message = "Thông tin đăng nhập không chính xác")
            : base(message, HttpStatusCode.BadRequest) { }
    }
}
