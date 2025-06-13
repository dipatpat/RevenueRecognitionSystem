using RevenueRecognitionSystem.Features.Clients.Models;

namespace RevenueRecognitionSystem.Features.Clients.Repositories;

public interface IClientRepository
{
    Task<Client?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<List<Client>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Client client, CancellationToken cancellationToken = default);
    Task UpdateAsync(Client client, CancellationToken cancellationToken = default);
    
    Task SoftDeleteAsync(int id, CancellationToken cancellationToken = default);
}