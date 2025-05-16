using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopWeb.Data;
using ShopWeb.Models;
using ShopWeb.Repositories;

namespace ShopWeb.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : Controller
{
  private readonly ProductRepository _productRepository;

  public ProductController(ProductRepository productRepository)
  {
    this._productRepository = productRepository;
  }

  /// <summary>
  /// Добавление товара в корзину.
  /// </summary>
  /// <param name="product">id товара.</param>
  /// <returns>Результат выполнения.</returns>
  [HttpPost]
  public async Task<IActionResult> AddProductToUserCart([FromBody] Product product)
  {
    if (string.IsNullOrWhiteSpace(product.Name) ||
        string.IsNullOrWhiteSpace(product.Color) ||
        string.IsNullOrWhiteSpace(product.Category) ||
        product.Price == null)
    {
      return BadRequest("Некорректные данные");
    }

    try
    {
      await _productRepository.AddProductAsync(product);
      return Ok("Товар добавлен.");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  
  /// <summary>
  /// Получить товар по Id/
  /// </summary>
  /// <param name="id">Id товара.</param>
  /// <returns></returns>
  [HttpGet("{id}")]
  public async Task<IActionResult> GetProductById(Guid id)
  {
    var product = await _productRepository.GetProductByIdAsync(id);

    if (product == null)
    {
      return NotFound($"{id} not found");
    }

    return Ok(product);
  }

  /// <summary>
  /// Получить отчет по остаткам товара.
  /// </summary>
  /// <param name="category">Категория.</param>
  /// <param name="size">Размер.</param>
  /// <returns>Количество товара.</returns>
  [HttpGet("{category}/{size}")]
  public async Task<IActionResult> GetRemainingProductsReportAsync(String category, String size)
  {
    var amount = await _productRepository.GetStockByCategoryAndSizeAsync(category, size);

    var report = new
    {
      Category = category,
      Size = size.ToUpper(),
      Stock = amount
    };
    
    return Ok(report);
  }
  
  /// <summary>
  /// Получить список всех товаров.
  /// </summary>
  /// <returns></returns>
  [HttpGet]
  public async Task<IActionResult> GetAllProducts()
  {
    var products = await _productRepository.GetAllProductsAsync();
    
    return Ok(products);
  }
}