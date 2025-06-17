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
}