namespace PRN232.Lab2.CoffeeStore.Repositories.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public PaymentMethod Method { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public long OrderId { get; set; }
        public Order Order { get; set; }
    }

    public enum PaymentMethod
    {
        Cash,
        OnlineBanking,
    }

    public enum PaymentStatus
    {
        Pending,
        Completed,
        Failed,
    }
}
