namespace PRN232.Lab2.CoffeeStore.Services.Models
{
    public class MenuResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
    }
}
