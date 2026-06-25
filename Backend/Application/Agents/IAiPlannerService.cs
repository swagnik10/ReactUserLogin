using Backend.DTOs.Agent;

namespace Backend.Application.Agents;

public interface IAiPlannerService
{
    Task<AgentPlanDto> GeneratePlanAsync(string prompt, CancellationToken cancellationToken);
}
