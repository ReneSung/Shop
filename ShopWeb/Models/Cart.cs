namespace ShopWeb.Models;

/// <summary>
/// Корзина.
/// </summary>
public class Cart
{
  /// <summary>
  /// Id Пользователя.
  /// </summary>
  public long UserId { get; set; }
  
  /// <summary>
  /// Id товара.
  /// </summary>
  public Guid ProductId { get; set; }
}