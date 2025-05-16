namespace ShopWeb.Data;

/// <summary>
/// Сущность корзина для базы данных
/// </summary>
public class CartEntity
{
  /// <summary>
  /// Id товара в корзине.
  /// </summary>
  public long Id { get; set; }
  
  /// <summary>
  /// Id пользователя.
  /// </summary>
  public long UserId { get; set; }
  
  /// <summary>
  /// Id товара.
  /// </summary>
  public Guid ProductId { get; set; }
  
  /// <summary>
  /// Товар.
  /// </summary>
  public ProductEntity? Product { get; set; }
  
  /// <summary>
  /// Пользователь
  /// </summary>
  public UserEntity? User { get; set; }
}