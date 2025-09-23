namespace PRN232.Lab1.CoffeeStore.Service.Models
{
    public class ProductUpdationRequest
    {
        public string Name { get; set; }

        public double? Price { get; set; }

        public string? Description { get; set; }

        public Guid? CategoryId { get; set; }
    }
}
