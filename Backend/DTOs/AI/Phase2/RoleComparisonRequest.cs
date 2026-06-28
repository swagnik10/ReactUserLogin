using Backend.DTOs.AI.Phase1;

namespace Backend.DTOs.AI.Phase2;

public class RoleComparisonRequest
{
    public RoleAnalysisRequest RoleA { get; set; } = new();

    public RoleAnalysisRequest RoleB { get; set; } = new();
}
