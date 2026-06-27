using Backend.DTOs.AI.Phase2;
using Backend.Enum_And_Constants;

namespace Backend.DTOs.AI.Phase1;

public class RoleAnalysisDto
{
    public string Summary { get; set; } = string.Empty;

    public List<string> Capabilities { get; set; } = [];

    public List<string> Restrictions { get; set; } = [];

    public List<RiskDto> Risks { get; set; } = [];

    public RiskLevel RiskLevel { get; set; }

    public List<RecommendationDto> Recommendations { get; set; } = [];
}