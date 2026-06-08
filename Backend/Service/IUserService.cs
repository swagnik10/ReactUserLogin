using Backend.DTOs.Auth;
using Backend.DTOs.Users;
using System.Security.Claims;

namespace Backend.Service;

public interface IUserService
{
    Task RegisterAsync(RegisterUserRequest request);
    Task<LoginDto> LoginAsync(LoginRequest request);

    Task<CurrentUserResponse> GetCurrentUserAsync(ClaimsPrincipal principal);

    Task<List<UserDto>> GetAllUsersAsync();

    Task<UserDetailsDto> GetUserByIdAsync(int userId);

    Task UpdateUserAsync(int userId, UpdateUserBody request);

    Task DeleteUserAsync(int userId);

    Task UpdateUserRoleAsync(int userId, UpdateUserRoleBody request);
}
