using PRN232.Lab2.CoffeeStore.Repositories;
using PRN232.Lab2.CoffeeStore.Services.Models.Order;

namespace PRN232.Lab2.CoffeeStore.Services.OrderService
{
    public interface IOrderService
    {
        Task<(OrderPlacingResponse order, string paymentUrl)> PlaceOrder(OrderPlacingRequest request);

        Task<(List<OrderPlacingResponse>, MetaData metaData)> GetAllOrders(OrderSearchParams searchParams);

        Task<bool> ProcessPayingOrder(OrderPayingRequest request);

    }
}
