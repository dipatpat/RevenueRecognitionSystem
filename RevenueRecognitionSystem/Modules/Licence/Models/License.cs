using System.Diagnostics.Contracts;
using RevenueRecognitionSystem.Features.Clients.Models;
using RevenueRecognitionSystem.Features.Payments.Models;
using RevenueRecognitionSystem.Shared.Enums;

namespace RevenueRecognitionSystem.Modules.Licence.Models;

public class Licence
{
    public int Id { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public int SoftwareId { get; set; }
    public Software.Modules.Software Software { get; set; } = null!;

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime PaymentDeadline { get; set; }

    public decimal FinalPrice { get; set; }

    public bool IsSigned { get; set; }

    public int SupportYears { get; set; } 

    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}