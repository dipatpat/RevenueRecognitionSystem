using RevenueRecognitionSystem.Features.Discounts.Models;
using RevenueRecognitionSystem.Shared.Enums;

namespace RevenueRecognitionSystem.Features.Discounts.Repositories;

public interface IDiscountRepository
{
    Task<IEnumerable<Discount>> GetActiveDiscountsAsync(DateTime date, DiscountType type);

}