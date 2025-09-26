using PRN232.Lab2.CoffeeStore.Repositories.GenericRepository;

namespace PRN232.Lab2.CoffeeStore.Repositories.ProductRepository
{
    public class ProductRepository : GenericRepository<Entities.Product>, IProductRepository
    {


        public ProductRepository(CoffeStoreDbContext context) : base(context) { }

        // Get all products with category
        //public new async Task<IEnumerable<Entities.Product>> GetAllAsync()
        //{
        //    return await _context.Products
        //        .Include(p => p.Category)
        //        .ToListAsync();
        //}

        // Get product by id with category
        //public async Task<Entities.Product?> GetProductByIdAsync(Guid id)
        //{
        //    return await _context.Products
        //        .Include(p => p.Category)
        //        .Include(p => p.ProductInMenus)
        //            .ThenInclude(pm => pm.Menu)
        //        .FirstOrDefaultAsync(p => p.Id == id);
        //}

        // Add product
        //public async Task<Entities.Product> AddProductAsync(Entities.Product product)
        //{
        //    var createdProduct = await _context.Products.AddAsync(product);
        //    await _context.SaveChangesAsync();

        //    // Load lại Category
        //    await _context.Entry(createdProduct.Entity)
        //                  .Reference(p => p.Category)
        //                  .LoadAsync();

        //    return createdProduct.Entity;
        //}

        // Update product
        //public async Task UpdateProductAsync(Entities.Product product)
        //{
        //    _context.Products.Update(product);
        //    await _context.SaveChangesAsync();
        //}

        // Delete product
        //public async Task DeleteProductAsync(Guid id)
        //{
        //    var product = await _context.Products.FindAsync(id);
        //    if (product != null)
        //    {
        //        _context.Products.Remove(product);
        //        await _context.SaveChangesAsync();
        //    }
        //}
    }
}
