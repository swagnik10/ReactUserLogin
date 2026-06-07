namespace Backend.Domain;

public class User
{
    public virtual int UserId { get; set; }

    public virtual string Username { get; set; }

    public virtual string FirstName { get; set; }

    public virtual string LastName { get; set; }

    public virtual string Email { get; set; }

    public virtual string PhoneNumber { get; set; }

    public virtual string Password { get; set; }

    public virtual bool IsActive { get; set; }

    public virtual DateTime CreatedAt { get; set; }
}
