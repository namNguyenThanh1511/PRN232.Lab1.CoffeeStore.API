using PRN232.Lab2.CoffeeStore.Repositories.Entities;
using PRN232.Lab2.CoffeeStore.Repositories.GenericRepository;

namespace PRN232.Lab2.CoffeeStore.Repositories.CategoryRepository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(CoffeStoreDbContext context) : base(context) { }
    }
}
