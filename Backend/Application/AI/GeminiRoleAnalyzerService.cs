using Backend.Application.AI.Gemini;
using Backend.DTOs.Agent;
using Backend.DTOs.AI.Phase1;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Backend.Application.AI;

public class GeminiRoleAnalyzerService : GeminiBaseService, IAiRoleAnalyzerService
{
    private readonly ILogger<GeminiRoleAnalyzerService> _logger;

    public GeminiRoleAnalyzerService(
        HttpClient httpClient,
        IOptions<GeminiSettings> settings,
        ILogger<GeminiRoleAnalyzerService> logger) : base(httpClient, settings, logger)
    {
        _logger = logger;
    }

    public Task<RoleAnalysisDto> AnalyzeRoleAsync(
    RoleAnalysisRequest request,
    CancellationToken cancellationToken)
    {
        _logger.LogInformation("Inside the Ai Role Analyzer Method");
        return GenerateAsync<RoleAnalysisDto>(
            BuildPrompt(request),
            BuildResponseSchema(),
            "Role Analysis",
            cancellationToken);
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