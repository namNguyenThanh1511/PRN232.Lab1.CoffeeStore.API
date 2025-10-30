using System.Net;

namespace PRN232.Lab2.CoffeeStore.Services.Exceptions
{
    public class NotFoundException : BaseDomainException
    {
        public NotFoundException(string message = "Không tìm thấy dữ liệu")
            : base(message, HttpStatusCode.NotFound) { }
    }
}
