using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebThoiTrang.Models;

namespace WebThoiTrangManager.Models.Configuration
{
    class OrderConfigration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.HasKey(p => p.OrderID);
            builder.Property(p => p.OrderID).UseIdentityColumn().HasColumnName("OrderID");
            builder.Property(p => p.Order_PurchaseTime).IsRequired().HasDefaultValueSql("GetDate()");
            builder.Property(p => p.Order_DeliveryTime).IsRequired();
            builder.Property(p => p.Order_DeliveryAddress).IsRequired();
            builder.Property(p => p.Order_Status).IsRequired();
            builder.Property(p => p.Order_Amount).IsRequired();
        }
    }
}
