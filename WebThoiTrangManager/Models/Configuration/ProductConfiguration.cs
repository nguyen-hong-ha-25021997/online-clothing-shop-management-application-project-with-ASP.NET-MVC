using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebThoiTrang.Models;

namespace WebThoiTrangManager.Models.Configuration
{
    class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(p => p.ProductID);
            builder.Property(p => p.ProductID).UseIdentityColumn().HasColumnName("ProductID");
            builder.Property(p => p.Product_Name).IsRequired();
            builder.Property(p => p.Product_Show).IsRequired();
            builder.Property(p => p.Product_Price).IsRequired();
            builder.Property(p => p.Product_Price).IsRequired();
            builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.Category_Id);
        }
    }
}
