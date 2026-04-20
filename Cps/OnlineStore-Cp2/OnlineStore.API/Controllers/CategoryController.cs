using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTO;
using OnlineStore.Application.Interfaces;

namespace OnlineSore.Domain.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    

    /// <summary>
    /// Cria uma nova categoria.
    /// </summary>
    [HttpPost]
    public IActionResult CreateCategory([FromBody] CreateCategoryRequest request)
    {
        try
        {
            var category = _categoryService.CreateCategory(request.ToDomain());
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, CategoryResponse.FromDomain(category));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // CategoryController
    /// <summary>
    /// Remove uma categoria pelo Id.
    /// </summary>
    [HttpDelete("{id:guid}")]
    public IActionResult DeleteCategory(Guid id)
    {
        try
        {
            _categoryService.DeleteCategory(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    
    /// <summary>
    /// Atualiza os dados de uma categoria.
    /// </summary>
    [HttpPut("{id:guid}")]
    public IActionResult UpdateCategory(Guid id, [FromBody] UpdateCategoryRequest request)
    {
        try
        {
            var category = _categoryService.UpdateCategory(id, request.Name, request.Description);
            return Ok(CategoryResponse.FromDomain(category));
        }
        catch (InvalidOperationException ex)
        {
            return ex.Message.Contains("não encontrada")
                ? NotFound(new { message = ex.Message })
                : BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Busca uma categoria pelo nome.
    /// </summary>
    [HttpGet("name/{name}")]
    public IActionResult GetCategoryByName(string name)
    {
        var category = _categoryService.GetCategoryByName(name);
        if (category == null)
            return NotFound();

        return Ok(CategoryResponse.FromDomain(category));
    }

    /// <summary>
    /// Retorna todas as categorias.
    /// </summary>
    [HttpGet]
    public IActionResult GetAll()
    {
        var categories = _categoryService.GetAll();
        return Ok(categories.Select(CategoryResponse.FromDomain));
    }

    /// <summary>
    /// Busca uma categoria pelo Id.
    /// </summary>
    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var category = _categoryService.GetById(id);
        if (category == null)
            return NotFound();

        return Ok(CategoryResponse.FromDomain(category));
    }
}