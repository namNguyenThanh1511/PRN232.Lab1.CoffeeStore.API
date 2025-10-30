using PRN232.Lab2.CoffeeStore.Services.Models;

namespace PRN232.Lab2.CoffeeStore.Services.MenuServices
{
    public interface IMenuService
    {
        Task<List<MenuResponse>> GetAllMenusAsync();
        Task<MenuDetailsReponse> GetMenuByIdAsync(Guid id);
        Task<MenuResponse> AddMenuAsync(MenuCreationRequest request);
        Task UpdateMenuAsync(Guid id, MenuUpdationRequest request);
        Task DeleteMenuAsync(Guid id);
    }
}
