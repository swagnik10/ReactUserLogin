using Backend.Domain;

namespace Backend.Repositories;

public interface IRoleRepository
{
    Task<Role?> GetByNameAsync(string roleName);

    Task<Role?> GetByRoleIdAsync(int roleId);
}
