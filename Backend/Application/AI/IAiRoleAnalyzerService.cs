using Backend.DTOs.AI;

namespace Backend.Application.AI;

public interface IAiRoleAnalyzerService
{
    Task<RoleAnalysisDto> AnalyzeRoleAsync(RoleAnalysisRequest request, CancellationToken cancellationToken);
}
