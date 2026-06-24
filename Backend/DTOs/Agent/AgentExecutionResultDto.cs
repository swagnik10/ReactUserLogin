namespace Backend.DTOs.Agent;

public class AgentExecutionResultDto
{
    public bool Success { get; set; }

    public List<string> Messages { get; set; } = [];
}