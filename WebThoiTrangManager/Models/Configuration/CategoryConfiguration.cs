using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebThoiTrang.Models;

namespace WebThoiTrangManager.Models.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");
            builder.HasKey(p => p.CategoryID);
            builder.Property(p => p.CategoryID).UseIdentityColumn().HasColumnName("CategoryID");
            builder.Property(p => p.Category_Name).IsRequired();
            builder.Property(p => p.CategoryID).IsRequired();
        }
    }

}
