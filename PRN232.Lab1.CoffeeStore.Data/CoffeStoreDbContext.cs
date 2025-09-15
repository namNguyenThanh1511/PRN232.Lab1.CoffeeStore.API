using Microsoft.EntityFrameworkCore;
using PRN232.Lab1.CoffeeStore.Data.Configurations;

namespace PRN232.Lab1.CoffeeStore.Data
{
    public class CoffeStoreDbContext : DbContext
    {
        public CoffeStoreDbContext(DbContextOptions<CoffeStoreDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure your entities here
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new MenuConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductInMenuConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        }

        public DbSet<Entities.Product> Products { get; set; }
        public DbSet<Entities.Menu> Menus { get; set; }
        public DbSet<Entities.ProductInMenu> ProductInMenus { get; set; }
        public DbSet<Entities.Category> Categories { get; set; }
    }
}
