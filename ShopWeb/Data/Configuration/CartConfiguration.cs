using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShopWeb.Data.Configuration;

public class CartConfiguration : IEntityTypeConfiguration<CartEntity>
{
  public void Configure(EntityTypeBuilder<CartEntity> builder)
  {
    builder.ToTable("carts");
    
    builder.HasKey(c => c.Id);
    
    builder.Property(c => c.Id)
      .HasColumnName("id")
      .ValueGeneratedOnAdd();

    builder.Property(c => c.UserId)
      .HasColumnName("user_id")
      .IsRequired();
    
    builder.Property(c => c.ProductId)
      .HasColumnName("product_id")
      .IsRequired();

    builder.HasOne(c => c.User)
      .WithMany(u => u.Carts)
      .HasForeignKey(c => c.UserId);
    
    builder.HasOne(c => c.Product)
      .WithMany(p => p.Carts)
      .HasForeignKey(c => c.ProductId);
  }
}