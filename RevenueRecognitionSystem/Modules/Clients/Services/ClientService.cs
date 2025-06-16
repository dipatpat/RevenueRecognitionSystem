using RevenueRecognitionSystem.Exceptions;
using RevenueRecognitionSystem.Features.Clients.Models;
using RevenueRecognitionSystem.Features.Clients.Repositories;

namespace RevenueRecognitionSystem.Features.Clients.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<List<Client>> GetAllClientsAsync(CancellationToken cancellationToken = default)
    {
        return await _clientRepository.GetAllAsync(cancellationToken);
    }

    public async Task<Client?> GetClientByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _clientRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task AddClientAsync(Client client, CancellationToken cancellationToken = default)
    {
        await _clientRepository.AddAsync(client, cancellationToken);
    }

    public async Task UpdateClientAsync(Client client, CancellationToken cancellationToken = default)
    {
        var existing = await _clientRepository.GetByIdAsync(client.Id, cancellationToken);
        if (existing == null)
            throw new NotFoundException("Client not found");

        if (existing is IndividualClient existingIndividual && client is IndividualClient updated)
        {
            if (!string.IsNullOrEmpty(updated.Pesel) && updated.Pesel != existingIndividual.Pesel)
                throw new ConflictException("PESEL cannot be changed");

            if (updated.FirstName != null)
                existingIndividual.FirstName = updated.FirstName;

            if (updated.LastName != null)
                existingIndividual.LastName = updated.LastName;

            if (updated.Email != null)
                existingIndividual.Email = updated.Email;

            if (updated.Address != null)
                existingIndividual.Address = updated.Address;

            if (updated.PhoneNumber != null)
                existingIndividual.PhoneNumber = updated.PhoneNumber;
        }
        else if (existing is CompanyClient existingCompany && client is CompanyClient updatedCompany)
        {
            if (!string.IsNullOrEmpty(updatedCompany.KRS) && updatedCompany.KRS != existingCompany.KRS)
                throw new ConflictException("KRS cannot be changed");

            if (updatedCompany.Name != null)
                existingCompany.Name = updatedCompany.Name;

            if (updatedCompany.Email != null)
                existingCompany.Email = updatedCompany.Email;

            if (updatedCompany.Address != null)
                existingCompany.Address = updatedCompany.Address;

            if (updatedCompany.PhoneNumber != null)
                existingCompany.PhoneNumber = updatedCompany.PhoneNumber;
        }

        await _clientRepository.UpdateAsync(existing, cancellationToken);
    }


    public async Task SoftDeleteClientAsync(int id, CancellationToken cancellationToken = default)
    {
        var client = await _clientRepository.GetByIdAsync(id, cancellationToken);

        if (client is null)
            throw new NotFoundException("Client not found");

        if (client is CompanyClient)
            throw new ConflictException("Company clients cannot be deleted.");

        if (client is IndividualClient individual)
        {
            individual.SoftDelete();
            await _clientRepository.UpdateAsync(individual, cancellationToken);
        }
    }
}