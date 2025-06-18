using RevenueRecognitionSystem.Exceptions;

namespace RevenueRecognitionSystem.Features.Revenue.Services;

public class CurrencyService : ICurrencyService
{
    private readonly HttpClient _httpClient;

    public CurrencyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<decimal> GetExchangeRateAsync(string currency)
    {
        if (currency.ToUpper() == "PLN")
            return 1.0m;

        var url = $"https://api.nbp.pl/api/exchangerates/rates/A/{currency}?format=json";

        var httpResponse = await _httpClient.GetAsync(url);

        if (!httpResponse.IsSuccessStatusCode)
            throw new BadRequestException($"Currency '{currency}' not recognized.");

        var result = await httpResponse.Content.ReadFromJsonAsync<NBPResponse>();

        return result?.Rates?.FirstOrDefault()?.Mid 
               ?? throw new Exception("Invalid API response.");
    }

    private class NBPResponse
    {
        public List<Rate> Rates { get; set; }

        public class Rate
        {
            public decimal Mid { get; set; }
        }
    }
}
