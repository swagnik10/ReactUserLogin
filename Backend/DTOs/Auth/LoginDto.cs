namespace Backend.DTOs.Auth;

public class LoginDto
{
    public string Token { get; set; }

    public int UserId { get; set; }

    public string Username { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Role { get; set; }
}
