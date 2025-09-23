namespace PRN232.Lab1.CoffeeStore.Service.Models
{
    public class MenuDetailsReponse : MenuResponse
    {
        public List<ProductInMenuResponse> Products { get; set; }
    }
}
