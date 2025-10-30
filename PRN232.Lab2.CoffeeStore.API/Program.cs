using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN232.Lab2.CoffeeStore.API.Extensions;
using PRN232.Lab2.CoffeeStore.API.Middleware;
using PRN232.Lab2.CoffeeStore.API.Models;
using PRN232.Lab2.CoffeeStore.Repositories;
using StackExchange.Redis;

namespace PRN232.Lab2.CoffeeStore.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            DotNetEnv.Env.Load();

            builder.Services.AddControllers().AddXmlSerializerFormatters();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.ConfigureSwaggerForAuthentication();
            builder.Services.ConfigureJWT(builder.Configuration);
            builder.Services.ConfigureGlobalException();

            // Configure logging (Console + Debug)
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            var environment = builder.Environment.EnvironmentName;
            builder.Logging.AddConsole();

            // Database connection
            var connectionString = builder.Configuration.GetConnectionString("CoffeeStoreDB");
            builder.Services.AddDbContext<CoffeStoreDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Redis connection
            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var redisConnection = builder.Configuration.GetConnectionString("Redis") ?? "localhost:6379";
                return ConnectionMultiplexer.Connect(redisConnection);
            });

            builder.Services.AddApplicationServices(builder.Configuration);

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors.Select(e => new ApiError { Message = e.ErrorMessage }))
                        .ToList();

                    var response = new ApiResponse
                    {
                        IsSuccess = false,
                        Message = "Dữ liệu không hợp lệ",
                        Errors = errors
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            var app = builder.Build();

            // 🔥 LOG MÔI TRƯỜNG VÀ CONNECTION INFO
            var logger = app.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("🚀 Application starting in {Environment} environment", environment);
            logger.LogInformation("📦 SQL Server connection string: {Connection}", connectionString);
            logger.LogInformation("🔗 Redis connection: {Redis}", builder.Configuration.GetConnectionString("Redis"));

            app.UseMiddleware<JwtBlacklistMiddleware>();

            // Apply pending migrations automatically
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<CoffeStoreDbContext>();
                if (db.Database.GetPendingMigrations().Any())
                {
                    logger.LogInformation("🛠 Applying pending migrations...");
                    db.Database.Migrate();
                    logger.LogInformation("✅ Database migrations applied successfully.");
                }
                else
                {
                    logger.LogInformation("✅ No pending migrations found.");
                }
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseExceptionHandler();

            app.MapControllers();

            app.Run();
        }
    }
}
