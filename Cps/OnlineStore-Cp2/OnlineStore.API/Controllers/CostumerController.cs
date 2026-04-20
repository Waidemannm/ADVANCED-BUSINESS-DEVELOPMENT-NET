using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTO;
using OnlineStore.Application.Interfaces;

namespace OnlineSore.Domain.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CostumerController : ControllerBase
{
    private readonly ICostumerService _costumerService;

    public CostumerController(ICostumerService costumerService)
    {
        _costumerService = costumerService;
    }

    
    // CostumerController
    /// <summary>
    /// Remove um cliente pelo Id.
    /// </summary>
    [HttpDelete("{id:guid}")]
    public IActionResult DeleteCostumer(Guid id)
    {
        try
        {
            _costumerService.DeleteCostumer(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    /// <summary>
    /// Cria um novo cliente.
    /// </summary>
    [HttpPost]
    public IActionResult CreateCostumer([FromBody] CreateCostumerRequest request)
    {
        try
        {
            var costumer = _costumerService.CreateCostumer(request.ToDomain());
            return CreatedAtAction(nameof(GetById), new { id = costumer.Id }, CostumerResponse.FromDomain(costumer));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Atualiza os dados de um cliente.
    /// </summary>
    [HttpPut("{id:guid}")]
    public IActionResult UpdateCostumer(Guid id, [FromBody] UpdateCostumerRequest request)
    {
        try
        {
            var costumer = _costumerService.UpdateCostumer(id, request.Name, request.SetBirthDate, request.Email);
            return Ok(CostumerResponse.FromDomain(costumer));
        }
        catch (InvalidOperationException ex)
        {
            return ex.Message.Contains("não encontrado")
                ? NotFound(new { message = ex.Message })
                : BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Busca um cliente pelo e-mail.
    /// </summary>
    [HttpGet("email/{email}")]
    public IActionResult GetCostumerByEmail(string email)
    {
        var costumer = _costumerService.GetCostumerByEmail(email);
        if (costumer == null)
            return NotFound();

        return Ok(CostumerResponse.FromDomain(costumer));
    }

    /// <summary>
    /// Retorna todos os clientes.
    /// </summary>
    [HttpGet]
    public IActionResult GetAll()
    {
        var costumers = _costumerService.GetAll();
        return Ok(costumers.Select(CostumerResponse.FromDomain));
    }

    /// <summary>
    /// Busca um cliente pelo Id.
    /// </summary>
    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var costumer = _costumerService.GetById(id);
        if (costumer == null)
            return NotFound();

        return Ok(CostumerResponse.FromDomain(costumer));
    }
}