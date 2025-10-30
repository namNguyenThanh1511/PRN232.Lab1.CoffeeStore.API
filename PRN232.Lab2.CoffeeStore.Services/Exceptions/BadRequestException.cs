using System.Net;

namespace PRN232.Lab2.CoffeeStore.Services.Exceptions
{
    public class BadRequestException : BaseDomainException
    {
        public BadRequestException(string message = "Yêu cầu không hợp lệ")
            : base(message, HttpStatusCode.BadRequest) { }
    }
}
