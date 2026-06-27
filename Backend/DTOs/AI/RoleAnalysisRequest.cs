namespace Backend.DTOs.AI;

public class RoleAnalysisRequest
{
    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

    public List<string> GrantedPermissions { get; set; } = [];

    public List<string> DeniedPermissions { get; set; } = [];
}
