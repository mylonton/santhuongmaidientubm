using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SanThuongMaiDienTu.Models;

namespace SanThuongMaiDienTu.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(18,2)");

            builder.Entity<Order>().Property(o => o.TotalAmount).HasColumnType("decimal(18,2)");

            builder.Entity<OrderItem>().Property(oi => oi.Price).HasColumnType("decimal(18,2)");

        }
    }
}