namespace PRN232.Lab1.CoffeeStore.Data.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        //fk
        public Guid? CategoryId { get; set; }
        // Quan hệ N - 1
        public Category Category { get; set; }

        // Quan hệ N - N với Menu thông qua ProductInMenu
        public ICollection<ProductInMenu> ProductInMenus { get; set; } = new List<ProductInMenu>();
    }
}
