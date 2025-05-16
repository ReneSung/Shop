using Microsoft.EntityFrameworkCore;
using ShopWeb.Data.Configuration;

namespace ShopWeb.Data;

public class ShopDbContext : DbContext
{
  public DbSet<UserEntity> Users { get; set; }
  
  public DbSet<ProductEntity> Products { get;set; }
  
  public DbSet<CartEntity> Carts { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfiguration(new UserConfiguration());
    modelBuilder.ApplyConfiguration(new ProductConfiguration());
    modelBuilder.ApplyConfiguration(new CartConfiguration());
  }
  
  public ShopDbContext(DbContextOptions<ShopDbContext> options)
    : base(options) {}
}