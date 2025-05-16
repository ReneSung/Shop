namespace FrontShop.Models;

public class User
{
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