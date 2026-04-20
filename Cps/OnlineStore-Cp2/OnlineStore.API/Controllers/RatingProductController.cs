using Microsoft.AspNetCore.Mvc;
using OnlineSore.Domain.Enum;
using OnlineStore.Application.DTO;
using OnlineStore.Application.Interfaces;

namespace OnlineSore.Domain.Controllers;

[Route("api/[controller]")]
[ApiController] 
public class RatingProductController:ControllerBase
{
    private readonly IRatingProductService _ratingProductService;
    
    public RatingProductController(IRatingProductService ratingProductService)
    {
        _ratingProductService = ratingProductService;
    }
    
    // RatingProductController
    /// <summary>
    /// Remove uma avaliação pelo Id do cliente e Id do produto.
    /// </summary>
    [HttpDelete("{idCostumer:guid}/{idProduct:guid}")]
    public IActionResult DeleteRatingProduct(Guid idCostumer, Guid idProduct)
    {
        try
        {
            _ratingProductService.DeleteRatingProduct(idCostumer, idProduct);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    /// <summary>
    /// Cria uma nova avaliação.
    /// </summary>
    [HttpPost]
    public IActionResult Create([FromBody] CreateRatingProductRequest request)
    {
        try
        {
            var rp = _ratingProductService.CreateRatingProduct(request.ToDomain());
            return CreatedAtAction(nameof(GetById), new { idCostumer = rp.IdCostumer, idProduct = rp.IdProduct}, RatingProductResponse.FromDomain(rp));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    /// <summary>
    /// Busca um avaliação pela score.
    /// </summary>
    [HttpGet("score/{scoreEnum}")]
    public IActionResult GetRatingProductByScore(ScoreEnum scoreEnum)
    {
        var user = _ratingProductService.GetRatingProductByScore(scoreEnum);
        if (user == null)
            return NotFound();

        return Ok(RatingProductResponse.FromDomain(user));
    }
    
    
    /// <summary>
    /// Retorna todos as avaliações.
    /// </summary>
    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _ratingProductService.GetAll();
        return Ok(users.Select(RatingProductResponse.FromDomain));
    }
    
    /// <summary>
    /// Busca uma avaliação pelo Id.
    /// </summary>
    [HttpGet("{idCostumer:guid}/{idProduct:guid}")]
    public IActionResult GetById(Guid idCostumer, Guid idProduct)
    {
        var user = _ratingProductService.GetById(idCostumer, idProduct);
        if (user == null)
            return NotFound();

        return Ok(RatingProductResponse.FromDomain(user));
    }
}