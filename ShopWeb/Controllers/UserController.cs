using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopWeb.Data;
using ShopWeb.Models;
using ShopWeb.Repositories;

namespace ShopWeb.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
  private readonly UserRepository _userRepository;
  
  /// <summary>
  /// Добавить пользователя в базу.
  /// </summary>
  /// <param name="user">Пользователь.</param>
  /// <returns>Результат добавления.</returns>
  [HttpPost]
  public async Task<IActionResult> AddUser([FromBody] User user)
  {
    if (string.IsNullOrWhiteSpace(user.FirstName) ||
        string.IsNullOrWhiteSpace(user.LastName) ||
        string.IsNullOrWhiteSpace(user.Login) ||
        string.IsNullOrWhiteSpace(user.Password))
    {
      return BadRequest("Все поля (FirstName, LastName, Login, Password) обязательны.");
    }

    try
    {
      await _userRepository.AddUserAsync(user);
      return Ok("Пользователь успешно добавлен.");
    }
    catch (DbUpdateException)
    {
      return Conflict("Пользователь с таким логином уже существует.");
    }
  }
  
  /// <summary>
  /// Пополнить баланс пользователя.
  /// </summary>
  /// <param name="userBalance"></param>
  /// <returns>Результат добавления.</returns>
  [HttpPost("topup")]
  public async Task<IActionResult> TopUpBalance([FromBody] UserBalanceTopUp userBalance)
  {
    try
    {
      await _userRepository.TopUpBalanceAsync(userBalance.UserId, userBalance.Amount);
      return Ok("Транзакция завершена.");
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }

  /// <summary>
  /// Получить пользователя по id.
  /// </summary>
  /// <param name="id">Id пользователя.</param>
  /// <returns>Пользователь.</returns>
  [HttpGet("{id}")]
  public async Task<IActionResult> GetUserById(long id)
  {
    var user = await _userRepository.GetuserByIdAsync(id);

    if (user == null)
    {
      return NotFound($"{id} not found");
    }
    
    return Ok(user);
  }
  
  public UserController(UserRepository userRepository)
  {
    _userRepository = userRepository;
  }
}