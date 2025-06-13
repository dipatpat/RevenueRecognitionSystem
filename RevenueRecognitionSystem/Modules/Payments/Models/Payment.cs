using RevenueRecognitionSystem.Modules.Licence.Models;
using RevenueRecognitionSystem.Shared.Enums;

namespace RevenueRecognitionSystem.Features.Payments.Models;

public class Payment
{
    public int Id { get; set; }

    public int LicenceId { get; set; }
    public Licence Licence { get; set; } = null!;

    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

    public decimal Amount { get; set; }

    public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.BankTransfer;
    public string TransactionReference { get; set; } = Guid.NewGuid().ToString(); 

    public bool Confirmed { get; set; } = true; 
}
