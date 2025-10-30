using Microsoft.AspNetCore.Mvc;
using PRN232.Lab2.CoffeeStore.API.Models;
using PRN232.Lab2.CoffeeStore.Services.Exceptions;
using PRN232.Lab2.CoffeeStore.Services.MenuServices;
using PRN232.Lab2.CoffeeStore.Services.Models;

namespace PRN232.Lab2.CoffeeStore.API.Controllers
{
    [Route("api/menus")]
    public class MenusController : BaseController
    {
        private readonly IMenuService _menuService;

        public MenusController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        // GET: api/menus
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<MenuResponse>>>> GetMenus()
        {
            var menus = await _menuService.GetAllMenusAsync();
            return Ok(menus);
        }

        // GET: api/menus/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<MenuDetailsReponse>>> GetMenu(Guid id)
        {
            var menu = await _menuService.GetMenuByIdAsync(id);
            if (menu == null)
            {
                throw new NotFoundException("Menu not found");
            }
            return Ok(menu, "Lấy menu thành công");
        }

        // POST: api/menus
        [HttpPost]
        public async Task<ActionResult<ApiResponse<MenuResponse>>> PostMenu(MenuCreationRequest request)
        {
            var result = await _menuService.AddMenuAsync(request);
            return Created(result, "Tạo Menu thành công");
        }

        // PUT: api/menus/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> PutMenu(Guid id, MenuUpdationRequest request)
        {
            await _menuService.UpdateMenuAsync(id, request);
            return NoContent();
        }

        // DELETE: api/menus/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(Guid id)
        {
            await _menuService.DeleteMenuAsync(id);
            return NoContent();
        }
    }
}
