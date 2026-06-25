using Backend.DTOs.Auth;
using Backend.DTOs.Users;
using MediatR;

namespace Backend.Application.CommandAndQuery;

public class AuthRequests
{
    public record RegisterUserRequest(RegisterUserBody RegisterUserBody) : IRequest<CreatedUserDto>;
    public record LoginRequest(LoginBody LoginBody) :IRequest<LoginDto>;
}
