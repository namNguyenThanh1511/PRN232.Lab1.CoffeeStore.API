using Microsoft.AspNetCore.Mvc;

namespace PRN232.Lab2.CoffeeStore.API.Controllers
{
    [Route("api/[controller]")]

    public class TestController : BaseController
    {
        [HttpGet("testcases/login")]
        public IActionResult GetLoginTestCases()
        {
            var json = System.IO.File.ReadAllText("D:\\Semester 8\\PRN232\\Lab\\02\\PRN232.Lab2.CoffeeStore\\test\\login\\login_testcases.json");
            return Content(json, "application/json");
        }

    }
}
