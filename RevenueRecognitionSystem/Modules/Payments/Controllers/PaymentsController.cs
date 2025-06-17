using Microsoft.AspNetCore.Mvc;
using RevenueRecognitionSystem.Features.Payments.Services;
using RevenueRecognitionSystem.Modules.Payments.DTOs.Requests;

namespace RevenueRecognitionSystem.Features.Payments.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _paymentService;
    
    public PaymentsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }
    
    [HttpPost]
    public async Task<IActionResult> PayForLicence([FromBody] CreatePaymentDto dto)
    {
        await _paymentService.AddPaymentAsync(dto);
        return Ok(new { message = "Payment processed." });
    }

}