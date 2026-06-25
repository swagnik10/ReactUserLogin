using Backend.Application.Agents;
using Backend.Application.CommandAndQuery;
using Backend.DTOs.Agent;
using MediatR;

namespace Backend.Application.Handlers.AgentHandler;

public class GeneratePlanHandler
    : IRequestHandler<GeneratePlanRequest, AgentPlanDto>
{
    private readonly IAiPlannerService _planner;

    public GeneratePlanHandler(
        IAiPlannerService planner)
    {
        _planner = planner;
    }

    public async Task<AgentPlanDto> Handle(
        GeneratePlanRequest request,
        CancellationToken cancellationToken)
    {
        return await _planner.GeneratePlanAsync(
            request.Prompt,
            cancellationToken);
    }
}