using AutoMapper;
using Backend.DbConnection;
using Backend.Domain;
using Backend.DTOs.Auth;
using Backend.DTOs.Users;
using Backend.Repositories;
using Backend.Secutity;
using System.Security.Claims;

namespace Backend.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IUnitOfWorkFactory _uowFactory;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, 
                    IUnitOfWorkFactory uowFactory, 
                    IMapper mapper,
                    IRoleRepository roleRepository,
                    IUserRoleRepository userRoleRepository,
                    IJwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _uowFactory = uowFactory;
        _mapper = mapper;
        _roleRepository = roleRepository;
        _userRoleRepository = userRoleRepository;
        _jwtTokenService = jwtTokenService;
    }
    public async Task RegisterAsync(RegisterUserRequest request)
    {
        using var uow = _uowFactory.Create();

        uow.BeginTransaction();

        try
        {
            var existingUsername = await _userRepository.GetByUsernameAsync(request.Username);

            if (existingUsername != null)
            {
                throw new Exception("Username already exists.");
            }

            var existingEmail = await _userRepository.GetByEmailAsync(request.Email);

            if (existingEmail != null)
            {
                throw new Exception("Email already exists.");
            }

            var user = _mapper.Map<User>(request);

            user.IsActive = true;
            user.CreatedAt = DateTime.Now;

            await _userRepository.SaveAsync(user);

            var userRole = await _roleRepository.GetByNameAsync("User");

            if (userRole == null)
            {
                throw new Exception(
                    "Default role not found.");
            }

            await _userRoleRepository.SaveAsync(
                new UserRole
                {
                    UserId = user.UserId,
                    RoleId = userRole.RoleId
                });

            await uow.CommitAsync();
        }
        catch
        {
            await uow.RollbackAsync();
            throw;
        }
    }

    public async Task<LoginResponse> LoginAsync(
    LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user == null)
        {
            throw new Exception("Invalid credentials.");
        }

        if (user.Password != request.Password)
        {
            throw new Exception("Invalid credentials.");
        }

        if (!user.IsActive)
        {
            throw new Exception("User is inactive.");
        }

        var role = await _userRepository.GetRoleNameByUserIdAsync(user.UserId);

        var token =
            _jwtTokenService.GenerateToken(
                user.UserId,
                user.Username,
                role ?? "User");

        return new LoginResponse
        {
            Token = token,
            UserId = user.UserId,
            Username = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Role = role ?? "User"
        };
    }

    public async Task<CurrentUserResponse> GetCurrentUserAsync(ClaimsPrincipal principal)
    {
        var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
        {
            throw new Exception(
                "User not found.");
        }

        int userId = int.Parse(userIdClaim.Value);

        var role = principal.FindFirst(ClaimTypes.Role)?.Value;

        var user = await _userRepository.GetByUseIdAsync(userId);

        if (user == null)
        {
            throw new Exception(
                "User not found.");
        }

        return new CurrentUserResponse
        {
            UserId = user.UserId,
            Username = user.Username,
            Role = role ?? ""
        };
    }

    public async Task<List<UserResponse>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();

        var result = new List<UserResponse>();

        foreach (var user in users)
        {
            var role = await _userRepository.GetRoleNameByUserIdAsync(user.UserId);

            result.Add(new UserResponse
            {
                UserId = user.UserId,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsActive = user.IsActive,
                Role = role ?? ""
            });
        }

        return result;
    }

    public async Task<UserDetailsResponse> GetUserByIdAsync(int userId)
    {
        var user = await _userRepository.GetByUseIdAsync(userId);

        if (user == null)
        {
            throw new Exception(
                "User not found.");
        }

        var role = await _userRepository.GetRoleNameByUserIdAsync(user.UserId);

        return new UserDetailsResponse
        {
            UserId = user.UserId,
            Username = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt,
            Role = role ?? ""
        };
    }

    public async Task UpdateUserAsync(int userId, UpdateUserRequest request)
    {
        using var uow = _uowFactory.Create();

        uow.BeginTransaction();

        try
        {
            var user = await _userRepository.GetByUseIdAsync(userId);

            if (user == null)
            {
                throw new Exception(
                    "User not found.");
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            user.IsActive = request.IsActive;

            await _userRepository.UpdateAsync(user);

            await uow.CommitAsync();
        }
        catch
        {
            await uow.RollbackAsync();
            throw;
        }
    }

    public async Task DeleteUserAsync(int userId)
    {
        using var uow = _uowFactory.Create();

        uow.BeginTransaction();

        try
        {
            var user = await _userRepository.GetByUseIdAsync(userId);

            if (user == null)
            {
                throw new Exception(
                    "User not found.");
            }
            await _userRoleRepository.DeleteByUserIdAsync(userId);
            await _userRepository.DeleteAsync(user);

            await uow.CommitAsync();
        }
        catch
        {
            await uow.RollbackAsync();
            throw;
        }
    }

    public async Task UpdateUserRoleAsync(
    int userId,
    UpdateUserRoleRequest request)
    {
        using var uow = _uowFactory.Create();

        uow.BeginTransaction();

        try
        {
            var user = await _userRepository.GetByUseIdAsync(userId);

            if (user == null)
            {
                throw new Exception(
                    "User not found.");
            }

            var role = await _roleRepository.GetByNameAsync(request.RoleName);

            if (role == null)
            {
                throw new Exception(
                    "Role not found.");
            }

            var userRole = await _userRoleRepository.GetByUserIdAsync(userId);

            if (userRole == null)
            {
                throw new Exception(
                    "User role not found.");
            }

            await _userRoleRepository.DeleteAsync(userRole);

            await _userRoleRepository.SaveAsync(
                new UserRole
                {
                    UserId = userId,
                    RoleId = role.RoleId
                });

            await uow.CommitAsync();
        }
        catch
        {
            await uow.RollbackAsync();
            throw;
        }
    }
}
