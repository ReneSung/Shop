using Microsoft.EntityFrameworkCore;
using ShopWeb.Data;

namespace ShopWeb.Repositories;

/// <summary>
/// Репозиторий корзины.
/// </summary>
public class CartRepository
{
  /// <summary>
  /// Контекст базы данных.
  /// </summary>
  private readonly ShopDbContext _context;

  /// <summary>
  /// Добавить товар в корзину.
  /// </summary>
  /// <param name="userId">Id пользователя.</param>
  /// <param name="productId">Id товара.</param>
  /// <exception cref="ArgumentException">Передан некорректный id.</exception>
  public async Task AddToCartAsync(long userId, Guid productId)
  {
    var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
    var productExists = await _context.Products.AnyAsync(p => p.Id == productId);
    
    if (!userExists)
      throw new ArgumentException("Некорректный id пользователя");
      
    if (!productExists)
      throw new ArgumentException("Некорректный id товара");

    var cartItem = new CartEntity
    {
      UserId = userId,
      ProductId = productId
    };

    _context.Carts.Add(cartItem);
    await _context.SaveChangesAsync();
  }
  
  /// <summary>
  /// Удалить товар из корзины.
  /// </summary>
  /// <param name="userId">Id пользователя.</param>
  /// <param name="productId">Id товара.</param>
  /// <exception cref="InvalidOperationException">Товар в корзине не найден.</exception>
  public async Task RemoveFromCartAsync(long userId, Guid productId)
  {
    var cartItem = await _context.Carts
      .FirstOrDefaultAsync(c =>
        c.UserId == userId
        && c.ProductId == productId);

    if (cartItem == null)
      throw new InvalidOperationException("Товар не найден.");

    _context.Carts.Remove(cartItem);
    await _context.SaveChangesAsync();
  }
  
  /// <summary>
  /// Получить товары пользователя в корзине.
  /// </summary>
  /// <param name="userId">Id пользователя.</param>
  /// <returns>Список товаров.</returns>
  public async Task<List<CartEntity>> GetUserCartAsync(long userId)
  {
    return await _context.Carts
      .Include(c => c.Product)
      .Where(c => c.UserId == userId)
      .ToListAsync();
  }
  
  /// <summary>
  /// Получить сумму стоимости товаров в корзине.
  /// </summary>
  /// <param name="userId">Id пользователя.</param>
  /// <returns>Сумма.</returns>
  public async Task<decimal> GetSumCartAsync(long userId)
  {
    var carts = await _context.Carts
      .Where(c => c.UserId == userId)
      .Include(c => c.Product)
      .ToListAsync();

    return carts.Sum(c => (decimal?)c.Product?.Price ?? 0);
  }
  
  /// <summary>
  /// Совершить покупку товаров в корзине.
  /// </summary>
  /// <param name="userId">Id пользователя.</param>
  /// <exception cref="InvalidOperationException">Пустая корзина.</exception>
  public async Task PurchaseProductsInCartAsync(long userId)
  {
    var cartItems = await _context.Carts
      .Where(c => c.UserId == userId)
      .Include(c => c.Product)
      .ToListAsync();

    if (!cartItems.Any())
      throw new InvalidOperationException("Корзина пуста.");

    decimal total = cartItems.Sum(c => (decimal?)c.Product?.Price ?? 0);

    var user = await _context.Users.FindAsync(userId);
    if (user == null)
      throw new InvalidOperationException("Пользователь не найден.");

    if (user.Balance < total)
      throw new InvalidOperationException("Недостаточно средств.");

    user.Balance -= total;

    _context.Carts.RemoveRange(cartItems);

    await _context.SaveChangesAsync();
  }

  public async Task<int> GetProductsAmountInUserCart(long userId)
  {
    var products = await _context.Carts
      .Where(c => c.UserId == userId)
      .ToListAsync();
    
    return products.Count;
  }
  
  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="context">Контекст базы данных.</param>
  public CartRepository(ShopDbContext context)
  {
    this._context = context;
  }
}