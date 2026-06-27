using System.Text.Json;
using Backend.DTOs.Agent;
using Backend.DTOs.AI;
using Microsoft.Extensions.Options;

namespace Backend.Application.AI;

public class GeminiRoleComparerService : IAiRoleComparerService
{
    private readonly HttpClient _httpClient;
    private readonly GeminiSettings _settings;
    private readonly ILogger<GeminiRoleComparerService> _logger;

    public GeminiRoleComparerService(
        HttpClient httpClient,
        IOptions<GeminiSettings> settings,
        ILogger<GeminiRoleComparerService> logger)
    {
        _httpClient = httpClient;
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task<RoleComparisonDto> CompareRolesAsync(
        RoleComparisonRequest request,
        CancellationToken cancellationToken)
    {
        var prompt = BuildPrompt(request);

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
                responseSchema = BuildResponseSchema()
            }
        };

        var endpoint =
            $"https://generativelanguage.googleapis.com/v1beta/models/{_settings.Model}:generateContent?key={_settings.ApiKey}";

        var response =
            await _httpClient.PostAsJsonAsync(
                endpoint,
                requestBody,
                cancellationToken);

        response.EnsureSuccessStatusCode();

        var rawResponse =
            await response.Content.ReadAsStringAsync(
                cancellationToken);

        _logger.LogInformation(
            "Gemini Role Comparison Response: {Response}",
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
            throw new Exception("Gemini returned an empty comparison.");
        }

        text = CleanJson(text);

        var comparison =
            JsonSerializer.Deserialize<RoleComparisonDto>(
                text,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

        if (comparison == null)
        {
            throw new Exception("Failed to deserialize role comparison.");
        }

        return comparison;
    }
    private static string CleanJson(string json)
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
    private static string BuildPrompt(RoleComparisonRequest request)
    {
        var requestJson = JsonSerializer.Serialize(
            request,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

        return $$"""
                You are an Enterprise RBAC Security Auditor.

                Your task is to compare two existing roles based ONLY on the information provided.

                The roles being compared are:

                - Role A: {{request.RoleA.Name}}
                - Role B: {{request.RoleB.Name}}

                IMPORTANT RULES

                - Use ONLY the supplied role information.
                - Do NOT invent permissions.
                - Do NOT assume hidden capabilities.
                - Do NOT infer business rules.
                - Do NOT recommend renaming roles.
                - Do NOT mention "RoleA" or "RoleB" anywhere in the response.
                - Always refer to roles by their actual names (for example: "Admin" and "PowerUser").
                - If there is insufficient information, explicitly state that.

                WRITING STYLE

                - Return concise, professional enterprise language.
                - Each array item must contain exactly one observation.
                - Do not combine multiple observations into a single sentence.
                - Keep every array item under 25 words.

                JSON FIELD REQUIREMENTS

                summary
                - Briefly summarize the overall differences between the two roles.
                - Mention the actual role names.

                similarities
                - Include ONLY permissions that are GRANTED to BOTH roles.
                - Ignore permissions denied by both roles.

                differences
                - Describe the major capability differences.
                - Use the actual role names.

                permissionsOnlyInRoleA
                - List capabilities that ONLY {{request.RoleA.Name}} has.
                - One permission per array item.

                permissionsOnlyInRoleB
                - List capabilities that ONLY {{request.RoleB.Name}} has.
                - One permission per array item.

                recommendedUseCases
                - Recommend practical enterprise scenarios for each role.
                - Keep recommendations realistic and concise.

                securityImplications
                - Highlight security risks, privilege concerns, or least-privilege observations.
                - Mention only meaningful security implications.

                Return ONLY valid JSON.

                Return EXACTLY this schema:

                {
                  "summary": "",
                  "similarities": [],
                  "differences": [],
                  "permissionsOnlyInRoleA": [],
                  "permissionsOnlyInRoleB": [],
                  "recommendedUseCases": [],
                  "securityImplications": []
                }

                Role data:

                {{requestJson}}
                """;
    }
    private static object BuildResponseSchema()
    {
        return new
        {
            type = "OBJECT",

            properties = new
            {
                summary = new
                {
                    type = "STRING"
                },

                similarities = new
                {
                    type = "ARRAY",
                    items = new { type = "STRING" }
                },

                differences = new
                {
                    type = "ARRAY",
                    items = new { type = "STRING" }
                },

                permissionsOnlyInRoleA = new
                {
                    type = "ARRAY",
                    items = new { type = "STRING" }
                },

                permissionsOnlyInRoleB = new
                {
                    type = "ARRAY",
                    items = new { type = "STRING" }
                },

                recommendedUseCases = new
                {
                    type = "ARRAY",
                    items = new { type = "STRING" }
                },

                securityImplications = new
                {
                    type = "ARRAY",
                    items = new { type = "STRING" }
                }
            },

            required = new[]
            {
            "summary",
            "similarities",
            "differences",
            "permissionsOnlyInRoleA",
            "permissionsOnlyInRoleB",
            "recommendedUseCases",
            "securityImplications"
        }
        };
    }
}