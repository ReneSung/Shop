namespace ShopWeb.Models;

/// <summary>
/// Пользователь для работы в системе.
/// </summary>
public class UserModel
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
  /// Баланс на аккаунте.
  /// </summary>
  public decimal Balance { get; set; }
}