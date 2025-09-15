namespace PRN232.Lab1.CoffeeStore.Data.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public Guid? CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
