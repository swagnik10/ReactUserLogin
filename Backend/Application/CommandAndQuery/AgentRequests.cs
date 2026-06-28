using Backend.DTOs.Agent;
using Backend.DTOs.AI.Ai_Fun;
using Backend.DTOs.AI.Phase1;
using Backend.DTOs.AI.Phase2;
using Backend.DTOs.AI.Phase3;
using MediatR;

namespace Backend.Application.CommandAndQuery;

public record GeneratePlanRequest(string Prompt) : IRequest<AgentPlanDto>;

public record ExecutePlanRequest(AgentPlanDto Plan) : IRequest<AgentExecutionResultDto>;

public record AnalyzeRoleRequest(string RoleName) : IRequest<RoleAnalysisDto>;

public record CompareRolesRequest(string RoleA, string RoleB) : IRequest<RoleComparisonDto>;

public record AuditRbacRequest() : IRequest<RbacAuditDto>;

public record AskRbacQuestion(string Question) : IRequest<AskRbacQuestionResponse>;

public record GenerateNicknameRequest(UserFunRequest Request)
    : IRequest<NicknameResponse>;

public record GenerateRoastRequest(UserFunRequest Request)
    : IRequest<RoastResponse>;

public record GenerateFortuneRequest(UserFunRequest Request)
    : IRequest<FortuneResponse>;

public record GenerateAchievementRequest(UserFunRequest Request)
    : IRequest<AchievementResponse>;
