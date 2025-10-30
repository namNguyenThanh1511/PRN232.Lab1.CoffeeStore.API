
using Microsoft.Extensions.Configuration;
using Net.payOS.Types;
using PRN232.Lab2.CoffeeStore.Repositories.Entities;
using PRN232.Lab2.CoffeeStore.Repositories.UnitOfWork;
using PRN232.Lab2.CoffeeStore.Services.Exceptions;

namespace PRN232.Lab2.CoffeeStore.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PayOsService _payOsService;

        private readonly IConfiguration _config;
        public PaymentService(IUnitOfWork unitOfWork, PayOsService payOsService, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _payOsService = payOsService;
            _config = configuration;
        }
        public async Task<string> CreatePaymentLink(Guid orderId)
        {
            // Get the order and its items  
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId) ?? throw new NotFoundException("Order not found");

            // Prepare item list for PayOS  
            var items = order.OrderItems.Select(oi => new ItemData(
                          name: oi.Product?.Name ?? "Unknown",  // Hoặc bỏ named arguments nếu không cần
                          quantity: oi.Quantity,
                          price: (int)oi.UnitPrice
                      )).ToList();

            // Convert order.Id to long (cải thiện: dùng Guid.ToString() và parse, hoặc dùng timestamp nếu cần unique)
            if (!long.TryParse(order.Id.ToString(), out var orderCode))
            {
                throw new InvalidOperationException("Order ID is not in a valid format for conversion to long.");
            }

            var returnUrl = _config.GetSection("PayOS:ReturnUrl").Value;
            var cancelUrl = _config.GetSection("PayOS:CancelUrl").Value;

            // Prepare payment data for PayOS  
            var paymentData = new PaymentData(
                orderCode: orderCode,
                amount: (int)order.TotalAmount,
                description: $"Thanh toán đơn hàng #{order.Id}",
                items: items,
                returnUrl: returnUrl,
                cancelUrl: cancelUrl
            );

            // Gọi public method của PayOsService thay vì access private field
            var paymentResult = await _payOsService.CreatePaymentLinkAsync(paymentData);

            if (paymentResult == null || string.IsNullOrEmpty(paymentResult.checkoutUrl))
            {
                throw new InvalidOperationException("Tạo link thanh toán thất bại từ PayOS.");
            }

            // Optional: Lưu payment info vào DB (ví dụ: order.PaymentLink = paymentResult.checkoutUrl; await _unitOfWork.SaveAsync();)

            return paymentResult.checkoutUrl;  // Trả về link để redirect client
        }

        public async Task<string> CreatePaymentLink(Order order)
        {
            // Prepare item list for PayOS  
            var items = order.OrderItems.Select(oi => new ItemData(
                          name: oi.Product?.Name ?? "Unknown",  // Hoặc bỏ named arguments nếu không cần
                          quantity: oi.Quantity,
                          price: (int)oi.UnitPrice
                      )).ToList();

            // Convert order.Id to long (cải thiện: dùng Guid.ToString() và parse, hoặc dùng timestamp nếu cần unique)
            //if (!long.TryParse(order.Id.ToString(), out var orderCode))
            //{
            //    throw new InvalidOperationException("Order ID is not in a valid format for conversion to long.");
            //}
            long orderCode = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            var returnUrl = _config.GetSection("PayOS:ReturnUrl").Value;
            var cancelUrl = _config.GetSection("PayOS:CancelUrl").Value;

            // Prepare payment data for PayOS  
            var paymentData = new PaymentData(
                orderCode: order.Id,
                amount: (int)order.TotalAmount,
                description: $"PAYORDER",
                items: items,
                returnUrl: returnUrl,
                cancelUrl: cancelUrl
            );

            // Gọi public method của PayOsService thay vì access private field
            var paymentResult = await _payOsService.CreatePaymentLinkAsync(paymentData);

            if (paymentResult == null || string.IsNullOrEmpty(paymentResult.checkoutUrl))
            {
                throw new InvalidOperationException("Tạo link thanh toán thất bại từ PayOS.");
            }

            // Optional: Lưu payment info vào DB (ví dụ: order.PaymentLink = paymentResult.checkoutUrl; await _unitOfWork.SaveAsync();)

            return paymentResult.checkoutUrl;  // Trả về link để redirect client 
        }

        public WebhookData? VerifyPaymentWebhookData(dynamic webhookBody)
        {
            return _payOsService.VerifyPaymentWebhookData(webhookBody);
        }
    }
}
