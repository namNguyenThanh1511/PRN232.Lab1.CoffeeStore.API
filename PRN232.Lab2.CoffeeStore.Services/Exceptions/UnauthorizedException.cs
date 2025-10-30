using System.Net;

namespace PRN232.Lab2.CoffeeStore.Services.Exceptions
{
    public class UnauthorizedException : BaseDomainException
    {
        public UnauthorizedException(string message = "Không có quyền truy cập")
            : base(message, HttpStatusCode.Unauthorized) { }
    }
}
