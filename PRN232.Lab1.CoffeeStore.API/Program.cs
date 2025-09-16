
using Microsoft.EntityFrameworkCore;
using PRN232.Lab1.CoffeeStore.Data;
using PRN232.Lab1.CoffeeStore.Data.Repositories;

namespace PRN232.Lab1.CoffeeStore.API
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
            options.UseSqlServer(builder.Configuration.GetConnectionString("CoffeeStoreDB_Docker")));


            builder.Services.AddScoped<ProductRepository>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                using (var scope = app.Services.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<CoffeStoreDbContext>();
                    if (db.Database.GetPendingMigrations().Any()) //only migrate if there are any new migrate file
                    {
                        db.Database.Migrate();
                    }
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
