using Microsoft.EntityFrameworkCore;
using ShopWeb.Data;
using ShopWeb.Models;

namespace ShopWeb.Repositories;

/// <summary>
/// Репозиторий товара.
/// </summary>
public class ProductRepository
{
  /// <summary>
  /// Контекст базы данных.
  /// </summary>
  private readonly ShopDbContext _context;

  /// <summary>
  /// Добавить товар в базу данных.
  /// </summary>
  /// <param name="product">Сущность Product</param>
  /// <returns>Добавленный товар.</returns>
  public async Task<Product> AddProductAsync(Product product)
  {
    ProductEntity productEntity = new ProductEntity();
    productEntity.Name = product.Name;
    productEntity.Color = product.Color;
    productEntity.Category = product.Category;
    productEntity.Price = product.Price;
    productEntity.Size = product.Size;

    _context.Products.Add(productEntity);
    await _context.SaveChangesAsync();
    return product;
  }

  /// <summary>
  /// Получить товар из базы данных по id.
  /// </summary>
  /// <param name="id">Id товара.</param>
  /// <returns>Сущность ProductEntity.</returns>
  public async Task<ProductEntity> GetProductByIdAsync(Guid id)
  {
    return await _context.Products.FindAsync(id);
  }

  /// <summary>
  /// Получить коллекцию List всех товаров.
  /// </summary>
  /// <returns>Коллекция товаров</returns>
  public async Task<List<ProductEntity>> GetAllProductsAsync()
  {
    return await _context.Products.ToListAsync();
  }

  /// <summary>
  /// Получить количество товаров по категории и размерую
  /// </summary>
  /// <param name="category">Категория товара.</param>
  /// <param name="size">Размер.</param>
  /// <returns>Количество товара.</returns>
  public async Task<int> GetStockByCategoryAndSizeAsync(String category, String size)
  {
    var products = await _context.Products
      .Where(p => p.Category.ToLower() == category.ToLower() && p.Size.ToLower() == size.ToLower())
      .ToListAsync();

    return products.Count;
  }

  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="context">Контекст базы данных.</param>
  public ProductRepository(ShopDbContext context)
  {
    this._context = context;
  }
}
 