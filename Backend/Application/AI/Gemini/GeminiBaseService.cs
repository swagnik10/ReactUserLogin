namespace Backend.Application.AI.Gemini;

using System.Text.Json;
using System.Text.Json.Serialization;
using Backend.DTOs.Agent;
using Microsoft.Extensions.Options;


public abstract class GeminiBaseService
{
    protected readonly HttpClient HttpClient;
    protected readonly GeminiSettings Settings;
    protected readonly ILogger Logger;

    protected GeminiBaseService(
        HttpClient httpClient,
        IOptions<GeminiSettings> settings,
        ILogger logger)
    {
        HttpClient = httpClient;
        Settings = settings.Value;
        Logger = logger;
    }

    protected async Task<T> GenerateAsync<T>(
        string prompt,
        object responseSchema,
        string operationName,
        CancellationToken cancellationToken)
    {
        var requestBody = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new
                        {
                            text = prompt
                        }
                    }
                }
            },

            generationConfig = new
            {
                responseMimeType = "application/json",
                responseSchema
            }
        };

        var endpoint =
            $"https://generativelanguage.googleapis.com/v1beta/models/{Settings.Model}:generateContent?key={Settings.ApiKey}";

        var response =
            await HttpClient.PostAsJsonAsync(
                endpoint,
                requestBody,
                cancellationToken);

        response.EnsureSuccessStatusCode();

        var rawResponse =
            await response.Content.ReadAsStringAsync(
                cancellationToken);

        Logger.LogInformation(
            "Gemini {Operation} Response: {Response}",
            operationName,
            rawResponse);

        using var document =
            JsonDocument.Parse(rawResponse);

        var text =
            document.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

        if (string.IsNullOrWhiteSpace(text))
        {
            throw new Exception(
                $"Gemini returned an empty {operationName} response.");
        }

        text = CleanJson(text);

      

    var result = JsonSerializer.Deserialize<T>(
                    text,
                    JsonOptions);

        if (result == null)
        {
            throw new Exception(
                $"Failed to deserialize Gemini {operationName} response.");
        }

        return result;
    }

    protected static string CleanJson(string json)
    {
        json = json.Trim();

        if (json.StartsWith("```json"))
        {
            json = json.Replace("```json", "");
        }

        if (json.EndsWith("```"))
        {
            json = json.Replace("```", "");
        }

        return json.Trim();
    }

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters =
        {
            new JsonStringEnumConverter()
        }
    };
}
