using Backend.DTOs.Agent;
using Backend.DTOs.AI.Phase1;
using Backend.DTOs.AI.Phase2;
using MediatR;

namespace Backend.Application.CommandAndQuery;

public record GeneratePlanRequest(string Prompt) : IRequest<AgentPlanDto>;

public record ExecutePlanRequest(AgentPlanDto Plan) : IRequest<AgentExecutionResultDto>;

public record AnalyzeRoleRequest(string RoleName) : IRequest<RoleAnalysisDto>;

public record CompareRolesRequest(string RoleA, string RoleB) : IRequest<RoleComparisonDto>;
