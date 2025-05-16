using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShopWeb.Data.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
{
  public void Configure(EntityTypeBuilder<ProductEntity> builder)
  {
    builder.ToTable("products");
    
    builder.HasKey(p => p.Id);
    
    builder.Property(p => p.Id)
      .HasColumnName("id")
      .ValueGeneratedOnAdd();
    
    builder.Property(p => p.Name)
      .HasColumnName("name")
      .HasColumnType("TEXT")
      .IsRequired();
    
    builder.Property(p => p.Color)
      .HasColumnName("color")
      .HasColumnType("TEXT")
      .IsRequired();
    
    builder.Property(p => p.Category)
      .HasColumnName("category")
      .HasColumnType("TEXT")
      .IsRequired();
    
    builder.Property(p => p.Price)
      .HasColumnName("price")
      .HasColumnType("REAL")
      .IsRequired();
    
    builder.Property(p => p.Size)
      .HasColumnName("size")
      .HasColumnType("TEXT")
      .IsRequired();

    builder.HasMany(p => p.Carts)
      .WithOne(c => c.Product)
      .HasForeignKey(c => c.ProductId);
  }
}