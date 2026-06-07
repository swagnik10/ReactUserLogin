namespace Backend.DTOs.Auth;

public class CurrentUserResponse
{
    public int UserId { get; set; }

    public string Username { get; set; }

    public string Role { get; set; }
}
