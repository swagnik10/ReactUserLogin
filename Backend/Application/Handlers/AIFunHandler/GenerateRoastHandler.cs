using Backend.Application.AI.AiFun;
using Backend.Application.CommandAndQuery;
using Backend.DTOs.AI.Ai_Fun;
using MediatR;

namespace Backend.Application.Handlers.AIFunHandler;

public class GenerateRoastHandler
    : IRequestHandler<
        GenerateRoastRequest,
        RoastResponse>
{
    private readonly IAiUserFunService _aiService;

    public GenerateRoastHandler(
        IAiUserFunService aiService)
    {
        _aiService = aiService;
    }

    public async Task<RoastResponse> Handle(
        GenerateRoastRequest request,
        CancellationToken cancellationToken)
    {
        return await _aiService.GenerateRoastAsync(
            request.Request,
            cancellationToken);
    }
}