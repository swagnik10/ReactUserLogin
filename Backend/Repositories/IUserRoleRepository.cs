using Backend.Domain;

namespace Backend.Repositories;

public interface IUserRoleRepository
{
    Task SaveAsync(UserRole userRole);

    Task DeleteByUserIdAsync(int userId);

    Task<UserRole?> GetByUserIdAsync(int userId);

    Task UpdateAsync(UserRole userRole);

    Task DeleteAsync(UserRole userRole);
}
