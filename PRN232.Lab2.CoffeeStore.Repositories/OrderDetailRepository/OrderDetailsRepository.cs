using PRN232.Lab2.CoffeeStore.Repositories.Entities;
using PRN232.Lab2.CoffeeStore.Repositories.GenericRepository;

namespace PRN232.Lab2.CoffeeStore.Repositories.OrderDetailRepository
{
    public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(CoffeStoreDbContext context) : base(context)
        {
        }

    }
}