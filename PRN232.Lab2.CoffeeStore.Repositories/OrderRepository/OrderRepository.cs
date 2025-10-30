using PRN232.Lab2.CoffeeStore.Repositories.Entities;
using PRN232.Lab2.CoffeeStore.Repositories.GenericRepository;

namespace PRN232.Lab2.CoffeeStore.Repositories.OrderRepository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(CoffeStoreDbContext context) : base(context)
        {
        }
        public async Task<PagedList<Order>> GetAllOrders(IQueryable<Order> queryable, int pageNumber, int pageSize)
        {
            return await PagedList<Order>.ToPagedList(queryable, pageNumber, pageSize);
        }
    }
}
