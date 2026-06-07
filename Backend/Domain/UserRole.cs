namespace Backend.Domain;

public class UserRole
{
    public virtual int UserId { get; set; }

    public virtual int RoleId { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not UserRole other)
            return false;

        return Equals(UserId, other.UserId)
            && Equals(RoleId, other.RoleId);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(UserId, RoleId);
    }
}
