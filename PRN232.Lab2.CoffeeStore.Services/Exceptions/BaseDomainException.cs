using System.Net;

namespace PRN232.Lab2.CoffeeStore.Services.Exceptions
{
    /// <summary>
    /// Base class for domain-level exceptions with HTTP status mapping.
    /// </summary>
    public abstract class BaseDomainException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        protected BaseDomainException(string message, HttpStatusCode statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
