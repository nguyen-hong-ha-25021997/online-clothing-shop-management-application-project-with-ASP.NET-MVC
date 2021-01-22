using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebThoiTrang.Models;

namespace WebThoiTrangManager.Models.Configuration
{
    class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetail");
            builder.HasKey(p => p.OrderDetailID);
            builder.Property(p => p.OrderDetailID).UseIdentityColumn().HasColumnName("OrderDetailID");
            builder.Property(p => p.Order_Id).IsRequired();
            builder.Property(p => p.Product_Id).IsRequired();
            builder.Property(p => p.OrderDetail_Quantity).IsRequired();
            builder.Property(p => p.OrderDetail_Amount).IsRequired();
            builder.HasOne(x => x.Product).WithMany(x => x.OrderDetails).HasForeignKey(x => x.Product_Id);
            builder.HasOne(x => x.Order).WithMany(x => x.OrderDetails).HasForeignKey(x => x.Order_Id);
        }
    }
}
