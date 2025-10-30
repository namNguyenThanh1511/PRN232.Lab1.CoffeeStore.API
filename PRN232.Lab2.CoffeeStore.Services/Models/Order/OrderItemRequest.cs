namespace PRN232.Lab2.CoffeeStore.Services.Models.Order
{
    public class OrderItemRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
