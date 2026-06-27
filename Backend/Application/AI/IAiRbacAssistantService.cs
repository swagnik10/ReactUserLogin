using Backend.DTOs.AI.Phase3;

namespace Backend.Application.AI;

public interface IAiRbacAssistantService
{
    Task<AskRbacQuestionResponse> AskQuestionAsync(
        RbacQuestion request,
        CancellationToken cancellationToken);
}
