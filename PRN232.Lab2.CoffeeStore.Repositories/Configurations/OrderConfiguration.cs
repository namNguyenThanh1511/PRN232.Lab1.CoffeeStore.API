using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRN232.Lab2.CoffeeStore.Repositories.Entities;

namespace PRN232.Lab2.CoffeeStore.Repositories.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>

    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.OrderDate).IsRequired();
            builder.Property(o => o.TotalAmount).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(o => o.Status).IsRequired();
            builder.Property(o => o.CreatedDate).IsRequired();
            // Quan hệ N - 1 với User (Customer)
            builder.HasOne(o => o.Customer)
                   .WithMany(u => u.Orders)
                   .HasForeignKey(o => o.CustomerId)
                   .OnDelete(DeleteBehavior.SetNull); // Nếu User bị xóa, đặt CustomerId thành null
            // Quan hệ 1 - 1 với Payment
            builder.HasOne(o => o.Payment)
                   .WithOne(p => p.Order)
                   .HasForeignKey<Order>(o => o.paymentId)
                   .OnDelete(DeleteBehavior.SetNull); // Nếu Payment bị xóa, đặt paymentId thành null
            // Quan hệ 1 - N với OrderDetail (OrderItems)
            builder.HasMany(o => o.OrderItems)
                   .WithOne(od => od.Order)
                   .HasForeignKey(od => od.OrderId)
                   .OnDelete(DeleteBehavior.Cascade); // Nếu Order bị xóa, xóa tất cả OrderDetails liên quan
        }
    }
}
