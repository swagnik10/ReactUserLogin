using System.Text.Json;
using Backend.Application.AI.Gemini;
using Backend.DTOs.Agent;
using Backend.DTOs.AI.Phase2;
using Microsoft.Extensions.Options;

namespace Backend.Application.AI;

public class GeminiRoleComparerService : GeminiBaseService, IAiRoleComparerService
{
    private readonly ILogger<GeminiRoleComparerService> _logger;

    public GeminiRoleComparerService(
        HttpClient httpClient,
        IOptions<GeminiSettings> settings,
        ILogger<GeminiRoleComparerService> logger)
        : base(httpClient, settings, logger)
    {
        _logger = logger;
    }

    public Task<RoleComparisonDto> CompareRolesAsync(
    RoleComparisonRequest request,
    CancellationToken cancellationToken)
    {
        _logger.LogInformation("Inside the AI Role Comparer Method");
        return GenerateAsync<RoleComparisonDto>(
            BuildPrompt(request),
            BuildResponseSchema(),
            "Role Comparison",
            cancellationToken);
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