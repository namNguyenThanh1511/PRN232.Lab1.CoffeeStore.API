using PRN232.Lab2.CoffeeStore.Repositories.GenericRepository;

namespace PRN232.Lab2.CoffeeStore.Repositories.MenuRepository
{
    public class MenuRepository : GenericRepository<Entities.Menu>, IMenuRepository
    {
        private readonly CoffeStoreDbContext _context;
        public MenuRepository(CoffeStoreDbContext context) : base(context) { }

        //public async Task<IEnumerable<Entities.Menu>> GetAllMenusAsync()
        //{
        //    return await _context.Menus.ToListAsync();
        //}

        //public async Task<Entities.Menu?> GetMenuByIdAsync(Guid id)
        //{
        //    return await _context.Menus.Include(m => m.ProductInMenus).ThenInclude(pm => pm.Product).ThenInclude(p => p.Category).FirstOrDefaultAsync(m => m.Id == id);
        //}

        //public async Task<Entities.Menu> AddMenuAsync(Entities.Menu menu)
        //{
        //    var createdMenu = await _context.Menus.AddAsync(menu);
        //    await _context.SaveChangesAsync();
        //    return createdMenu.Entity;
        //}

        //public Entities.Menu AddMenu(Entities.Menu menu)
        //{
        //    var createdMenu = _context.Menus.Add(menu);
        //    return createdMenu.Entity;
        //}

        //public async Task UpdateMenuAsync(Entities.Menu menu)
        //{
        //    _context.Menus.Update(menu);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task DeleteMenuAsync(Guid id)
        //{
        //    var menu = await _context.Menus.FindAsync(id);
        //    if (menu != null)
        //    {
        //        _context.Menus.Remove(menu);
        //        await _context.SaveChangesAsync();
        //    }
        //}

        //public int SaveChanges()
        //{
        //    return _context.SaveChanges();
        //}
    }
}
