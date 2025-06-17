using Microsoft.AspNetCore.Mvc;
using RevenueRecognitionSystem.Features.Contracts.Repositories;
using RevenueRecognitionSystem.Features.Contracts.Services;
using RevenueRecognitionSystem.Modules.Licence.DTOs.Requests;

namespace RevenueRecognitionSystem.Features.Contracts.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LicenseController : ControllerBase
{
    private readonly ILicenseService _licenceService;

    public LicenseController(ILicenseService licenceService)
    {
        _licenceService = licenceService;
    }
    
    
    [HttpPost("create-upfront")]
    public async Task<IActionResult> CreateUpfront([FromBody] CreateLicenceDto dto)
    {
        var licenceId = await _licenceService.CreateUpfrontLicenceAsync(dto);
        var licenceDto = await _licenceService.GetByIdAsync(licenceId);
        return CreatedAtAction(nameof(GetById), new { id = licenceId }, licenceDto);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var licence = await _licenceService.GetByIdAsync(id);
        if (licence == null)
            return NotFound();

        return Ok(licence);
    }

}