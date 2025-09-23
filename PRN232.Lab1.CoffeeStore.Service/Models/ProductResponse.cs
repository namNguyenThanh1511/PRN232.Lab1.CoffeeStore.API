namespace PRN232.Lab1.CoffeeStore.Service.Models
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public double? Price { get; set; }

        public string? Description { get; set; }

        public string? Category { get; set; }
    }
}
