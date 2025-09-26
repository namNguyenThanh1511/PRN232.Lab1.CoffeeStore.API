using Microsoft.AspNetCore.Mvc;
using PRN232.Lab2.CoffeeStore.Repositories.Entities;
using PRN232.Lab2.CoffeeStore.Services.MenuServices;
using PRN232.Lab2.CoffeeStore.Services.Models;

namespace PRN232.Lab2.CoffeeStore.API.Controllers
{
    [Route("api/menus")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenusController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        // GET: api/menus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuResponse>>> GetMenus()
        {
            var menus = await _menuService.GetAllMenusAsync();
            return Ok(menus);
        }

        // GET: api/menus/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Menu>> GetMenu(Guid id)
        {
            var menu = await _menuService.GetMenuByIdAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            return Ok(menu);
        }

        // POST: api/menus
        [HttpPost]
        public async Task<ActionResult<MenuResponse>> PostMenu(MenuCreationRequest request)
        {
            if (request == null)
                return BadRequest();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return await _menuService.AddMenuAsync(request);
        }

        // PUT: api/menus/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenu(Guid id, MenuUpdationRequest request)
        {
            try
            {
                await _menuService.UpdateMenuAsync(id, request);
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        // DELETE: api/menus/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(Guid id)
        {
            try
            {
                await _menuService.DeleteMenuAsync(id);
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }
    }
}
