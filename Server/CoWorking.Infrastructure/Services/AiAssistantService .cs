using CoWorking.Application.DTOs.AiAssistant;
using CoWorking.Application.Exceptions;
using CoWorking.Application.Interfaces.Repositories;
using CoWorking.Application.Interfaces.Services;
using CoWorking.Application.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
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

    public async Task<AiAssistantResponseDTO> AskAsync(string question, IEnumerable<AiBookingDTO> bookings, CancellationToken cancellationToken)
    {
        // Get ai prompt.
        var baseDir = AppContext.BaseDirectory;
        var serverRoot = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", ".."));
        var filePath = Path.Combine(serverRoot, "CoWorking.Infrastructure", "Resources", "AiAssistantPrompt.txt");

        var userTemplate = await File.ReadAllTextAsync(filePath);
        var userPrompt = userTemplate
            .Replace("{CurrentTime}", DateTime.Now.ToString())
            .Replace("{question}", question)
            .Replace("{bookings_json}", JsonSerializer.Serialize(bookings, new JsonSerializerOptions { WriteIndented = true }));

        // Configure request.
        var requestContent = new
        {
            model = _settings.Model,
            messages = new[]
            {
                new { role = "user",  content = userPrompt }
            }
        };

        var content = new StringContent(JsonSerializer.Serialize(requestContent), Encoding.UTF8, "application/json");

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _settings.ApiKey);

        // Get response.
        var response = await _httpClient.PostAsync(_settings.Endpoint, content, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new BusinessException("AI response could not be parsed.");
        }

        // Read and parse.
        using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        using var document = await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken);

        // Get the necessary information.
        var rawContent = document.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString();

        if (string.IsNullOrWhiteSpace(rawContent))
        {
            throw new BusinessException("AI did not return any content.");
        }

        // Extracts the valid JSON.
        var jsonPart = ExtractFirstJsonBlock(rawContent);

        if (jsonPart == null)
        {
            throw new BusinessException("Could not extract valid JSON from AI response.");
        }

        // Deserialize JSON into the object.
        var aiResponse = JsonSerializer.Deserialize<AiAssistantResponseDTO>
        (
            jsonPart,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        if (aiResponse == null)
        {
            throw new BusinessException("AI response could not be parsed.");
        }

        return aiResponse;
    }
    
    // Extracts the first valid JSON object substring from the input text, correctly handling nested curly braces.
    string ExtractFirstJsonBlock(string input)
    {
        int start = input.IndexOf('{');
        if (start == -1) return null!;

        int depth = 0;
        for (int i = start; i < input.Length; i++)
        {
            if (input[i] == '{') depth++;
            else if (input[i] == '}') depth--;

            if (depth == 0)
            {
                return input.Substring(start, i - start + 1);
            }
        }

        return null!;
    }
}
