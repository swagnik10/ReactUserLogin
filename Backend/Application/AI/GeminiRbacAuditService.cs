using System.Text.Json;
using Backend.DTOs.AI.Phase3;
using Backend.DTOs.Agent;
using Microsoft.Extensions.Options;
using Backend.Application.AI.Gemini;

namespace Backend.Application.AI;

public class GeminiRbacAuditService
    : GeminiBaseService, IAiRbacAuditService
{
    public readonly ILogger<GeminiRbacAuditService> _logger;
    public GeminiRbacAuditService(
        HttpClient httpClient,
        IOptions<GeminiSettings> settings,
        ILogger<GeminiRbacAuditService> logger)
        : base(httpClient, settings, logger)
    {
        _logger = logger;
    }

    public Task<RbacAuditDto> AuditAsync(
        RbacAuditRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Inside the Ai Audit Service");
        return GenerateAsync<RbacAuditDto>(
            BuildPrompt(request),
            BuildResponseSchema(),
            "RBAC Audit",
            cancellationToken);
    }

    private static string BuildPrompt(RbacAuditRequest request)
    {
        var requestJson = JsonSerializer.Serialize(
            request,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

        return $$"""
                You are an Enterprise RBAC Security Auditor.

                You are an expert in:

                - Role-Based Access Control (RBAC)
                - Enterprise Authorization
                - Least Privilege
                - Separation of Duties
                - Access Governance
                - Permission Design
                - Enterprise Security Reviews

                You will receive a complete RBAC model.

                Your task is to perform a comprehensive security and governance audit using ONLY the supplied role and permission data.

                Evaluate the RBAC model for:

                - Overall security posture
                - Overall RBAC health
                - Overlapping roles
                - Redundant permissions
                - Missing permissions
                - Dangerous permissions
                - Least privilege violations
                - Separation of duties violations
                - Orphan roles
                - Role hierarchy issues
                - Naming consistency
                - Permission consistency
                - Maintainability

                WRITING STYLE
                
                - Return concise, professional enterprise language.
                - Each array item must contain exactly one observation.
                - Do not combine multiple observations into a single sentence.
                - Keep every array item under 25 words.


                Determine:

                - Overall risk (Low, Medium, High, Critical)
                - Security score (0-100)
                - Maintainability score (0-100)
                - Least Privilege score (0-100)
                - Consistency score (0-100)
                - Overall score (0-100)
                - Best designed role
                - Most privileged role
                - Most restricted role

                Findings

                - Each finding must describe exactly one issue.
                - Use severity: Critical, High, Medium, Low, or Informational.
                - Mention the actual role names whenever applicable.
                - If two or more roles overlap, explicitly identify every overlapping role.
                - Explain why the issue matters from a security or governance perspective.
                - Do not create duplicate findings.

                Recommendations

                - Every recommendation must address one or more findings.
                - Recommendations must be practical and actionable.
                - Recommend role consolidation only when substantial permission overlap exists.
                - Recommend separation of duties when excessive privileges are combined.
                - Recommend least privilege improvements where appropriate.
                - If the RBAC model is already well designed, state that instead of inventing problems.

                Scoring Guidelines

                - Base every score solely on the supplied RBAC model.
                - Penalize excessive privilege concentration.
                - Penalize separation of duties violations.
                - Penalize redundant or overlapping roles that reduce maintainability.
                - Reward clear role separation and least privilege.
                - Reward consistent naming and focused role responsibilities.

                Rules

                - Use ONLY the supplied RBAC model.
                - Never invent roles.
                - Never invent permissions.
                - Never invent business rules.
                - Never assume hidden permissions.
                - Do not speculate.
                - Keep findings concise.
                - Keep recommendations actionable.
                - Return ONLY valid JSON.
                - Do not return markdown.
                - Do not return explanations outside the JSON response.

                RBAC Model:

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
                overallRisk = new
                {
                    type = "STRING"
                },

                score = new
                {
                    type = "OBJECT",

                    properties = new
                    {
                        security = new { type = "INTEGER" },
                        maintainability = new { type = "INTEGER" },
                        leastPrivilege = new { type = "INTEGER" },
                        consistency = new { type = "INTEGER" },
                        overall = new { type = "INTEGER" }
                    },

                    required = new[]
                    {
                        "security",
                        "maintainability",
                        "leastPrivilege",
                        "consistency",
                        "overall"
                    }
                },

                bestDesignedRole = BuildRoleSchema(),

                mostPrivilegedRole = BuildRoleSchema(),

                mostRestrictedRole = BuildRoleSchema(),

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
                "overallRisk",
                "score",
                "bestDesignedRole",
                "mostPrivilegedRole",
                "mostRestrictedRole",
                "findings",
                "recommendations"
            }
        };
    }

    private static object BuildRoleSchema()
    {
        return new
        {
            type = "OBJECT",

            properties = new
            {
                name = new { type = "STRING" },
                reason = new { type = "STRING" }
            },

            required = new[]
            {
                "name",
                "reason"
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