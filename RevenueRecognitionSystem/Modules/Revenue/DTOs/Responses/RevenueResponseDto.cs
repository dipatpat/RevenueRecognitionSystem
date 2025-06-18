namespace RevenueRecognitionSystem.Modules.Revenue.DTOs.Responses;

public class RevenueResponseDto
{
    public decimal ValueInPLN { get; set; }
    public decimal? ValueInCurrency { get; set; }
    public string? Currency { get; set; }
}