using Backend.Application.Agents;
using Backend.Application.CommandAndQuery;
using Backend.DbConnection;
using Backend.DTOs.Agent;
using Backend.DTOs.Auth;
using Backend.DTOs.Users;
using MediatR;
using static Backend.Application.CommandAndQuery.AuthRequests;

namespace Backend.Application.Handlers.AgentHandler;

public class ExecutePlanHandler : IRequestHandler<ExecutePlanRequest, AgentExecutionResultDto>
{
    private readonly IMediator _mediator;
    private readonly ILogger<ExecutePlanHandler> _logger;
    private readonly IUnitOfWorkFactory _uowFactory;

    public ExecutePlanHandler(
        IMediator mediator,
        ILogger<ExecutePlanHandler> logger,
        IUnitOfWorkFactory uowFactory)
    {
        _mediator = mediator;
        _logger = logger;
        _uowFactory = uowFactory;
    }

    public async Task<AgentExecutionResultDto> Handle(ExecutePlanRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("ExecutePlanHandler started. Total Steps: {StepCount}", request.Plan.Steps.Count);

        var result = new AgentExecutionResultDto
        {
            Success = true
        };

        int createdUserId = 0;

        using var uow = _uowFactory.Create();

        uow.BeginTransaction();

        try
        {
            foreach (var step in request.Plan.Steps)
            {
                _logger.LogInformation("Executing action: {Action}", step.Action);

                switch (step.Action)
                {
                    case AgentActions.CreateUser:

                        _logger.LogInformation("Creating user. Username: {Username}, Email: {Email}",
                            step.Parameters["Username"],
                            step.Parameters["Email"]);

                        var registerBody = new RegisterUserBody
                        {
                            FirstName = step.Parameters["FirstName"],
                            LastName = step.Parameters["LastName"],
                            Email = step.Parameters["Email"],
                            Username = step.Parameters["Username"],
                            Password = step.Parameters["Password"],
                            PhoneNumber = step.Parameters["PhoneNumber"]
                        };

                        var createdUser = await _mediator.Send(new RegisterUserRequest(registerBody), cancellationToken);

                        createdUserId = createdUser.UserId;

                        _logger.LogInformation("User created successfully. UserId: {UserId}, Username: {Username}",
                            createdUser.UserId,
                            createdUser.Username);

                        result.Messages.Add($"User '{createdUser.Username}' created successfully.");

                        break;

                    case AgentActions.AssignRole:

                        if (createdUserId == 0)
                        {
                            _logger.LogError("AssignRole attempted before CreateUser.");

                            throw new InvalidOperationException("Cannot assign role before creating a user.");
                        }

                        var roleName = step.Parameters["RoleName"];

                        _logger.LogInformation("Assigning role '{RoleName}' to UserId {UserId}",
                            roleName,
                            createdUserId);

                        await _mediator.Send(
                            new UpdateRoleRequest(
                                createdUserId,
                                new UpdateUserRoleBody
                                {
                                    RoleName = roleName
                                }),
                            cancellationToken);

                        _logger.LogInformation("Role '{RoleName}' assigned successfully to UserId {UserId}",
                            roleName,
                            createdUserId);

                        result.Messages.Add($"Role '{roleName}' assigned successfully.");

                        break;

                    default:

                        _logger.LogWarning(
                            "Unsupported action encountered: {Action}",
                            step.Action);

                        throw new InvalidOperationException(
                            $"Unsupported action '{step.Action}'.");
                }
            }

            await uow.CommitAsync();

            _logger.LogInformation("ExecutePlanHandler completed successfully.");

            return result;
        }
        catch (Exception ex)
        {
            await uow.RollbackAsync();

            _logger.LogError(ex, "ExecutePlanHandler failed.");

            throw;
        }
    }
}