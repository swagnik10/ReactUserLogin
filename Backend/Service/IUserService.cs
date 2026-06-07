using Backend.DTOs.Auth;
using Backend.DTOs.Users;
using System.Security.Claims;

namespace Backend.Service;

public interface IUserService
{
    Task RegisterAsync(RegisterUserRequest request);
    Task<LoginResponse> LoginAsync(LoginRequest request);

    Task<CurrentUserResponse> GetCurrentUserAsync(ClaimsPrincipal principal);

    Task<List<UserResponse>> GetAllUsersAsync();

    Task<UserDetailsResponse> GetUserByIdAsync(int userId);

    Task UpdateUserAsync(int userId, UpdateUserRequest request);

    Task DeleteUserAsync(int userId);

    Task UpdateUserRoleAsync(int userId, UpdateUserRoleRequest request);
}
