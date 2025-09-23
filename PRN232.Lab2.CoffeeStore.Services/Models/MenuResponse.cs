namespace PRN232.Lab1.CoffeeStore.Service.Models
{
    public class MenuResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
    }
}
