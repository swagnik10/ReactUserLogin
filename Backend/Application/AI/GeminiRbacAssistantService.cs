using System.Text.Json;
using Backend.Application.AI.Gemini;
using Backend.DTOs.Agent;
using Backend.DTOs.AI.Phase3;
using Microsoft.Extensions.Options;

namespace Backend.Application.AI;

public class GeminiRbacAssistantService
    : GeminiBaseService, IAiRbacAssistantService
{
    public GeminiRbacAssistantService(
        HttpClient httpClient,
        IOptions<GeminiSettings> settings,
        ILogger<GeminiRbacAssistantService> logger)
        : base(httpClient, settings, logger)
    {
    }

    public Task<AskRbacQuestionResponse> AskQuestionAsync(
        RbacQuestion request,
        CancellationToken cancellationToken)
    {
        return GenerateAsync<AskRbacQuestionResponse>(
            BuildPrompt(request),
            BuildResponseSchema(),
            "RBAC Assistant",
            cancellationToken);
    }

    private static string BuildPrompt(RbacQuestion request)
    {
        var requestJson = JsonSerializer.Serialize(
            request,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

        return $$"""
                You are an Enterprise RBAC Security Consultant.

                You are an expert in:

                - Role-Based Access Control (RBAC)
                - Enterprise Authorization
                - Permission Management
                - Least Privilege
                - Separation of Duties
                - Access Governance

                You will receive:

                1. A user question.
                2. The complete RBAC model.

                Your job is to answer ONLY questions related to the supplied RBAC model.

                Allowed topics include:

                - Roles
                - Permissions
                - Authorization
                - Security
                - Least Privilege
                - Separation of Duties
                - Dangerous permissions
                - Missing permissions
                - Role recommendations
                - Access management
                - RBAC design

                WRITING STYLE
                
                - Return concise, professional enterprise language.
                - Each array item must contain exactly one observation.
                - Do not combine multiple observations into a single sentence.
                - Keep every array item under 25 words.

                Findings

                - Include findings only if they support the user's question.
                - If there are no meaningful findings, return an empty array.

                Recommendations

                - Include recommendations only when they add value.
                - Do not generate recommendations for simple informational questions.
                - If no recommendation is appropriate, return an empty array.

                When mentioning roles:

                - Always use the actual role names.
                - When comparing or discussing overlap, explicitly list the roles involved.
                - If multiple role pairs overlap, return one finding for each pair.

                If the question is unrelated to RBAC or cannot be answered from the supplied RBAC model:

                Answer exactly:

                "Sorry, this question is not related to the RBAC model or cannot be answered using the provided role data."

                Rules

                - Use ONLY the supplied RBAC model.
                - Never invent permissions.
                - Never invent roles.
                - Never assume hidden business rules.
                - Keep the answer concise.
                - Include only relevant findings.
                - Include only relevant recommendations.
                - Return ONLY valid JSON.

                Input:

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
                answer = new
                {
                    type = "STRING"
                },

                findings = new
                {
                    type = "ARRAY",
                    items = BuildFindingSchema()
                },

                recommendations = new
                {
                    type = "ARRAY",
                    items = BuildRecommendationSchema()
                }
            },

            required = new[]
            {
                "answer",
                "findings",
                "recommendations"
            }
        };
    }

    private static object BuildFindingSchema()
    {
        return new
        {
            type = "OBJECT",

            properties = new
            {
                severity = new { type = "STRING" },
                title = new { type = "STRING" },
                description = new { type = "STRING" }
            },

            required = new[]
            {
                "severity",
                "title",
                "description"
            }
        };
    }

    private static object BuildRecommendationSchema()
    {
        return new
        {
            type = "OBJECT",

            properties = new
            {
                priority = new { type = "STRING" },
                title = new { type = "STRING" },
                description = new { type = "STRING" }
            },

            required = new[]
            {
                "priority",
                "title",
                "description"
            }
        };
    }
}