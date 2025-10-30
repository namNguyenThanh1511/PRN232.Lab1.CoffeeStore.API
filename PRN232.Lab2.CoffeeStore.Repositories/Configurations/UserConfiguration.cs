using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRN232.Lab2.CoffeeStore.Repositories.Entities;

namespace PRN232.Lab2.CoffeeStore.Repositories.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Password).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(100);
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.PhoneNumber).HasMaxLength(15);
            builder.HasIndex(u => u.PhoneNumber).IsUnique();
            builder.Property(u => u.FullName).HasMaxLength(100);
            builder.Property(u => u.Role).IsRequired();
            builder.Property(u => u.Role).HasConversion<string>();
            builder.Property(u => u.CreatedAt);
            builder.Property(u => u.UpdatedAt);
            builder.HasMany(u => u.Orders)
                   .WithOne(o => o.Customer)
                   .HasForeignKey(o => o.CustomerId)
                   .OnDelete(DeleteBehavior.SetNull);// Khi User bị xóa, đặt CustomerId trong Order thành null
        }
    }
}
