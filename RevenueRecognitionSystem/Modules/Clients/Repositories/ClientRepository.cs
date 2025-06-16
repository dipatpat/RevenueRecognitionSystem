using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.Features.Clients.Models;
using RevenueRecognitionSystem.Infrastructure.DAL;

namespace RevenueRecognitionSystem.Features.Clients.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly RevenueRecognitionDbContext _context;

    public ClientRepository(RevenueRecognitionDbContext context)
    {
        _context = context;
    }

    public async Task<Client?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Clients.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<List<Client>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Clients.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Client client, CancellationToken cancellationToken = default)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Client client, CancellationToken cancellationToken = default)
    {
        _context.Clients.Update(client);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task SoftDeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var client = await GetByIdAsync(id, cancellationToken);
        if (client is IndividualClient individual)
        {
            individual.SoftDelete();
            await UpdateAsync(individual, cancellationToken);
        }
    }
}