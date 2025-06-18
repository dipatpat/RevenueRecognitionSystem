namespace RevenueRecognitionSystem.Features.Revenue.Services;

public interface ICurrencyService
{
    Task<decimal> GetExchangeRateAsync(string currency);
}