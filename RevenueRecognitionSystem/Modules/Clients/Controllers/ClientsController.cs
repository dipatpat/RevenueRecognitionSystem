using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RevenueRecognitionSystem.Exceptions;
using RevenueRecognitionSystem.Features.Clients.Models;
using RevenueRecognitionSystem.Features.Clients.Services;
using RevenueRecognitionSystem.Modules.Clients.DTOs.Requests;

namespace RevenueRecognitionSystem.Features.Clients.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var clients = await _clientService.GetAllClientsAsync();
        return Ok(clients);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var client = await _clientService.GetClientByIdAsync(id); // throws if not found
        return Ok(client);
    }

    [HttpPost("individual")]
    public async Task<IActionResult> CreateIndividual(CreateIndividualClientDto dto)
    {
        var client = new IndividualClient(dto.Pesel)
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Address = dto.Address,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber
        };

        await _clientService.AddClientAsync(client);
        return CreatedAtAction(nameof(GetById), new { id = client.Id }, client);
    }
    
    [HttpPost("company")]
    public async Task<IActionResult> CreateCompany(CreateCompanyClientDto dto)
    {
        var client = new CompanyClient
        {
            Name = dto.Name,
            KRS = dto.KRS,
            Address = dto.Address,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber
        };

        await _clientService.AddClientAsync(client);
        return CreatedAtAction(nameof(GetById), new { id = client.Id }, client);
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("individual/{id}")]
    public async Task<IActionResult> UpdateIndividual(int id, [FromBody] UpdateIndividualClientDto dto)
    {
        var client = new IndividualClient(pesel: null!) { Id = id };

        var updatedFields = new Dictionary<string, object>();

        if (dto.FirstName != null)
        {
            client.FirstName = dto.FirstName;
            updatedFields[nameof(dto.FirstName)] = dto.FirstName;
        }

        if (dto.LastName != null)
        {
            client.LastName = dto.LastName;
            updatedFields[nameof(dto.LastName)] = dto.LastName;
        }

        if (dto.Address != null)
        {
            client.Address = dto.Address;
            updatedFields[nameof(dto.Address)] = dto.Address;
        }

        if (dto.Email != null)
        {
            client.Email = dto.Email;
            updatedFields[nameof(dto.Email)] = dto.Email;
        }

        if (dto.PhoneNumber != null)
        {
            client.PhoneNumber = dto.PhoneNumber;
            updatedFields[nameof(dto.PhoneNumber)] = dto.PhoneNumber;
        }

        if (updatedFields.Count == 0)
            throw new BadRequestException("No fields provided to update."); // now handled by middleware

        await _clientService.UpdateClientAsync(client);

        return Ok(new
        {
            Id = id,
            Updated = updatedFields
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("company/{id}")]
    public async Task<IActionResult> UpdateCompany(int id, UpdateCompanyClientDto dto)
    {
        var client = new CompanyClient { Id = id };

        var updatedFields = new Dictionary<string, object>();

        if (dto.Name != null)
        {
            client.Name = dto.Name;
            updatedFields[nameof(dto.Name)] = dto.Name;
        }

        if (dto.Address != null)
        {
            client.Address = dto.Address;
            updatedFields[nameof(dto.Address)] = dto.Address;
        }

        if (dto.Email != null)
        {
            client.Email = dto.Email;
            updatedFields[nameof(dto.Email)] = dto.Email;
        }

        if (dto.PhoneNumber != null)
        {
            client.PhoneNumber = dto.PhoneNumber;
            updatedFields[nameof(dto.PhoneNumber)] = dto.PhoneNumber;
        }

        if (updatedFields.Count == 0)
            throw new BadRequestException("No fields provided to update."); 

        await _clientService.UpdateClientAsync(client);

        return Ok(new
        {
            Id = id,
            Updated = updatedFields
        });
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _clientService.SoftDeleteClientAsync(id); 
        return NoContent();
    }
}
