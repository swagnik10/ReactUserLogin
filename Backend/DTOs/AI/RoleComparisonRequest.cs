namespace Backend.DTOs.AI;

public class RoleComparisonRequest
{
    public RoleAnalysisRequest RoleA { get; set; } = new();

    public RoleAnalysisRequest RoleB { get; set; } = new();
}
