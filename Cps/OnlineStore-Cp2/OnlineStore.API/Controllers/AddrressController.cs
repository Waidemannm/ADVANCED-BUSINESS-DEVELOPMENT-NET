using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTO;
using OnlineStore.Application.Interfaces;

namespace OnlineSore.Domain.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddressController : ControllerBase
{
    private readonly IAddressService _addressService;

    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    /// <summary>
    /// Cria um novo endereço.
    /// </summary>
    [HttpPost]
    public IActionResult CreateAddress([FromBody] CreateAddressRequest request)
    {
        try
        {
            var address = _addressService.CreateAddress(request.ToDomain());
            return CreatedAtAction(nameof(GetById), new { id = address.Id }, AddressResponse.FromDomain(address));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // AddressController
    /// <summary>
    /// Remove um endereço pelo Id.
    /// </summary>
    [HttpDelete("{id:guid}")]
    public IActionResult DeleteAddress(Guid id)
    {
        try
        {
            _addressService.DeleteAddress(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    /// <summary>
    /// Atualiza os dados de um endereço.
    /// </summary>
    [HttpPut("{id:guid}")]
    public IActionResult UpdateAddress(Guid id, [FromBody] UpdateAddressRequest request)
    {
        try
        {
            var address = _addressService.UpdateAddress(id, request.street, request.city, request.state, request.postalCode, request.number, request.country);
            return Ok(AddressResponse.FromDomain(address));
        }
        catch (InvalidOperationException ex)
        {
            return ex.Message.Contains("não encontrado")
                ? NotFound(new { message = ex.Message })
                : BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Busca um endereço pelo CEP.
    /// </summary>
    [HttpGet("postalcode/{postalCode}")]
    public IActionResult GetAddressByPostalCode(string postalCode)
    {
        var address = _addressService.GetAddressByPostalCode(postalCode);
        if (address == null)
            return NotFound();

        return Ok(AddressResponse.FromDomain(address));
    }

    /// <summary>
    /// Retorna todos os endereços.
    /// </summary>
    [HttpGet]
    public IActionResult GetAll()
    {
        var addresses = _addressService.GetAll();
        return Ok(addresses.Select(AddressResponse.FromDomain));
    }

    /// <summary>
    /// Busca um endereço pelo Id.
    /// </summary>
    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var address = _addressService.GetById(id);
        if (address == null)
            return NotFound();

        return Ok(AddressResponse.FromDomain(address));
    }
}