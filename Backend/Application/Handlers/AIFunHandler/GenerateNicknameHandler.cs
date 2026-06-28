using Backend.Application.AI.AiFun;
using Backend.Application.CommandAndQuery;
using Backend.DTOs.AI.Ai_Fun;
using MediatR;

namespace Backend.Application.Handlers.AIFunHandler;

public class GenerateNicknameHandler
    : IRequestHandler<
        GenerateNicknameRequest,
        NicknameResponse>
{
    private readonly IAiUserFunService _aiService;

    public GenerateNicknameHandler(
        IAiUserFunService aiService)
    {
        _aiService = aiService;
    }

    public async Task<NicknameResponse> Handle(
        GenerateNicknameRequest request,
        CancellationToken cancellationToken)
    {
        return await _aiService.GenerateNicknameAsync(
            request.Request,
            cancellationToken);
    }
}