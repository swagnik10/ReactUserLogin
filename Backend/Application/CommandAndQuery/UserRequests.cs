using Backend.DTOs.Users;
using MediatR;

namespace Backend.Application.CommandAndQuery;

public record GetUserByIdRequest(int UserId) : IRequest<UserDetailsDto>;

public record GetAllUsersRequest() : IRequest<List<UserDto>>;

public record UpdateUserRequest(int UserId, UpdateUserBody UserBody) : IRequest;

public record DeleteUserRequest(int UserId) : IRequest;

public record UpdateRoleRequest(int UserId, UpdateUserRoleBody RoleBody) : IRequest;
