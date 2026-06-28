namespace Backend.DTOs.AI.Phase3;

public class RbacAuditDto
{
    public string OverallRisk { get; set; }

    public AuditScoreDto Score { get; set; }

    public BestRoleDto BestDesignedRole { get; set; }

    public BestRoleDto MostPrivilegedRole { get; set; }

    public BestRoleDto MostRestrictedRole { get; set; }

    public List<AuditFindingDto> Findings { get; set; }

    public List<AuditRecommendationDto> Recommendations { get; set; }
}
