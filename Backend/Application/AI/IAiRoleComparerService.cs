using Backend.DTOs.AI.Phase2;

namespace Backend.Application.AI;

public interface IAiRoleComparerService
{
    Task<RoleComparisonDto> CompareRolesAsync(RoleComparisonRequest request, CancellationToken cancellationToken);
}
