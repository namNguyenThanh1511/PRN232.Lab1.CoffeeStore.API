namespace PRN232.Lab1.CoffeeStore.Service.Models
{
    public class ProductDetailsResponse : ProductResponse
    {
        public Guid? CategoryId { get; set; }
        public List<MenuResponse> Menus { get; set; }
    }
}
