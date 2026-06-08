namespace Backend.DTOs.Users;

public class UserDto
{
    public int UserId { get; set; }

    public string Username { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public bool IsActive { get; set; }

    public string Role { get; set; }
}
