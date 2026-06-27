using Backend.DTOs.AI.Phase1;

namespace Backend.Application.AI;

public interface IAiRoleAnalyzerService
{
    Task<RoleAnalysisDto> AnalyzeRoleAsync(RoleAnalysisRequest request, CancellationToken cancellationToken);
}
