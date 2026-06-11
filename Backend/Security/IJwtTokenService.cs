namespace Backend.Secutity;

public interface IJwtTokenService
{
    string GenerateToken(int userId, string userName, string role);
}
