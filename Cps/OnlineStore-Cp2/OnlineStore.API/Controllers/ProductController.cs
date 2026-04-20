using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTO;
using OnlineStore.Application.Interfaces;

namespace OnlineSore.Domain.Controllers;

[Route("api/[controller]")]
[ApiController] 
public class ProductController : ControllerBase
{
    
    private readonly IProductService _productService;
    
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    /// <summary>
    /// Cria um novo produto.
    /// </summary>
    [HttpPost]
    public IActionResult CreateProduct([FromBody] CreateProductRequest request)
    {
        try
        {
            var p = _productService.CreateProduct(request.ToDomain());
            return CreatedAtAction(nameof(GetById), new { id = p.Id}, ProductResponse.FromDomain(p));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    /// <summary>
    /// Atualiza os dados de um produto.
    /// </summary>
    [HttpPut("{id:guid}")]
    public IActionResult UpdateProduct(Guid id, [FromBody] UpdateProductRequest request)
    {
        try
        {
            var product = _productService.UpdateProduct(id, request.Name, request.Description, request.Price, request.Stock);
            return Ok(ProductResponse.FromDomain(product));
        }
        catch (InvalidOperationException ex)
        {
            return ex.Message.Contains("não encontrado")
                ? NotFound(new { message = ex.Message })
                : BadRequest(new { message = ex.Message });
        }
    }
    
    
    /// <summary>
    /// Busca um produto.
    /// </summary>
    [HttpGet("name/{name}")]
    public IActionResult GetProductByName(string name)
    {
        var productByScore = _productService.GetProductByName(name);
        if (productByScore == null)
            return NotFound();

        return Ok(ProductResponse.FromDomain(productByScore));
    }
    
    /// <summary>
    /// Retorna todos os produtos.
    /// </summary>
    [HttpGet]
    public IActionResult GetAll()
    {
        var products = _productService.GetAll();
        return Ok(products.Select(ProductResponse.FromDomain));
    }
    
    /// <summary>
    /// Busca um produto pelo Id.
    /// </summary>
    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var product = _productService.GetById(id);
        if (product == null)
            return NotFound();

        return Ok(ProductResponse.FromDomain(product));
    }

    /// <summary>
    /// Remove um produto.
    /// </summary>
    [HttpDelete("{id:guid}")]
    public IActionResult DeleteProduct(Guid id)
    {
        try
        {
            _productService.DeleteProduct(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}