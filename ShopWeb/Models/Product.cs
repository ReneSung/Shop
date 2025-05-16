namespace ShopWeb.Models;

/// <summary>
/// Товар.
/// </summary>
public class Product
{
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
}