using Microsoft.EntityFrameworkCore;
using PRN232.Lab1.CoffeeStore.Data.Configurations;
using PRN232.Lab1.CoffeeStore.Data.Entities;

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


            // Category
            var categories = new[]
            {
        new { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Coffee", Description = "Coffee drinks like espresso, latte, cappuccino." },
        new { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Tea", Description = "Black tea, green tea, and herbal teas." },
        new { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Juice", Description = "Fresh fruit and vegetable juices." },
        new { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Name = "Smoothies", Description = "Blended fruit smoothies with yogurt or milk." },
        new { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), Name = "Bakery", Description = "Cakes, breads, croissants, and pastries." },
        new { Id = Guid.Parse("66666666-6666-6666-6666-666666666666"), Name = "Sandwich", Description = "Fresh sandwiches with meats and veggies." },
        new { Id = Guid.Parse("77777777-7777-7777-7777-777777777777"), Name = "Pasta", Description = "Italian pasta dishes with different sauces." },
        new { Id = Guid.Parse("88888888-8888-8888-8888-888888888888"), Name = "Pizza", Description = "Thin crust and deep dish pizzas." },
        new { Id = Guid.Parse("99999999-9999-9999-9999-999999999999"), Name = "Salad", Description = "Fresh and healthy salads." },
        new { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Name = "Soup", Description = "Warm soups for all seasons." },
    };
            modelBuilder.Entity<Category>().HasData(categories);

            // Menu
            var menus = new[]
            {
        new { Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), Name = "Breakfast Menu", FromDate = new DateTime(2025, 1, 1), ToDate = new DateTime(2025, 12, 31) },
        new { Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), Name = "Lunch Menu", FromDate = new DateTime(2025, 1, 1), ToDate = new DateTime(2025, 12, 31) },
        new { Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), Name = "Dinner Menu", FromDate = new DateTime(2025, 1, 1), ToDate = new DateTime(2025, 12, 31) },
        new { Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), Name = "Drinks Menu", FromDate = new DateTime(2025, 1, 1), ToDate = new DateTime(2025, 12, 31) },
        new { Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"), Name = "Dessert Menu", FromDate = new DateTime(2025, 1, 1), ToDate = new DateTime(2025, 12, 31) },
        new { Id = Guid.Parse("12121212-1212-1212-1212-121212121212"), Name = "Weekend Specials", FromDate = new DateTime(2025, 1, 1), ToDate = new DateTime(2025, 12, 31) },
        new { Id = Guid.Parse("13131313-1313-1313-1313-131313131313"), Name = "Vegan Menu", FromDate = new DateTime(2025, 1, 1), ToDate = new DateTime(2025, 12, 31) },
        new { Id = Guid.Parse("14141414-1414-1414-1414-141414141414"), Name = "Kids Menu", FromDate = new DateTime(2025, 1, 1), ToDate = new DateTime(2025, 12, 31) },
        new { Id = Guid.Parse("15151515-1515-1515-1515-151515151515"), Name = "Happy Hour Menu", FromDate = new DateTime(2025, 1, 1), ToDate = new DateTime(2025, 12, 31) },
        new { Id = Guid.Parse("16161616-1616-1616-1616-161616161616"), Name = "Seasonal Menu", FromDate = new DateTime(2025, 1, 1), ToDate = new DateTime(2025, 12, 31) },
    };
            modelBuilder.Entity<Menu>().HasData(menus);

            // Product
            var products = new[]
            {
        new { Id = Guid.Parse("17171717-1717-1717-1717-171717171717"), Name = "Espresso", Price = 2.5, Description = "Strong black coffee", CategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111") },
        new { Id = Guid.Parse("18181818-1818-1818-1818-181818181818"), Name = "Latte", Price = 3.0, Description = "Coffee with steamed milk", CategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111") },
        new { Id = Guid.Parse("19191919-1919-1919-1919-191919191919"), Name = "Green Tea", Price = 1.8, Description = "Refreshing green tea", CategoryId = Guid.Parse("22222222-2222-2222-2222-222222222222") },
        new { Id = Guid.Parse("20202020-2020-2020-2020-202020202020"), Name = "Orange Juice", Price = 2.2, Description = "Fresh squeezed orange juice", CategoryId = Guid.Parse("33333333-3333-3333-3333-333333333333") },
        new { Id = Guid.Parse("21212121-2121-2121-2121-212121212121"), Name = "Berry Smoothie", Price = 3.5, Description = "Mixed berry smoothie", CategoryId = Guid.Parse("44444444-4444-4444-4444-444444444444") },
        new { Id = Guid.Parse("22222222-3333-4444-5555-666666666666"), Name = "Croissant", Price = 1.5, Description = "Buttery French pastry", CategoryId = Guid.Parse("55555555-5555-5555-5555-555555555555") },
        new { Id = Guid.Parse("23232323-2323-2323-2323-232323232323"), Name = "Club Sandwich", Price = 4.5, Description = "Triple-decker sandwich", CategoryId = Guid.Parse("66666666-6666-6666-6666-666666666666") },
        new { Id = Guid.Parse("24242424-2424-2424-2424-242424242424"), Name = "Spaghetti Bolognese", Price = 6.5, Description = "Classic Italian pasta", CategoryId = Guid.Parse("77777777-7777-7777-7777-777777777777") },
        new { Id = Guid.Parse("25252525-2525-2525-2525-252525252525"), Name = "Margherita Pizza", Price = 7.0, Description = "Cheese and tomato pizza", CategoryId = Guid.Parse("88888888-8888-8888-8888-888888888888") },
        new { Id = Guid.Parse("26262626-2626-2626-2626-262626262626"), Name = "Caesar Salad", Price = 5.0, Description = "Salad with romaine lettuce and dressing", CategoryId = Guid.Parse("99999999-9999-9999-9999-999999999999") },
    };
            modelBuilder.Entity<Product>().HasData(products);

            // ProductInMenu
            var productInMenus = new[]
            {
        new { Id = Guid.Parse("27272727-2727-2727-2727-272727272727"), ProductId = Guid.Parse("17171717-1717-1717-1717-171717171717"), MenuId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), Quantity = 50 },
        new { Id = Guid.Parse("28282828-2828-2828-2828-282828282828"), ProductId = Guid.Parse("18181818-1818-1818-1818-181818181818"), MenuId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), Quantity = 40 },
        new { Id = Guid.Parse("29292929-2929-2929-2929-292929292929"), ProductId = Guid.Parse("19191919-1919-1919-1919-191919191919"), MenuId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), Quantity = 60 },
        new { Id = Guid.Parse("30303030-3030-3030-3030-303030303030"), ProductId = Guid.Parse("20202020-2020-2020-2020-202020202020"), MenuId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), Quantity = 70 },
        new { Id = Guid.Parse("31313131-3131-3131-3131-313131313131"), ProductId = Guid.Parse("21212121-2121-2121-2121-212121212121"), MenuId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), Quantity = 30 },
        new { Id = Guid.Parse("32323232-3232-3232-3232-323232323232"), ProductId = Guid.Parse("22222222-3333-4444-5555-666666666666"), MenuId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), Quantity = 80 },
        new { Id = Guid.Parse("33333333-3333-4444-5555-666666666666"), ProductId = Guid.Parse("23232323-2323-2323-2323-232323232323"), MenuId = Guid.Parse("12121212-1212-1212-1212-121212121212"), Quantity = 25 },
        new { Id = Guid.Parse("34343434-3434-3434-3434-343434343434"), ProductId = Guid.Parse("24242424-2424-2424-2424-242424242424"), MenuId = Guid.Parse("12121212-1212-1212-1212-121212121212"), Quantity = 20 },
        new { Id = Guid.Parse("35353535-3535-3535-3535-353535353535"), ProductId = Guid.Parse("25252525-2525-2525-2525-252525252525"), MenuId = Guid.Parse("14141414-1414-1414-1414-141414141414"), Quantity = 15 },
        new { Id = Guid.Parse("36363636-3636-3636-3636-363636363636"), ProductId = Guid.Parse("26262626-2626-2626-2626-262626262626"), MenuId = Guid.Parse("15151515-1515-1515-1515-151515151515"), Quantity = 35 },
    };
            modelBuilder.Entity<ProductInMenu>().HasData(productInMenus);
        }

        public DbSet<Entities.Product> Products { get; set; }
        public DbSet<Entities.Menu> Menus { get; set; }
        public DbSet<Entities.ProductInMenu> ProductInMenus { get; set; }
        public DbSet<Entities.Category> Categories { get; set; }
    }
}
