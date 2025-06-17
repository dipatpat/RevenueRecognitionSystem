using RevenueRecognitionSystem.Features.Payments.Models;

namespace RevenueRecognitionSystem.Features.Payments.Repositories;

public interface IPaymentRepository
{
    Task AddAsync(Payment payment);
    Task<Payment?> GetLatestPaymentByLicenceIdAsync(int licenceId);
    Task<List<Payment>> GetPaymentByLicenceIdAsync(int licenceId);
}