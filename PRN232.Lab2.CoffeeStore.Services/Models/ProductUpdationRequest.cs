namespace PRN232.Lab2.CoffeeStore.Services.Models
{
    public class ProductUpdationRequest
    {
        public string Name { get; set; }

        public decimal? Price { get; set; }

        public string? Description { get; set; }

        public Guid? CategoryId { get; set; }
    }
}
