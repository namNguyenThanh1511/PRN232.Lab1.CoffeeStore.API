using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PRN232.Lab1.CoffeeStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Coffee drinks like espresso, latte, cappuccino.", "Coffee" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Black tea, green tea, and herbal teas.", "Tea" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Fresh fruit and vegetable juices.", "Juice" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "Blended fruit smoothies with yogurt or milk.", "Smoothies" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "Cakes, breads, croissants, and pastries.", "Bakery" },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "Fresh sandwiches with meats and veggies.", "Sandwich" },
                    { new Guid("77777777-7777-7777-7777-777777777777"), "Italian pasta dishes with different sauces.", "Pasta" },
                    { new Guid("88888888-8888-8888-8888-888888888888"), "Thin crust and deep dish pizzas.", "Pizza" },
                    { new Guid("99999999-9999-9999-9999-999999999999"), "Fresh and healthy salads.", "Salad" },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Warm soups for all seasons.", "Soup" }
                });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "FromDate", "Name", "ToDate" },
                values: new object[,]
                {
                    { new Guid("12121212-1212-1212-1212-121212121212"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Weekend Specials", new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("13131313-1313-1313-1313-131313131313"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vegan Menu", new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("14141414-1414-1414-1414-141414141414"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kids Menu", new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("15151515-1515-1515-1515-151515151515"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Happy Hour Menu", new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("16161616-1616-1616-1616-161616161616"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seasonal Menu", new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Breakfast Menu", new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lunch Menu", new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dinner Menu", new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Drinks Menu", new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dessert Menu", new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("17171717-1717-1717-1717-171717171717"), new Guid("11111111-1111-1111-1111-111111111111"), "Strong black coffee", "Espresso", 2.5m },
                    { new Guid("18181818-1818-1818-1818-181818181818"), new Guid("11111111-1111-1111-1111-111111111111"), "Coffee with steamed milk", "Latte", 3m },
                    { new Guid("19191919-1919-1919-1919-191919191919"), new Guid("22222222-2222-2222-2222-222222222222"), "Refreshing green tea", "Green Tea", 1.8m },
                    { new Guid("20202020-2020-2020-2020-202020202020"), new Guid("33333333-3333-3333-3333-333333333333"), "Fresh squeezed orange juice", "Orange Juice", 2.2m },
                    { new Guid("21212121-2121-2121-2121-212121212121"), new Guid("44444444-4444-4444-4444-444444444444"), "Mixed berry smoothie", "Berry Smoothie", 3.5m },
                    { new Guid("22222222-3333-4444-5555-666666666666"), new Guid("55555555-5555-5555-5555-555555555555"), "Buttery French pastry", "Croissant", 1.5m },
                    { new Guid("23232323-2323-2323-2323-232323232323"), new Guid("66666666-6666-6666-6666-666666666666"), "Triple-decker sandwich", "Club Sandwich", 4.5m },
                    { new Guid("24242424-2424-2424-2424-242424242424"), new Guid("77777777-7777-7777-7777-777777777777"), "Classic Italian pasta", "Spaghetti Bolognese", 6.5m },
                    { new Guid("25252525-2525-2525-2525-252525252525"), new Guid("88888888-8888-8888-8888-888888888888"), "Cheese and tomato pizza", "Margherita Pizza", 7m },
                    { new Guid("26262626-2626-2626-2626-262626262626"), new Guid("99999999-9999-9999-9999-999999999999"), "Salad with romaine lettuce and dressing", "Caesar Salad", 5m }
                });

            migrationBuilder.InsertData(
                table: "ProductInMenu",
                columns: new[] { "Id", "MenuId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("27272727-2727-2727-2727-272727272727"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("17171717-1717-1717-1717-171717171717"), 50 },
                    { new Guid("28282828-2828-2828-2828-282828282828"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("18181818-1818-1818-1818-181818181818"), 40 },
                    { new Guid("29292929-2929-2929-2929-292929292929"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new Guid("19191919-1919-1919-1919-191919191919"), 60 },
                    { new Guid("30303030-3030-3030-3030-303030303030"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new Guid("20202020-2020-2020-2020-202020202020"), 70 },
                    { new Guid("31313131-3131-3131-3131-313131313131"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new Guid("21212121-2121-2121-2121-212121212121"), 30 },
                    { new Guid("32323232-3232-3232-3232-323232323232"), new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new Guid("22222222-3333-4444-5555-666666666666"), 80 },
                    { new Guid("33333333-3333-4444-5555-666666666666"), new Guid("12121212-1212-1212-1212-121212121212"), new Guid("23232323-2323-2323-2323-232323232323"), 25 },
                    { new Guid("34343434-3434-3434-3434-343434343434"), new Guid("12121212-1212-1212-1212-121212121212"), new Guid("24242424-2424-2424-2424-242424242424"), 20 },
                    { new Guid("35353535-3535-3535-3535-353535353535"), new Guid("14141414-1414-1414-1414-141414141414"), new Guid("25252525-2525-2525-2525-252525252525"), 15 },
                    { new Guid("36363636-3636-3636-3636-363636363636"), new Guid("15151515-1515-1515-1515-151515151515"), new Guid("26262626-2626-2626-2626-262626262626"), 35 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: new Guid("13131313-1313-1313-1313-131313131313"));

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: new Guid("16161616-1616-1616-1616-161616161616"));

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"));

            migrationBuilder.DeleteData(
                table: "ProductInMenu",
                keyColumn: "Id",
                keyValue: new Guid("27272727-2727-2727-2727-272727272727"));

            migrationBuilder.DeleteData(
                table: "ProductInMenu",
                keyColumn: "Id",
                keyValue: new Guid("28282828-2828-2828-2828-282828282828"));

            migrationBuilder.DeleteData(
                table: "ProductInMenu",
                keyColumn: "Id",
                keyValue: new Guid("29292929-2929-2929-2929-292929292929"));

            migrationBuilder.DeleteData(
                table: "ProductInMenu",
                keyColumn: "Id",
                keyValue: new Guid("30303030-3030-3030-3030-303030303030"));

            migrationBuilder.DeleteData(
                table: "ProductInMenu",
                keyColumn: "Id",
                keyValue: new Guid("31313131-3131-3131-3131-313131313131"));

            migrationBuilder.DeleteData(
                table: "ProductInMenu",
                keyColumn: "Id",
                keyValue: new Guid("32323232-3232-3232-3232-323232323232"));

            migrationBuilder.DeleteData(
                table: "ProductInMenu",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-4444-5555-666666666666"));

            migrationBuilder.DeleteData(
                table: "ProductInMenu",
                keyColumn: "Id",
                keyValue: new Guid("34343434-3434-3434-3434-343434343434"));

            migrationBuilder.DeleteData(
                table: "ProductInMenu",
                keyColumn: "Id",
                keyValue: new Guid("35353535-3535-3535-3535-353535353535"));

            migrationBuilder.DeleteData(
                table: "ProductInMenu",
                keyColumn: "Id",
                keyValue: new Guid("36363636-3636-3636-3636-363636363636"));

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: new Guid("12121212-1212-1212-1212-121212121212"));

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: new Guid("14141414-1414-1414-1414-141414141414"));

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: new Guid("15151515-1515-1515-1515-151515151515"));

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"));

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("17171717-1717-1717-1717-171717171717"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("18181818-1818-1818-1818-181818181818"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("19191919-1919-1919-1919-191919191919"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("20202020-2020-2020-2020-202020202020"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("21212121-2121-2121-2121-212121212121"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("22222222-3333-4444-5555-666666666666"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("23232323-2323-2323-2323-232323232323"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("24242424-2424-2424-2424-242424242424"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("25252525-2525-2525-2525-252525252525"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("26262626-2626-2626-2626-262626262626"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"));
        }
    }
}
