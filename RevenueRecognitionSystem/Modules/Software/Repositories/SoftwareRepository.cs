using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.Infrastructure.DAL;

namespace RevenueRecognitionSystem.Modules.Software.Repositories;

public class SoftwareRepository : ISoftwareRepository
{
    private readonly RevenueRecognitionDbContext _context;

    public SoftwareRepository(RevenueRecognitionDbContext context)
    {
        _context = context;
    }
    
    public async Task<Modules.Software?> GetByIdAsync(int id)
    {
        return await _context.Software.FirstOrDefaultAsync(s => s.Id == id);
    }
}