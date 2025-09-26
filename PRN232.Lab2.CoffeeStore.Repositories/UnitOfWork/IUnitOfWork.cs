using PRN232.Lab2.CoffeeStore.Repositories.CategoryRepository;
using PRN232.Lab2.CoffeeStore.Repositories.MenuRepository;
using PRN232.Lab2.CoffeeStore.Repositories.ProductRepository;

namespace PRN232.Lab2.CoffeeStore.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }

        IMenuRepository Menus { get; }

        ICategoryRepository Categories { get; }

        Task<int> SaveChangesAsync();
    }
}
