using RevenueRecognitionSystem.Features.Clients.Models;

namespace RevenueRecognitionSystem.Features.Clients.Services;

public interface IClientService
{
    Task<List<Client>> GetAllClientsAsync(CancellationToken cancellationToken = default);
    Task<Client?> GetClientByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddClientAsync(Client client, CancellationToken cancellationToken = default);
    Task UpdateClientAsync(Client client, CancellationToken cancellationToken = default);
    Task SoftDeleteClientAsync(int id, CancellationToken cancellationToken = default);
}