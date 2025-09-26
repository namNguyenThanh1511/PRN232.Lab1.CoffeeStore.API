
using Microsoft.EntityFrameworkCore;
using PRN232.Lab2.CoffeeStore.API.Extensions;
using PRN232.Lab2.CoffeeStore.Repositories;

namespace PRN232.Lab2.CoffeeStore.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //configure database connection string
            builder.Services.AddDbContext<CoffeStoreDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("CoffeeStoreDB"),
                                    sqlOptions => sqlOptions.EnableRetryOnFailure())
            );

            var connectionString = builder.Configuration.GetConnectionString("CoffeeStoreDB");

            builder.Services.AddApplicationServices(builder.Configuration);

            var app = builder.Build();


            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<CoffeStoreDbContext>();
                if (db.Database.GetPendingMigrations().Any()) //only migrate if there are any new migrate file
                {
                    db.Database.Migrate();
                }
            }



            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //}


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
