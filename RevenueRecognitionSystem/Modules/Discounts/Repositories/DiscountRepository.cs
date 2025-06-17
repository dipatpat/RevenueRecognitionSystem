using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.Features.Discounts.Models;
using RevenueRecognitionSystem.Infrastructure.DAL;
using RevenueRecognitionSystem.Shared.Enums;

namespace RevenueRecognitionSystem.Features.Discounts.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly RevenueRecognitionDbContext _context;

    public DiscountRepository(RevenueRecognitionDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Discount>> GetActiveDiscountsAsync(DateTime date, DiscountType type)
    {
        return await _context.Discounts
            .Where(d => d.Type == type && date >= d.StartDate && date <= d.EndDate)
            .ToListAsync();
    }

        
}