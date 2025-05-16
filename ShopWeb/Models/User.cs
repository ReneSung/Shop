namespace ShopWeb.Models;

/// <summary>
/// Пользователь.
/// </summary>
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
  /// Логин.
  /// </summary>
  public string Login { get; set; }
  
  /// <summary>
  /// Пароль.
  /// </summary>
  public string Password { get; set; }
}