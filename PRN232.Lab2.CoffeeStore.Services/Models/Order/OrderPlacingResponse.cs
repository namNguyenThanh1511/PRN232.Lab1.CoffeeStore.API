namespace PRN232.Lab2.CoffeeStore.Services.Models.Order
{
    public class OrderPlacingResponse
    {
        public long Id { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? Status { get; set; }

        public Guid? CustomerId { get; set; }

        public List<OrderItemResponse>? OrderItems { get; set; }
    }
}
