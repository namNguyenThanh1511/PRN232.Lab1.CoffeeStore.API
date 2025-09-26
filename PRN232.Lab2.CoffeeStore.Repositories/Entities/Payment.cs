namespace PRN232.Lab2.CoffeeStore.Repositories.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public PaymentMethod Method { get; set; }
        public string Status { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        //fk
        public Guid OrderId { get; set; }
        // 1 - 1 với Order
        public Order Order { get; set; }
    }

    public enum PaymentMethod
    {
        Cash,
        OnlineBanking,
    }
}
