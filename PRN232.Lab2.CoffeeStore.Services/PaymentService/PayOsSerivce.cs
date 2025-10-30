using Microsoft.Extensions.Configuration;
using Net.payOS;

namespace PRN232.Lab2.CoffeeStore.Services.PaymentService
{
    using Microsoft.Extensions.Logging;
    using Net.payOS.Types;

    public class PayOsService
    {
        private readonly PayOS _payOS;  // Giữ private
        private readonly ILogger<PayOsService> _logger;  // Optional: Để log lỗi

        public PayOsService(IConfiguration configuration, ILogger<PayOsService> logger)
        {
            var clientId = configuration["PayOS:ClientId"];
            var apiKey = configuration["PayOS:ApiKey"];
            var checksumKey = configuration["PayOS:ChecksumKey"];
            _payOS = new PayOS(clientId, apiKey, checksumKey);
            _logger = logger;
        }

        // Public method để tạo payment link
        public async Task<CreatePaymentResult> CreatePaymentLinkAsync(PaymentData paymentData)
        {
            try
            {
                var result = await _payOS.createPaymentLink(paymentData);  // Gọi internal
                return result;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Lỗi tạo payment link cho orderCode: {OrderCode}", paymentData.orderCode);
                throw;  // Re-throw để handle ở layer trên
            }
        }

        // Các method khác nếu cần, ví dụ: VerifyWebhook, GetPaymentInfo, etc.
        public WebhookData? VerifyPaymentWebhookData(dynamic webhookBody)
        {
            return _payOS.verifyPaymentWebhookData(webhookBody);
        }
    }
}
