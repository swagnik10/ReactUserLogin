using Backend.DTOs.AI.Phase3;

namespace Backend.Application.AI;

public interface IAiRbacAuditService
{
    Task<RbacAuditDto> AuditAsync(
        RbacAuditRequest request,
        CancellationToken cancellationToken);
}
