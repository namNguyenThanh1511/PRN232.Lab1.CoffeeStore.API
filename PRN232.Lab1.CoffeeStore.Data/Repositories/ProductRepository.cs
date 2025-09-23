using Microsoft.EntityFrameworkCore;
using PRN232.Lab1.CoffeeStore.Data.Entities;

namespace PRN232.Lab1.CoffeeStore.Data.Repositories
{
    public class ProductRepository
    {
        private readonly CoffeStoreDbContext _context;

        public ProductRepository(CoffeStoreDbContext context)
        {
            _context = context;
        }

        // Get all products with category
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
        }

        // Get product by id with category
        public async Task<Product?> GetProductByIdAsync(Guid id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductInMenus)
                    .ThenInclude(pm => pm.Menu)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        // Add product
        public async Task<Product> AddProductAsync(Product product)
        {
            var createdProduct = await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            // Load lại Category
            await _context.Entry(createdProduct.Entity)
                          .Reference(p => p.Category)
                          .LoadAsync();

            return createdProduct.Entity;
        }

        // Update product
        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        // Delete product
        public async Task DeleteProductAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
