using System.Text.Json;

namespace frontend.Data;

public class RateService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<RateService> _logger;

    public RateService(HttpClient httpClient, ILogger<RateService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<Dictionary<string, double>?> GetRatesAsync()
    {
        try
        {
            _logger.LogInformation("Fetching rates from backend API");
            
            var response = await _httpClient.GetAsync("/api/rate");
            
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Backend API returned status code: {StatusCode}", response.StatusCode);
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            var jsonDocument = JsonSerializer.Deserialize<JsonElement>(content);
            
            // Extract rates from the API response structure
            if (jsonDocument.TryGetProperty("rates", out var ratesElement))
            {
                var rates = new Dictionary<string, double>();
                
                foreach (var rate in ratesElement.EnumerateObject())
                {
                    if (rate.Value.ValueKind == JsonValueKind.Number)
                    {
                        rates[rate.Name] = rate.Value.GetDouble();
                    }
                }
                
                _logger.LogInformation("Successfully fetched {Count} currency rates", rates.Count);
                return rates;
            }
            
            _logger.LogWarning("No rates found in API response");
            return new Dictionary<string, double>();
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Network error while fetching rates from backend");
            return null;
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogError(ex, "Request timeout while fetching rates from backend");
            return null;
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Error parsing JSON response from backend");
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while fetching rates");
            return null;
        }
    }
}
