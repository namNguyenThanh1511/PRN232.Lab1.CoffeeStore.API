namespace PRN232.Lab2.CoffeeStore.Repositories.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
