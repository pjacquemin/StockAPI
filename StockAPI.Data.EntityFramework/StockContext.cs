using Microsoft.EntityFrameworkCore;
using StockAPI.Models;

namespace StockAPI.Data.EntityFramework
{
    public class StockContext : DbContext
    {
        public StockContext(DbContextOptions<StockContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<ProductStock> ProductStocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product").HasMany(p => p.ProductStocks);
            modelBuilder.Entity<Stock>().ToTable("Stock").HasMany(s => s.ProductStocks); ;
            modelBuilder.Entity<ProductStock>().ToTable("ProductStock").HasKey(ps => new { ps.ProductId, ps.StockId });
        }
    }
}