using Backend.DTOs.Users;
using Backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]


    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Fetching all users from the database.");
            var users =
                await _userService.GetAllUsersAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Fetching user with ID: {UserId} from the database.", id);
            var user =
                await _userService
                    .GetUserByIdAsync(id);

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateUserRequest request)
        {
            _logger.LogInformation("Updating user with ID: {UserId} in the database.", id);
            await _userService.UpdateUserAsync(id, request);

            return Ok(new
            {
                Message = "User updated successfully"
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting user with ID: {UserId} from the database.", id);
            await _userService.DeleteUserAsync(id);

            return Ok(new
            {
                Message = "User deleted successfully"
            });
        }

        [HttpPut("{id}/role")]
        public async Task<IActionResult> UpdateRole(int id, UpdateUserRoleRequest request)
        {
            _logger.LogInformation("Updating role for user with ID: {UserId} in the database.", id);
            await _userService.UpdateUserRoleAsync(id, request);

            return Ok(new
            {
                Message = "Role updated successfully"
            });
        }
    }
}
