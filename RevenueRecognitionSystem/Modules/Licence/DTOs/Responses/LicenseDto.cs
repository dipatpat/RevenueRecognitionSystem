namespace RevenueRecognitionSystem.Modules.Licence.DTOs.Responses;

public class LicenceDto
{
    public int Id { get; set; }
    public decimal FinalPrice { get; set; }
    public bool IsSigned { get; set; }
    public bool IsCancelled { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime PaymentDeadline { get; set; }
    public int SupportYears { get; set; }

    public string SoftwareName { get; set; } = null!;
    public string SoftwareVersion { get; set; } = null!;
    public string ClientFullName { get; set; } = null!;
}