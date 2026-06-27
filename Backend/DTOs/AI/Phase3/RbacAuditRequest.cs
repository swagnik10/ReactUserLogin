using Backend.DTOs.AI.Phase1;

namespace Backend.DTOs.AI.Phase3;

public class RbacAuditRequest
{
    public List<RoleAnalysisRequest> Roles { get; set; } = [];
}
