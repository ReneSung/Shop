namespace ShopWeb.Data;

/// <summary>
/// Сущность товар для базы данных.
/// </summary>
public class ProductEntity
{
  /// <summary>
  /// Id.
  /// </summary>
  public Guid Id { get; set; }
  
  /// <summary>
  /// Название.
  /// </summary>
  public string Name { get; set; }
  
  /// <summary>
  /// Цвет.
  /// </summary>
  public string Color { get; set; }
  
  /// <summary>
  /// Категория.
  /// </summary>
  public string Category { get; set; }
  
  /// <summary>
  /// Цена.
  /// </summary>
  public decimal Price { get; set; }
  
  /// <summary>
  /// Размер.
  /// </summary>
  public string Size { get; set; }
  
  /// <summary>
  /// Корзина.
  /// </summary>
  public List<CartEntity>? Carts { get; set; }
}