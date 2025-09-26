using PRN232.Lab2.CoffeeStore.Repositories.Entities;
using PRN232.Lab2.CoffeeStore.Repositories.UnitOfWork;
using PRN232.Lab2.CoffeeStore.Services.Models;

namespace PRN232.Lab2.CoffeeStore.Services.MenuServices
{
    public class MenuService : IMenuService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MenuService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<MenuResponse>> GetAllMenusAsync()
        {
            var menus = await _unitOfWork.Menus.GetAllAsync();
            return MapToMenuResponseList(menus);
        }

        public async Task<MenuDetailsReponse> GetMenuByIdAsync(Guid id)
        {
            var menu = await _unitOfWork.Menus.GetByIdAsync(id);
            return MapToMenuDetailsReponse(menu);
        }

        public async Task<MenuResponse> AddMenuAsync(MenuCreationRequest request)
        {
            var menu = new Menu
            {
                Name = request.Name,
                FromDate = request.FromDate ?? DateTime.Now,
                ToDate = request.ToDate ?? DateTime.Now,
            };

            menu = await _unitOfWork.Menus.AddAsync(menu);

            menu.ProductInMenus = request.ProductInMenuList?.Select(p => new ProductInMenu
            {
                ProductId = p.ProductId,
                Quantity = p.Quantity,
                MenuId = menu.Id
            }).ToList();

            await _unitOfWork.SaveChangesAsync();

            return MapToMenuResponse(menu);
        }

        public async Task UpdateMenuAsync(Guid id, MenuUpdationRequest request)
        {
            var existMenu = await _unitOfWork.Menus.GetByIdAsync(id);
            if (existMenu == null) throw new KeyNotFoundException("Menu not found");

            existMenu.Name = request.Name ?? existMenu.Name;
            existMenu.FromDate = request.FromDate ?? existMenu.FromDate;
            existMenu.ToDate = request.ToDate ?? existMenu.ToDate;

            _unitOfWork.Menus.Update(existMenu);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteMenuAsync(Guid id)
        {
            var existMenu = await _unitOfWork.Menus.GetByIdAsync(id);
            if (existMenu == null) throw new KeyNotFoundException("Menu not found");

            _unitOfWork.Menus.Remove(existMenu);
            await _unitOfWork.SaveChangesAsync();
        }

        #region Mapping
        private MenuResponse MapToMenuResponse(Menu menu) =>
            new MenuResponse
            {
                Id = menu.Id,
                Name = menu.Name,
                FromDate = menu.FromDate,
                ToDate = menu.ToDate
            };

        private MenuDetailsReponse MapToMenuDetailsReponse(Menu? menu)
        {
            if (menu == null) return null;

            return new MenuDetailsReponse
            {
                Id = menu.Id,
                Name = menu.Name,
                FromDate = menu.FromDate,
                ToDate = menu.ToDate,
                Products = menu.ProductInMenus?
                    .Where(pm => pm.Product != null)
                    .Select(pm => new ProductInMenuResponse
                    {
                        Id = pm.ProductId.Value,
                        Name = pm.Product?.Name ?? "",
                        Description = pm.Product?.Description,
                        Quantity = pm.Quantity,
                        Price = pm.Product?.Price,
                        Category = pm.Product?.Category.Name
                    }).ToList() ?? new List<ProductInMenuResponse>()
            };
        }

        private IEnumerable<MenuResponse> MapToMenuResponseList(IEnumerable<Menu> menus) =>
            menus.Select(m => MapToMenuResponse(m));
        #endregion
    }
}
