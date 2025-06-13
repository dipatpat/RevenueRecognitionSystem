using RevenueRecognitionSystem.Shared.Enums;

namespace RevenueRecognitionSystem.Features.Discounts.Models;

public class Discount
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DiscountType Type { get; set; }
    public decimal Percentage { get; set; } // e.g., 0.10 = 10%
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public bool IsActive(DateTime referenceDate)
        => referenceDate >= StartDate && referenceDate <= EndDate;
}