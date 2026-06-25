using Backend.Application.Agents;
using Backend.Application.CommandAndQuery;
using Backend.DTOs.Agent;
using MediatR;

namespace Backend.Application.Handlers.AgentHandler;

public class GeneratePlanHandler
    : IRequestHandler<GeneratePlanRequest, AgentPlanDto>
{
    private readonly IAiPlannerService _planner;
    private readonly ILogger<GeneratePlanHandler> _logger;

    public GeneratePlanHandler(
        IAiPlannerService planner, ILogger<GeneratePlanHandler> logger)
    {
        _planner = planner;
        _logger = logger;
    }

    public async Task<AgentPlanDto> Handle(
        GeneratePlanRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Generate Plan Handler started");
        return await _planner.GeneratePlanAsync(
            request.Prompt,
            cancellationToken);
        
    }
}