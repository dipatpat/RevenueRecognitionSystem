using RevenueRecognitionSystem.Shared.Enums;

namespace RevenueRecognitionSystem.Modules.Payments.DTOs.Requests;

public class CreatePaymentDto
{
    public int LicenceId { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}
