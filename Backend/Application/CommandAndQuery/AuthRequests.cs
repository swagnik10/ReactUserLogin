using Backend.DTOs.Auth;
using MediatR;

namespace Backend.Application.CommandAndQuery;

public class AuthRequests
{
    public record RegisterUserRequest(RegisterUserBody RegisterUserBody) : IRequest;
    public record LoginRequest(LoginBody LoginBody) :IRequest<LoginDto>;
}
