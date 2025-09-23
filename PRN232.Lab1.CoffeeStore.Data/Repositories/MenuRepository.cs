using Microsoft.EntityFrameworkCore;
using PRN232.Lab1.CoffeeStore.Data.Entities;

namespace PRN232.Lab1.CoffeeStore.Data.Repositories
{
    public class MenuRepository
    {
        private readonly CoffeStoreDbContext _context;
        public MenuRepository(CoffeStoreDbContext context) => _context = context;

        public async Task<IEnumerable<Menu>> GetAllMenusAsync()
        {
            return await _context.Menus.ToListAsync();
        }

        public async Task<Menu?> GetMenuByIdAsync(Guid id)
        {
            return await _context.Menus.Include(m => m.ProductInMenus).ThenInclude(pm => pm.Product).ThenInclude(p => p.Category).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Menu> AddMenuAsync(Menu menu)
        {
            var createdMenu = await _context.Menus.AddAsync(menu);
            await _context.SaveChangesAsync();
            return createdMenu.Entity;
        }

        public Menu AddMenu(Menu menu)
        {
            var createdMenu = _context.Menus.Add(menu);
            return createdMenu.Entity;
        }

        public async Task UpdateMenuAsync(Menu menu)
        {
            _context.Menus.Update(menu);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMenuAsync(Guid id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu != null)
            {
                _context.Menus.Remove(menu);
                await _context.SaveChangesAsync();
            }
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
