using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTO;
using OnlineStore.Application.Interfaces;

namespace OnlineSore.Domain.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    // PaymentController
    /// <summary>
    /// Remove um pagamento pelo Id.
    /// </summary>
    [HttpDelete("{id:guid}")]
    public IActionResult DeletePayment(Guid id)
    {
        try
        {
            _paymentService.DeletePayment(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    /// <summary>
    /// Cria um novo pagamento.
    /// </summary>
    [HttpPost]
    public IActionResult CreatePayment([FromBody] CreatePaymentRequest request)
    {
        try
        {
            var payment = _paymentService.CreatePayment(request.ToDomain());
            return CreatedAtAction(nameof(GetById), new { id = payment.Id }, PaymentResponse.FromDomain(payment));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Atualiza os dados de um pagamento.
    /// </summary>
    [HttpPut("{id:guid}")]
    public IActionResult UpdatePayment(Guid id, [FromBody] UpdatePaymentRequest request)
    {
        try
        {
            var payment = _paymentService.UpdatePayment(id, request.Value);
            return Ok(PaymentResponse.FromDomain(payment));
        }
        catch (InvalidOperationException ex)
        {
            return ex.Message.Contains("não encontrado")
                ? NotFound(new { message = ex.Message })
                : BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Busca um pagamento pelo tipo.
    /// </summary>
    [HttpGet("type/{paymentEnum}")]
    public IActionResult GetPaymentByPaymentEnum(OnlineSore.Domain.Enum.PaymentEnum paymentEnum)
    {
        var payment = _paymentService.GetPaymentByPaymentEnum(paymentEnum);
        if (payment == null)
            return NotFound();

        return Ok(PaymentResponse.FromDomain(payment));
    }

    /// <summary>
    /// Retorna todos os pagamentos.
    /// </summary>
    [HttpGet]
    public IActionResult GetAll()
    {
        var payments = _paymentService.GetAll();
        return Ok(payments.Select(PaymentResponse.FromDomain));
    }

    /// <summary>
    /// Busca um pagamento pelo Id.
    /// </summary>
    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var payment = _paymentService.GetById(id);
        if (payment == null)
            return NotFound();

        return Ok(PaymentResponse.FromDomain(payment));
    }
}