using Net.payOS.Types;
using PRN232.Lab2.CoffeeStore.Repositories.Entities;

namespace PRN232.Lab2.CoffeeStore.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<string> CreatePaymentLink(Guid orderId);

        Task<string> CreatePaymentLink(Order order);

        WebhookData? VerifyPaymentWebhookData(dynamic webhookBody);


    }
}
