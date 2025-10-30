using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRN232.Lab2.CoffeeStore.Repositories.Entities;

namespace PRN232.Lab2.CoffeeStore.Repositories.Configurations
{
    public class ProductInMenuConfiguration : IEntityTypeConfiguration<ProductInMenu>

    {
        public void Configure(EntityTypeBuilder<ProductInMenu> builder)
        {
            builder.ToTable("ProductInMenu");
            builder.HasKey(pm => pm.Id);
            builder.Property(pm => pm.ProductId);
            builder.Property(pm => pm.MenuId);
            builder.Property(pm => pm.Quantity)
                .IsRequired();
            builder.HasOne(pm => pm.Product)
                .WithMany(pm => pm.ProductInMenus)
                .HasForeignKey(pm => pm.ProductId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(pm => pm.Menu)
                .WithMany(m => m.ProductInMenus)
                .HasForeignKey(pm => pm.MenuId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
