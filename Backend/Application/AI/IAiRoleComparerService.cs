using Backend.DTOs.AI;

namespace Backend.Application.AI;

public interface IAiRoleComparerService
{
    Task<RoleComparisonDto> CompareRolesAsync(RoleComparisonRequest request, CancellationToken cancellationToken);
}
