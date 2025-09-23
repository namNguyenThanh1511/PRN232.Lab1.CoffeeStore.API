namespace PRN232.Lab1.CoffeeStore.Data.Entities
{
    public class ProductInMenu
    {
        public Guid Id { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? MenuId { get; set; }
        public int Quantity { get; set; }

        // Quan hệ N - 1
        public virtual Menu? Menu { get; set; }
        public virtual Product? Product { get; set; }
    }
}
