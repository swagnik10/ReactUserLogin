using Backend.Domain;

namespace Backend.Repositories;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);

    Task<User?> GetByEmailAsync(string email);

    Task SaveAsync(User user);

    Task<string?> GetRoleNameByUserIdAsync(int userId);

    Task<User?> GetByUseIdAsync(int userId);

    Task<List<User>> GetAllAsync();

    Task UpdateAsync(User user);

    Task DeleteAsync(User user);

}
