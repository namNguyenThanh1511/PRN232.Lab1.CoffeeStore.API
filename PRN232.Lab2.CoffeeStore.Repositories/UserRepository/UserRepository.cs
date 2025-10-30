using PRN232.Lab2.CoffeeStore.Repositories.Entities;
using PRN232.Lab2.CoffeeStore.Repositories.GenericRepository;

namespace PRN232.Lab2.CoffeeStore.Repositories.UserRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(CoffeStoreDbContext context) : base(context)
        {

        }

        public Task<User?> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserByPhoneNumberAsync(string phone)
        {
            throw new NotImplementedException();
        }
    }
}
