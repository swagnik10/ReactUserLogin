namespace Backend.DTOs.AI;

public class RoleComparisonDto
{
    public string Summary { get; set; } = "";

    public List<string> Similarities { get; set; } = [];

    public List<string> Differences { get; set; } = [];

    public List<string> PermissionsOnlyInRoleA { get; set; } = [];

    public List<string> PermissionsOnlyInRoleB { get; set; } = [];

    public List<string> RecommendedUseCases { get; set; } = [];

    public List<string> SecurityImplications { get; set; } = [];
}
