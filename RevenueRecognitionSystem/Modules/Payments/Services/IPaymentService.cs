using RevenueRecognitionSystem.Modules.Payments.DTOs.Requests;

namespace RevenueRecognitionSystem.Features.Payments.Services;

public interface IPaymentService
{
    Task AddPaymentAsync(CreatePaymentDto dto);

}