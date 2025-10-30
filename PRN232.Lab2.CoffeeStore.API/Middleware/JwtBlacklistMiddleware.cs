using PRN232.Lab2.CoffeeStore.Services.AuthService;

namespace PRN232.Lab2.CoffeeStore.API.Middleware
{
    public class JwtBlacklistMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtBlacklistMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var _jwtService = context.RequestServices.GetRequiredService<IJwtService>();

            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (!string.IsNullOrEmpty(token) && await _jwtService.IsRevokeAsync(token))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Token has been revoked");
                return;
            }

            await _next(context);
        }
    }

}
