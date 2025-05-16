using Microsoft.AspNetCore.Mvc;
using ShopWeb.Data;
using ShopWeb.Models;
using ShopWeb.Repositories;

namespace ShopWeb.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : Controller
{
  private readonly CartRepository _cartRepository;
  
  /// <summary>
  /// Добавить товар в корзину.
  /// </summary>
  /// <param name="cart">Корзина.</param>
  /// <returns>Результат выполнения.</returns>
  [HttpPost("add")]
  public async Task<IActionResult> AddToCart([FromBody] Cart cart)
  {
    try
    {
      await _cartRepository.AddToCartAsync(cart.UserId, cart.ProductId);
      return Ok("Товар добавлен в корзину.");
    }
    catch (InvalidOperationException ex)
    {
      return NotFound(ex.Message);
    }
    catch (Exception)
    {
      return StatusCode(500, "Ошибка при добавлении.");
    }
  }
  
  /// <summary>
  /// Удалить товар из корзины
  /// </summary>
  /// <param name="cart"></param>
  /// <returns></returns>
  [HttpDelete("remove")]
  public async Task<IActionResult> RemoveFromCart([FromBody] Cart cart)
  {
    try
    {
      await _cartRepository.RemoveFromCartAsync(cart.UserId, cart.ProductId);
      return Ok("Товар удалён из корзины.");
    }
    catch (InvalidOperationException ex)
    {
      return NotFound(ex.Message);
    }
  }
  
  /// <summary>
  /// Получить корзину пользователя.
  /// </summary>
  /// <param name="userId">id пользователя.</param>
  /// <returns>Полуить список товаров в корзине пользователя.</returns>
  [HttpGet("user/{userId}")]
  public async Task<IActionResult> GetCartByUserId(long userId)
  {
    var cartItems = await _cartRepository.GetUserCartAsync(userId);
    return Ok(cartItems);
  }
  
  /// <summary>
  /// Получить Сумму товаров в корзине.
  /// </summary>
  /// <param name="userId"></param>
  /// <returns></returns>
  [HttpGet("user/{userId}/total")]
  public async Task<IActionResult> GetCartSum(long userId)
  {
    var total = await _cartRepository.GetSumCartAsync(userId);
    return Ok(new { Total = total });
  }

  /// <summary>
  /// Получить количество товара в корзине.
  /// </summary>
  /// <param name="userId">id пользователя.</param>
  /// <returns>Количество товара в корзине пользователя.</returns>
  [HttpGet("/{userId}/amount")]
  public async Task<IActionResult> GetCartAmount(long userId)
  {
    var cartAmount = await _cartRepository.GetProductsAmountInUserCart(userId);
    return Ok(cartAmount);
  }
  
  /// <summary>
  /// Совершить покупку.
  /// </summary>
  /// <param name="userId">Id пользователя</param>
  /// <returns>Результат покупки.</returns>
  [HttpPost("user/{userId}/purchase")]
  public async Task<IActionResult> PurchaseCart(long userId)
  {
    try
    {
      await _cartRepository.PurchaseProductsInCartAsync(userId);
      return Ok("Транзакция завершена успешно.");
    }
    catch (InvalidOperationException ex)
    {
      return BadRequest(ex.Message);
    }
  }
  
  public CartController(CartRepository cartRepository)
  {
    _cartRepository = cartRepository;
  }
}