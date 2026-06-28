namespace Backend.DTOs.AI.Phase3;

public class AuditScoreDto
{
    public int Security { get; set; }

    public int Maintainability { get; set; }

    public int LeastPrivilege { get; set; }

    public int Consistency { get; set; }

    public int Overall { get; set; }
}
