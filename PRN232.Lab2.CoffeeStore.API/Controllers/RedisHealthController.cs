using Microsoft.AspNetCore.Mvc;
using PRN232.Lab2.CoffeeStore.Services.CacheService;

namespace PRN232.Lab2.CoffeeStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisHealthController : ControllerBase
    {
        private readonly IRedisService _redis;

        public RedisHealthController(IRedisService redis)
        {
            _redis = redis;
        }

        [HttpGet("set")]
        public async Task<IActionResult> SetValue()
        {
            await _redis.SetAsync("testKey", "Hello Redis!", TimeSpan.FromMinutes(10));
            return Ok("Set done");
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetValue()
        {
            var value = await _redis.GetAsync("testKey");
            return Ok(value);
        }
    }
}
