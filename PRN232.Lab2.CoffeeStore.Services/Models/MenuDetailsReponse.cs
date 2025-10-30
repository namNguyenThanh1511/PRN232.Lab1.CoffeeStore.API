namespace PRN232.Lab2.CoffeeStore.Services.Models
{
    public class MenuDetailsReponse : MenuResponse
    {
        public List<ProductInMenuResponse> Products { get; set; }
    }
}
