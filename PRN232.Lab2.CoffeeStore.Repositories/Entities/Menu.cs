namespace PRN232.Lab2.CoffeeStore.Repositories.Entities
{
    public class Menu
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime CreatedDate { get; set; }

        // Quan hệ N - N với Product thông qua ProductInMenu
        public ICollection<ProductInMenu> ProductInMenus { get; set; } = new List<ProductInMenu>();
    }
}
