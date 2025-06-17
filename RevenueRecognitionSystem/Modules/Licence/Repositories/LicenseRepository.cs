using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.Infrastructure.DAL;
using RevenueRecognitionSystem.Modules.Licence.Models;

namespace RevenueRecognitionSystem.Features.Contracts.Repositories;

public class LicenseRepository : ILicenseRepository
{
    private readonly RevenueRecognitionDbContext _context;
    public LicenseRepository(RevenueRecognitionDbContext context)
    {
        _context = context;
    }
        
        
    public async Task<bool> HasActiveContractAsync(int clientId, int softwareId)
    {
        return await _context.Licences.AnyAsync(l =>
            l.ClientId == clientId &&
            l.SoftwareId == softwareId &&
            !l.IsCancelled &&
            !l.IsSigned == false && 
            l.EndDate > DateTime.UtcNow);
    }

    public async Task<bool> HasAnyPastContractAsync(int clientId)
    {
        return await _context.Licences.AnyAsync(l => l.ClientId == clientId && l.IsSigned);
    }
    
    public async Task<Licence?> GetByIdWithClientAndSoftwareAsync(int id)
    {
        return await _context.Licences
            .Include(l => l.Client)
            .Include(l => l.Software)
            .FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<int> AddAsync(Licence licence)
    {
        _context.Licences.Add(licence);
        await _context.SaveChangesAsync();
        return licence.Id;
    }
    
    public async Task UpdateAsync(Licence licence)
    {
        _context.Licences.Update(licence);
        await _context.SaveChangesAsync();
    }

}