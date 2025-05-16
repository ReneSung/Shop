namespace ShopWeb.Data;

/// <summary>
/// Сущность пользователя для базы данных.
/// </summary>
public class UserEntity
{
  /// <summary>
  /// Id.
  /// </summary>
  public long Id { get; set; }
  
  /// <summary>
  /// Имя.
  /// </summary>
  public string FirstName { get; set; }
  
  /// <summary>
  /// Фамилия.
  /// </summary>
  public string LastName { get; set; }

  /// <summary>
  /// Логин.
  /// </summary>
  public string Login { get; set; }
  
  /// <summary>
  /// Пароль.
  /// </summary>
  public string Password { get; set; }
  
  /// <summary>
  /// Баланс на аккаунте.
  /// </summary>
  public decimal Balance { get; set; }
  
  /// <summary>
  /// Корзина для связи в базе.
  /// </summary>
  public List<CartEntity> Carts { get; set; } = new List<CartEntity>();
}