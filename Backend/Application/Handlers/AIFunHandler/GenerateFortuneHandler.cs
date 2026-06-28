using Backend.Application.AI.AiFun;
using Backend.Application.CommandAndQuery;
using Backend.DTOs.AI.Ai_Fun;
using MediatR;

namespace Backend.Application.Handlers.AIFunHandler;

public class GenerateFortuneHandler
    : IRequestHandler<
        GenerateFortuneRequest,
        FortuneResponse>
{
    private readonly IAiUserFunService _aiService;

    public GenerateFortuneHandler(
        IAiUserFunService aiService)
    {
        _aiService = aiService;
    }

    public async Task<FortuneResponse> Handle(
        GenerateFortuneRequest request,
        CancellationToken cancellationToken)
    {
        return await _aiService.GenerateFortuneAsync(
            request.Request,
            cancellationToken);
    }
}
