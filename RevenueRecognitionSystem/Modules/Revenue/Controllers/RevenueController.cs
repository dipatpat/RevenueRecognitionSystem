using Microsoft.AspNetCore.Mvc;
using RevenueRecognitionSystem.Features.Revenue.Services;
using RevenueRecognitionSystem.Modules.Revenue.DTOs.Requests;
using RevenueRecognitionSystem.Modules.Revenue.DTOs.Responses;

namespace RevenueRecognitionSystem.Features.Revenue.Controllers;

[ApiController]
[Route("api/revenue")]
public class RevenueController : ControllerBase
{
    private readonly IRevenueService _revenueService;

    public RevenueController(IRevenueService revenueService)
    {
        _revenueService = revenueService;
    }

    [HttpPost("current")]
    public async Task<ActionResult<RevenueResponseDto>> GetCurrentRevenue([FromBody] RevenueRequestDto dto)
    {
        return Ok(await _revenueService.GetCurrentRevenueAsync(dto));
    }

    [HttpPost("predicted")]
    public async Task<ActionResult<RevenueResponseDto>> GetPredictedRevenue([FromBody] RevenueRequestDto dto)
    {
        return Ok(await _revenueService.GetPredictedRevenueAsync(dto));
    }
}
