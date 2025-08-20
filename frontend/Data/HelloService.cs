using System.Text.Json;

namespace frontend.Data;

public class HelloService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<HelloService> _logger;

    public HelloService(HttpClient httpClient, ILogger<HelloService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<string?> GetGreetingAsync()
    {
        try
        {
            _logger.LogInformation("Fetching greeting from backend API");
            
            // Use WORKSPACE_DEV_DOMAIN environment variable for Codesphere
            var workspaceDomain = Environment.GetEnvironmentVariable("WORKSPACE_DEV_DOMAIN");
            var apiUrl = !string.IsNullOrEmpty(workspaceDomain) 
                ? $"https://{workspaceDomain}/api/hello"
                : "http://localhost:3000/api/hello"; // Fallback for local development
            
            _logger.LogInformation("Calling backend at: {ApiUrl}", apiUrl);
            var response = await _httpClient.GetAsync(apiUrl);
            
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Backend API returned status code: {StatusCode}", response.StatusCode);
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("Received response: {Content}", content);
            
            var jsonDocument = JsonSerializer.Deserialize<JsonElement>(content);
            
            if (jsonDocument.TryGetProperty("message", out var messageElement))
            {
                var message = messageElement.GetString();
                _logger.LogInformation("Successfully fetched greeting: {Message}", message);
                return message;
            }
            
            _logger.LogWarning("No message found in API response");
            return "Hello World!";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while fetching greeting");
            return null;
        }
    }
}
