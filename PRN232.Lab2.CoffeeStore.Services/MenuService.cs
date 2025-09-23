using PRN232.Lab1.CoffeeStore.Data.Entities;
using PRN232.Lab1.CoffeeStore.Data.Repositories;
using PRN232.Lab1.CoffeeStore.Service.Models;

namespace PRN232.Lab1.CoffeeStore.Service
{
    public class MenuService
    {
        private readonly MenuRepository _menuRepository;
        public MenuService(MenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<IEnumerable<MenuResponse>> GetAllMenusAsync()
        {
            return MapToMenuResponseList(await _menuRepository.GetAllMenusAsync());
        }
        public async Task<MenuDetailsReponse> GetMenuByIdAsync(Guid id)
        {
            return MapToMenuDetailsReponse(await _menuRepository.GetMenuByIdAsync(id));
        }
        public async Task<MenuResponse> AddMenuAsync(MenuCreationRequest request)
        {
            Menu menu = new Menu
            {
                Name = request.Name,
                FromDate = (DateTime)(request.FromDate ?? null),
                ToDate = (DateTime)(request.ToDate ?? null),
            };
            menu = _menuRepository.AddMenu(menu);
            ProductInMenu[] productInMenus = request.ProductInMenuList?.Select(p => new ProductInMenu
            {
                ProductId = p.ProductId,
                Quantity = p.Quantity,
                MenuId = menu.Id
            }).ToArray() ?? Array.Empty<ProductInMenu>();
            menu.ProductInMenus = productInMenus;
            _menuRepository.SaveChanges();
            return MapToMenuResponse(menu);
        }
        public async Task UpdateMenuAsync(Guid id, MenuUpdationRequest request)
        {
            var existMenu = await _menuRepository.GetMenuByIdAsync(id);
            if (existMenu == null) throw new KeyNotFoundException("Menu not found");

            existMenu.Name = request.Name ?? existMenu.Name;
            existMenu.FromDate = request.FromDate ?? existMenu.FromDate;
            existMenu.ToDate = request.ToDate ?? existMenu.ToDate;
            await _menuRepository.UpdateMenuAsync(existMenu);
        }
        public async Task DeleteMenuAsync(Guid id)
        {
            var existMenu = await _menuRepository.GetMenuByIdAsync(id);
            if (existMenu == null) throw new KeyNotFoundException("Menu not found");
            await _menuRepository.DeleteMenuAsync(id);
        }

        private MenuResponse MapToMenuResponse(Menu menu)
        {
            return new MenuResponse
            {
                Id = menu.Id,
                Name = menu.Name,
                FromDate = menu.FromDate,
                ToDate = menu.ToDate
            };
        }

        private MenuDetailsReponse MapToMenuDetailsReponse(Menu? menu)
        {
            if (menu == null) return null;
            return new MenuDetailsReponse
            {
                Id = menu.Id,
                Name = menu.Name,
                FromDate = menu.FromDate,
                ToDate = menu.ToDate,
                Products = menu.ProductInMenus?.Where(pm => pm.Product != null).Select(pm => new ProductInMenuResponse
                {
                    Id = pm.ProductId.Value,
                    Name = pm.Product?.Name ?? "",
                    Description = pm.Product?.Description,
                    Quantity = pm.Quantity,
                    Price = pm.Product?.Price ?? null,
                    Category = pm.Product?.Category.Name
                }).ToList() ?? new List<ProductInMenuResponse>()
            };
        }

        private IEnumerable<MenuResponse> MapToMenuResponseList(IEnumerable<Menu> menus)
        {
            return menus.Select(m => MapToMenuResponse(m));
        }


    }
}
