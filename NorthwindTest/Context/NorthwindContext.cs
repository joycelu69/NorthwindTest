using Microsoft.EntityFrameworkCore;
using NorthwindTest.DbSet;

namespace NorthwindTest.Context
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext(DbContextOptions<NorthwindContext> options) : base(options)
        {
        }

        public DbSet<Customers> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customers>(entity => {
                entity.HasKey(k => k.CustomerID);
            });
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(k => k.ProductID);
                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
