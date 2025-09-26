namespace PRN232.Lab2.CoffeeStore.Services.Models
{
    public class ProductDetailsResponse : ProductResponse
    {
        public Guid? CategoryId { get; set; }
        public List<MenuResponse> Menus { get; set; }
    }
}
