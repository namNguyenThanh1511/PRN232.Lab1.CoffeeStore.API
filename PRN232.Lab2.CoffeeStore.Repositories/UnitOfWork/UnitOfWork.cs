using Microsoft.EntityFrameworkCore.Storage;
using PRN232.Lab2.CoffeeStore.Repositories.CategoryRepository;
using PRN232.Lab2.CoffeeStore.Repositories.MenuRepository;
using PRN232.Lab2.CoffeeStore.Repositories.OrderRepository;
using PRN232.Lab2.CoffeeStore.Repositories.PaymentRepository;
using PRN232.Lab2.CoffeeStore.Repositories.ProductRepository;
using PRN232.Lab2.CoffeeStore.Repositories.UserRepository;

namespace PRN232.Lab2.CoffeeStore.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CoffeStoreDbContext _context;

        private IDbContextTransaction? _transaction;

        public IProductRepository Products { get; private set; }

        public IMenuRepository Menus { get; private set; }

        public ICategoryRepository Categories { get; private set; }

        public IUserRepository Users { get; private set; }

        public IOrderRepository Orders { get; private set; }

        public IPaymentRepository Payments { get; private set; }

        public UnitOfWork(CoffeStoreDbContext context, IProductRepository productRepository, IMenuRepository menuRepository, ICategoryRepository categoryRepository, IUserRepository userRepository, IOrderRepository orderRepository, IPaymentRepository paymentRepository)
        {
            _context = context;
            Products = productRepository;
            Menus = menuRepository;
            Categories = categoryRepository;
            Users = userRepository;
            Orders = orderRepository;
            Payments = paymentRepository;

        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task BeginTransaction()
        {
            if (_context.Database.CurrentTransaction == null)
            {
                await _context.Database.BeginTransactionAsync();
            }
        }

        public async Task CommitTransaction()
        {
            try
            {
                await _context.Database.CommitTransactionAsync();
                _transaction?.Commit();

            }
            catch
            {
                await RollbackTransaction();
                throw;
            }
            finally
            {
                if (_context.Database.CurrentTransaction != null)
                {
                    await _context.Database.CurrentTransaction.DisposeAsync();
                }
            }

        }

        public async Task RollbackTransaction()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
        public async Task<ITransaction?> GetCurrentTransaction()
        {
            throw new NotImplementedException();

        }
    }
}
