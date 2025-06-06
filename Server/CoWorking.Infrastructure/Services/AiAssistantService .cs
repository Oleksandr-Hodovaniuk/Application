using CoWorking.Application.DTOs.AiAssistant;
using CoWorking.Application.Interfaces.Repositories;
using CoWorking.Application.Interfaces.Services;
using CoWorking.Application.Models;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoWorking.Infrastructure.Services;

public class AiAssistantService : IAiAssistantService
{
    private readonly AiAssistantSettings _settings;
    private readonly HttpClient _httpClient;
    public AiAssistantService(IOptions<AiAssistantSettings> options, HttpClient httpClient)
    {
        _settings = options.Value;
        _httpClient = httpClient;
    }

    public async Task<string> AskAsync(string question, IEnumerable<AiBookingDTO> bookings, CancellationToken cancellationToken)
    {
        var systemPrompt = $"You are a helpful assistant that answers questions about bookings. Local time is: {DateTime.Now}";

        var userPrompt = $"User question: \"{question}\". Bookings: \n" +
            JsonSerializer.Serialize(bookings, new JsonSerializerOptions { WriteIndented = true});

        var requestContent = new
        {
            model = _settings.Model,
            messages = new[]
            {
                new { role = "system", content = systemPrompt },
                new { role = "user",  content = userPrompt }
            }
        };

        var content = new StringContent(JsonSerializer.Serialize(requestContent), Encoding.UTF8, "application/json");

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _settings.ApiKey);

        var response = await _httpClient.PostAsync(_settings.Endpoint, content, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return "Sorry, I couldn't generate an answer.";
        }

        using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        using var doc = await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken);

        return doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").ToString();
    }
}
