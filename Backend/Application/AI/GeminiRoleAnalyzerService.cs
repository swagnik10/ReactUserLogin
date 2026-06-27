using System.Text.Json;
using System.Text.Json.Serialization;
using Backend.DTOs.Agent;
using Backend.DTOs.AI;
using Backend.Enum_And_Constants;
using Microsoft.Extensions.Options;

namespace Backend.Application.AI;

public class GeminiRoleAnalyzerService : IAiRoleAnalyzerService
{
    private readonly HttpClient _httpClient;
    private readonly GeminiSettings _settings;
    private readonly ILogger<GeminiRoleAnalyzerService> _logger;

    public GeminiRoleAnalyzerService(
        HttpClient httpClient,
        IOptions<GeminiSettings> settings,
        ILogger<GeminiRoleAnalyzerService> logger)
    {
        _httpClient = httpClient;
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task<RoleAnalysisDto> AnalyzeRoleAsync(
        RoleAnalysisRequest request,
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
            await response.Content.ReadAsStringAsync(cancellationToken);

        _logger.LogInformation(
            "Gemini Role Analysis Response: {Response}",
            rawResponse);

        using var document = JsonDocument.Parse(rawResponse);

        var text =
            document.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

        if (string.IsNullOrWhiteSpace(text))
        {
            throw new Exception("Gemini returned an empty analysis.");
        }

        text = CleanJson(text);

        var analysis =
            JsonSerializer.Deserialize<RoleAnalysisDto>(
                text,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters =
                    {
                        new JsonStringEnumConverter()
                    },
                    
                });

        if (analysis == null)
        {
            throw new Exception("Failed to deserialize role analysis.");
        }
        
        return analysis;
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

    private static string BuildPrompt(RoleAnalysisRequest request)
    {
        var roleJson = JsonSerializer.Serialize(
            request,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

        return $$"""
            You are an Enterprise RBAC Security Auditor.

            Analyze ONLY the supplied role.

            Use ONLY the supplied role information.

            Do NOT assume additional functionality.

            Do NOT infer hidden permissions.

            Do NOT infer business rules.

            Assume the role names are intentional.

            Do NOT recommend renaming roles.

            If there is insufficient information to reach a conclusion,
            state that explicitly instead of making assumptions.

            Focus only on:

            • Granted permissions
            • Denied permissions
            • Security risks
            • Principle of Least Privilege
            • Separation of Duties
            • Privilege Escalation
            • Administrative Risk
            • Operational Maintainability

            Return ONLY valid JSON.

            RiskLevel MUST be one of these exact strings:

            Low
            Medium
            High
            Critical

            Never return integers.

            Never return enum indexes.

            Never return numeric values.

            Do NOT include markdown.

            Return exactly this schema:

            {
              "summary": "",
              "capabilities": [],
              "restrictions": [],
              "risks": [],
              "riskLevel": "",
              "recommendations":[
                {
                    "title":"",
                    "priority":"",
                    "description":""
                }
            ]
            }

            Role:

            {{roleJson}}
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

                capabilities = new
                {
                    type = "ARRAY",
                    items = new
                    {
                        type = "STRING"
                    }
                },

                restrictions = new
                {
                    type = "ARRAY",
                    items = new
                    {
                        type = "STRING"
                    }
                },

                risks = new
                {
                    type = "ARRAY",

                    items = new
                    {
                        type = "OBJECT",

                        properties = new
                        {
                            type = new
                            {
                                type = "STRING"
                            },

                            description = new
                            {
                                type = "STRING"
                            }
                        },

                        required = new[]
                        {
                        "type",
                        "description"
                    }
                    }
                },

                riskLevel = new
                {
                    type = "STRING",

                    @enum = new[]
                    {
                    "Low",
                    "Medium",
                    "High",
                    "Critical"
                }
                },

                recommendations = new
                {
                    type = "ARRAY",

                    items = new
                    {
                        type = "OBJECT",

                        properties = new
                        {
                            title = new
                            {
                                type = "STRING"
                            },

                            priority = new
                            {
                                type = "STRING"
                            },

                            description = new
                            {
                                type = "STRING"
                            }
                        },

                        required = new[]
                        {
                            "title",
                            "priority",
                            "description"
                        }
                    }
                }
            },

            required = new[]
            {
            "summary",
            "capabilities",
            "restrictions",
            "risks",
            "riskLevel",
            "recommendations"
        }
        };
    }

}