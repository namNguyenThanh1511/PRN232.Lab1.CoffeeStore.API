using PRN232.Lab2.CoffeeStore.Repositories.CategoryRepository;
using PRN232.Lab2.CoffeeStore.Repositories.MenuRepository;
using PRN232.Lab2.CoffeeStore.Repositories.OrderRepository;
using PRN232.Lab2.CoffeeStore.Repositories.PaymentRepository;
using PRN232.Lab2.CoffeeStore.Repositories.ProductRepository;
using PRN232.Lab2.CoffeeStore.Repositories.UserRepository;

namespace PRN232.Lab2.CoffeeStore.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }

        IMenuRepository Menus { get; }

        ICategoryRepository Categories { get; }

        IUserRepository Users { get; }

        IOrderRepository Orders { get; }

        IPaymentRepository Payments { get; }

        Task BeginTransaction();

        Task CommitTransaction();

        Task RollbackTransaction();

        Task<ITransaction?> GetCurrentTransaction();

        Task<int> SaveChangesAsync();
    }
}
