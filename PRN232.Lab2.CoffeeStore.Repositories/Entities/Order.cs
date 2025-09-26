namespace PRN232.Lab2.CoffeeStore.Repositories.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public DateTime CreatedDate { get; set; }

        public ICollection<OrderDetail> OrderItems { get; set; } = new List<OrderDetail>();
        //fk
        public Guid? CustomerId { get; set; }
        // Quan hệ N - 1 với User
        public User? Customer { get; set; }
        //fk
        public Guid? paymentId { get; set; }
        // Quan hệ 1 - 1 với Payment
        public Payment? Payment { get; set; }

    }

    public enum OrderStatus
    {
        Pending,
        Completed,
        Cancelled
    }
}
