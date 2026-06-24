namespace Backend.DTOs.Agent;

public class AgentStepDto
{
    public int StepNumber { get; set; }

    public string Action { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Dictionary<string, string> Parameters { get; set; } = [];
}
