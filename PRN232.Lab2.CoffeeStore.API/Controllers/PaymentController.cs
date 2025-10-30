using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Newtonsoft.Json;
using PRN232.Lab2.CoffeeStore.Services.Models.Order;
using PRN232.Lab2.CoffeeStore.Services.OrderService;
using PRN232.Lab2.CoffeeStore.Services.PaymentService;

namespace PRN232.Lab2.CoffeeStore.API.Controllers
{
    [ApiController]
    [Route("api/payment")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IOrderService _orderService;
        private readonly ILogger<PaymentController> _logger;


        public PaymentController(IPaymentService paymentService, IOrderService orderService, ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _orderService = orderService;
            _logger = logger;
        }



        [HttpPost("webhook/payos")]
        public async Task<IActionResult> HandleWebhook()
        {
            try
            {
                Request.EnableBuffering();
                using var reader = new StreamReader(Request.Body, leaveOpen: true);
                var body = await reader.ReadToEndAsync();
                Request.Body.Position = 0;

                _logger.LogInformation("Received PayOS webhook body: {Body}", body);

                if (string.IsNullOrWhiteSpace(body))
                    return BadRequest("Empty body");

                // ✅ Deserialize chính xác theo PayOS SDK
                var webhookType = JsonConvert.DeserializeObject<WebhookType>(body);
                if (webhookType == null)
                    return BadRequest("Invalid JSON format");

                // ✅ Gọi verify đúng kiểu dữ liệu
                var webhookData = _paymentService.VerifyPaymentWebhookData(webhookType);



                if (webhookData.code == "00")
                {
                    string description = webhookData.description;

                    if (description.Contains("PAYORDER"))
                    {
                        long orderId = webhookData.orderCode;
                        _logger.LogInformation("✅ Payment success: OrderCode={OrderCode}, Amount={Amount}",
                            webhookData.orderCode, webhookData.amount);
                        _logger.LogInformation("Extracted OrderId from description: {OrderId}", orderId);
                        await _orderService.ProcessPayingOrder(new OrderPayingRequest
                        {
                            OrderId = orderId
                        });
                    }
                    else
                    {
                        _logger.LogWarning("Description format unexpected: {Description}", description);
                    }
                    return Ok();
                }
                else
                {
                    _logger.LogWarning("❌ Invalid webhook signature or failed verification");
                    return BadRequest("Verification failed");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error processing PayOS webhook");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
