using Backend.Application.AI.AiFun;
using Backend.Application.CommandAndQuery;
using Backend.DTOs.AI.Ai_Fun;
using MediatR;

namespace Backend.Application.Handlers.AIFunHandler;

public class GenerateAchievementHandler
    : IRequestHandler<
        GenerateAchievementRequest,
        AchievementResponse>
{
    private readonly IAiUserFunService _aiService;

    public GenerateAchievementHandler(
        IAiUserFunService aiService)
    {
        _aiService = aiService;
    }

    public async Task<AchievementResponse> Handle(
        GenerateAchievementRequest request,
        CancellationToken cancellationToken)
    {
        return await _aiService.GenerateAchievementAsync(
            request.Request,
            cancellationToken);
    }
}