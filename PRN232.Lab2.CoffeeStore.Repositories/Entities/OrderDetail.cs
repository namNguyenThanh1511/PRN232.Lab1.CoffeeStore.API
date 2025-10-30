namespace PRN232.Lab2.CoffeeStore.Repositories.Entities
{
    public class OrderDetail
    {
        public Guid Id { get; set; }
        //FK
        public long OrderId { get; set; }
        //N - 1 , 1 order has many order details , 1 order detail belongs to 1 order
        public Order Order { get; set; }
        //FK
        public Guid ProductId { get; set; }
        //N - 1 , 1 product has many order details , 1 order detail belongs to 1 product
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
