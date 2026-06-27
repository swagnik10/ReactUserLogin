namespace Backend.DTOs.AI.Phase3;

public class AskRbacQuestionResponse
{
    public string Answer { get; set; } = string.Empty;

    public List<AuditFindingDto> Findings { get; set; } = [];

    public List<AuditRecommendationDto> Recommendations { get; set; } = [];
}
