using PRN232.Lab2.CoffeeStore.Repositories.CategoryRepository;
using PRN232.Lab2.CoffeeStore.Repositories.MenuRepository;
using PRN232.Lab2.CoffeeStore.Repositories.ProductRepository;

namespace PRN232.Lab2.CoffeeStore.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CoffeStoreDbContext _context;
        public IProductRepository Products { get; private set; }

        public IMenuRepository Menus { get; private set; }

        public ICategoryRepository Categories { get; private set; }


        public UnitOfWork(CoffeStoreDbContext context, IProductRepository productRepository, IMenuRepository menuRepository, ICategoryRepository categoryRepository)
        {
            _context = context;
            Products = productRepository;
            Menus = menuRepository;
            Categories = categoryRepository;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
