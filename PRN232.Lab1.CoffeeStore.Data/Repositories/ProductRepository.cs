namespace PRN232.Lab1.CoffeeStore.Data.Repositories
{
    public class ProductRepository
    {
        private readonly CoffeStoreDbContext _context;
        public ProductRepository() { }
        public ProductRepository(CoffeStoreDbContext context)
        {
            _context = context;
        }
        // Implement CRUD operations for Product entity here

        public IEnumerable<Entities.Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }
        public Entities.Product? GetProductById(Guid id)
        {
            return _context.Products.Find(id);
        }
        public void AddProduct(Entities.Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        public void UpdateProduct(Entities.Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
        public void DeleteProduct(Guid id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}
