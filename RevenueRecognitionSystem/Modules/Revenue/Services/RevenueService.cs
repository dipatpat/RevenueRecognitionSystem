using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.Infrastructure.DAL;
using RevenueRecognitionSystem.Modules.Revenue.DTOs.Requests;
using RevenueRecognitionSystem.Modules.Revenue.DTOs.Responses;

namespace RevenueRecognitionSystem.Features.Revenue.Services;

public class RevenueService : IRevenueService
{
    private readonly RevenueRecognitionDbContext _context;
    private readonly ICurrencyService _currencyService;

    public RevenueService(RevenueRecognitionDbContext context, ICurrencyService currencyService)
    {
        _context = context;
        _currencyService = currencyService;
    }

    public async Task<RevenueResponseDto> GetCurrentRevenueAsync(RevenueRequestDto dto)
    {
        var paymentsQuery = _context.Payments.AsQueryable();

        if (dto.SoftwareId.HasValue)
        {
            paymentsQuery = paymentsQuery
                .Where(p => p.Licence.SoftwareId == dto.SoftwareId.Value);
        }

        var revenuePLN = await paymentsQuery.SumAsync(p => p.Amount);

        return await ConvertCurrency(revenuePLN, dto.Currency);
    }

    public async Task<RevenueResponseDto> GetPredictedRevenueAsync(RevenueRequestDto dto)
    {
        var licencesQuery = _context.Licences.AsQueryable();

        if (dto.SoftwareId.HasValue)
        {
            licencesQuery = licencesQuery
                .Where(l => l.SoftwareId == dto.SoftwareId.Value);
        }

        var signed = licencesQuery.Where(l => l.IsSigned);
        var unsigned = licencesQuery.Where(l => !l.IsSigned && !l.IsCancelled);

        var signedRevenue = await signed.SumAsync(l => l.FinalPrice);
        var predictedRevenue = await unsigned.SumAsync(l => l.FinalPrice);

        var totalRevenue = signedRevenue + predictedRevenue;

        return await ConvertCurrency(totalRevenue, dto.Currency);
    }

    private async Task<RevenueResponseDto> ConvertCurrency(decimal valueInPLN, string? targetCurrency)
    {
        if (string.IsNullOrWhiteSpace(targetCurrency) || targetCurrency.ToUpper() == "PLN")
        {
            return new RevenueResponseDto
            {
                ValueInPLN = valueInPLN
            };
        }

        var rate = await _currencyService.GetExchangeRateAsync(targetCurrency.ToUpper());

        return new RevenueResponseDto
        {
            ValueInPLN = valueInPLN,
            ValueInCurrency = Math.Round(valueInPLN / rate, 2),
            Currency = targetCurrency.ToUpper()
        };
    }
}
