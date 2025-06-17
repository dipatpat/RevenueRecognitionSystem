using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.Features.Payments.Models;
using RevenueRecognitionSystem.Infrastructure.DAL;

namespace RevenueRecognitionSystem.Features.Payments.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly RevenueRecognitionDbContext _context;

    public PaymentRepository(RevenueRecognitionDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Payment payment)
    {
        await _context.Payments.AddAsync(payment);
        await _context.SaveChangesAsync();
    }
    
    public async Task<Payment?> GetLatestPaymentByLicenceIdAsync(int licenceId)
    {
        return await _context.Payments
            .Where(p => p.LicenceId == licenceId)
            .OrderByDescending(p => p.PaymentDate)
            .FirstOrDefaultAsync();
    }
    public async Task<List<Payment>> GetPaymentByLicenceIdAsync(int licenceId)
    {
        return await _context.Payments
            .Where(p => p.LicenceId == licenceId)
            .ToListAsync();
    }
}