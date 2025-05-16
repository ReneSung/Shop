using ShopWeb.Data;
using ShopWeb.Models;

namespace ShopWeb.Repositories;

/// <summary>
/// Репозиторий пользователя.
/// </summary>
public class UserRepository
{
  /// <summary>
  /// Контекст базы данных
  /// </summary>
  private readonly ShopDbContext _context;
  
  /// <summary>
  /// Добавить пользователя в базу данных.
  /// </summary>
  /// <param name="user">Сущность User.</param>
  /// <returns>Добавленный User.</returns>
  public async Task<User> AddUserAsync(User user)
  {
    UserEntity userEntity = new UserEntity();
    userEntity.FirstName = user.FirstName;
    userEntity.LastName = user.LastName;
    userEntity.Login = user.Login;
    userEntity.Password = user.Password;
    
    _context.Users.Add(userEntity);
    await _context.SaveChangesAsync();
    return user;
  }
  
  /// <summary>
  /// Пополнить баланс пользователя.
  /// </summary>
  /// <param name="userId">Id пользователя.</param>
  /// <param name="amount">Сумма для пополнения.</param>
  /// <exception cref="ArgumentException">Некорректные данные.</exception>
  /// <exception cref="InvalidOperationException">Пользователь не найден.</exception>
  public async Task TopUpBalanceAsync(long userId, decimal amount)
  {
    if (amount <= 0)
      throw new ArgumentException("Некорректная сумма пополнения");

    var user = await _context.Users.FindAsync(userId);

    if (user == null)
      throw new InvalidOperationException("Пользователь не найден.");

    user.Balance += amount;

    await _context.SaveChangesAsync();
  }

  /// <summary>
  /// Получить пользователя по id.
  /// </summary>
  /// <param name="userId"></param>
  /// <returns></returns>
  public async Task<UserModel> GetuserByIdAsync(long userId)
  {
    var userEntity = await _context.Users.FindAsync(userId);
    var userModel = new UserModel();
    
    userModel.Id = userEntity.Id;
    userModel.FirstName = userEntity.FirstName;
    userModel.LastName = userEntity.LastName;
    userModel.Balance = userEntity.Balance;

    return userModel;
  }
  
  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="context">Контекст базы данных.</param>
  public UserRepository(ShopDbContext context)
  {
    this._context = context;
  }
}