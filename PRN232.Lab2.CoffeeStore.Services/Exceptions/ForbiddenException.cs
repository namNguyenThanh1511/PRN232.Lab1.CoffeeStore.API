using System.Net;

namespace PRN232.Lab2.CoffeeStore.Services.Exceptions
{
    public class ForbiddenException : BaseDomainException
    {
        public ForbiddenException(string message = "Bị cấm truy cập")
            : base(message, HttpStatusCode.Forbidden) { }
    }
}
