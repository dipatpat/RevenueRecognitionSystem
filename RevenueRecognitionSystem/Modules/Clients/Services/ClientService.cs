using MovieRentalApp.Exceptions;
using RevenueRecognitionSystem.Features.Clients.Models;
using RevenueRecognitionSystem.Features.Clients.Repositories;

namespace RevenueRecognitionSystem.Features.Clients.Services;

public class ClientService
{
    private readonly IClientRepository _clientRepository;
    public async Task SoftDeleteClientAsync(int id)
    {
        var client = await _clientRepository.GetByIdAsync(id);

        if (client is null)
            throw new NotFoundException("Client not found");

        if (client is CompanyClient)
            throw new ConflictException("Company clients cannot be deleted.");

        if (client is IndividualClient individual)
        {
            individual.SoftDelete();
            await _clientRepository.UpdateAsync(individual);
        }
    }
}