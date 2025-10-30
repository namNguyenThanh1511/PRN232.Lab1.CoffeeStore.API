namespace PRN232.Lab2.CoffeeStore.Repositories.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; }
        public int Stock { get; set; }
        //fk
        public Guid? CategoryId { get; set; }
        // Quan hệ N - 1
        public Category Category { get; set; }
        // Quan hệ N - N với Menu thông qua ProductInMenu
        public ICollection<ProductInMenu> ProductInMenus { get; set; } = new List<ProductInMenu>();
        // Quan hệ 1 - N với OrderDetail
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
