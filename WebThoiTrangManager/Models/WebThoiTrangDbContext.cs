using Microsoft.EntityFrameworkCore;
using WebThoiTrangManager.Models.Configuration;
using WebThoiTrang.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace WebThoiTrangManager.Models
{
    public class WebThoiTrangDbContext : DbContext
    {
        public WebThoiTrangDbContext(DbContextOptions<WebThoiTrangDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { set; get; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfigration());
            modelBuilder.ApplyConfiguration(new OrderConfigration());

            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("AppUserTokens").HasKey(x => x.UserId);
        }

        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<WebThoiTrangDbContext>
        {

            public WebThoiTrangDbContext CreateDbContext(string[] args)
            {
                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();
                var builder = new DbContextOptionsBuilder<WebThoiTrangDbContext>();
                var connectionString = configuration.GetConnectionString("WebThoiTrangManagerDb");
                builder.UseSqlServer(connectionString);
                return new WebThoiTrangDbContext(builder.Options);
            }
        }
    }
}
