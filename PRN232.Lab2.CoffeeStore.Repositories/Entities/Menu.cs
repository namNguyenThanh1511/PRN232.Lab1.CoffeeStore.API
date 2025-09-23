namespace PRN232.Lab1.CoffeeStore.Data.Entities
{
    public class Menu
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        // Quan hệ N - N với Product thông qua ProductInMenu
        public ICollection<ProductInMenu> ProductInMenus { get; set; } = new List<ProductInMenu>();
    }
}
