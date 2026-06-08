using Backend.Application.CommandAndQuery;
using Backend.DTOs.Users;
using Backend.Service;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]


    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IMediator _mediator;

        public UsersController(IUserService userService, ILogger<UsersController> logger,
          IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Fetching all users from the database.");
            var users = await _mediator.Send(new GetAllUsersRequest());

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Fetching user with ID: {UserId} from the database.", id);
            var user = await _mediator.Send(new GetUserByIdRequest(id));

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateUserBody request)
        {
            _logger.LogInformation("Updating user with ID: {UserId} in the database.", id);
            await _mediator.Send(new UpdateUserRequest(id, request));

            return Ok(new
            {
                Message = "User updated successfully"
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting user with ID: {UserId} from the database.", id);
            await _mediator.Send(new DeleteUserRequest(id));

            return Ok(new
            {
                Message = "User deleted successfully"
            });
        }

        [HttpPut("{id}/role")]
        public async Task<IActionResult> UpdateRole(int id, UpdateUserRoleBody request)
        {
            _logger.LogInformation("Updating role for user with ID: {UserId} in the database.", id);
            await _mediator.Send(new UpdateRoleRequest(id, request));

            return Ok(new
            {
                Message = "Role updated successfully"
            });
        }
    }
}
