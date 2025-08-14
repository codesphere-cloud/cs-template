using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RateController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<RateController> _logger;

    public RateController(HttpClient httpClient, ILogger<RateController> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetRates()
    {
        try
        {
            _logger.LogInformation("Fetching currency rates from external API");
            
            var response = await _httpClient.GetAsync("https://open.er-api.com/v6/latest/USD");
            
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("External API returned status code: {StatusCode}", response.StatusCode);
                return StatusCode(500, "Failed to fetch currency rates from external service");
            }

            var content = await response.Content.ReadAsStringAsync();
            var rateData = JsonSerializer.Deserialize<object>(content);
            
            _logger.LogInformation("Successfully fetched currency rates");
            return Ok(rateData);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Network error while fetching currency rates");
            return StatusCode(500, "Network error occurred while fetching currency rates");
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogError(ex, "Request timeout while fetching currency rates");
            return StatusCode(500, "Request timeout while fetching currency rates");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while fetching currency rates");
            return StatusCode(500, "An unexpected error occurred");
        }
    }
}
