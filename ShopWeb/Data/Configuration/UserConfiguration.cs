using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShopWeb.Data.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
  public void Configure(EntityTypeBuilder<UserEntity> builder)
  {
    builder.ToTable("users");
    
    builder.HasKey(u => u.Id);
    
    builder.Property(u => u.Id)
      .HasColumnName("id")
      .ValueGeneratedOnAdd();
    
    builder.Property(u => u.FirstName)
      .HasColumnName("first_name")
      .HasColumnType("TEXT")
      .IsRequired();
    
    builder.Property(u => u.LastName)
      .HasColumnName("last_name")
      .HasColumnType("TEXT")
      .IsRequired();

    builder.Property(u => u.Login)
      .HasColumnName("login")
      .HasColumnType("TEXT")
      .IsRequired();
    
    builder.HasIndex(u => u.Login)
      .IsUnique();
    
    builder.Property(u => u.Password)
      .HasColumnName("password")
      .HasColumnType("TEXT")
      .IsRequired();
    
    builder.Property(u => u.Balance)
      .HasColumnName("balance")
      .HasColumnType("REAL")
      .HasDefaultValue(0)
      .IsRequired();
    
    builder.HasMany(u => u.Carts)
      .WithOne(c => c.User)
      .HasForeignKey(c => c.UserId);
  }
}