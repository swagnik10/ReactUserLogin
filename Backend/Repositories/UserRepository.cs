using AutoMapper;
using Backend.Domain;
using NHibernate.Linq;

namespace Backend.Repositories;

public class UserRepository : IUserRepository
{
    private readonly NHibernate.ISession _session;
    private readonly ILogger<UserRepository> _logger;
    public UserRepository(NHibernate.ISession session, ILogger<UserRepository> logger)
    {
        _session = session;
        _logger = logger;
    }

    //public async Task GetUserDetailsAsync()
    //{
    //    try
    //    {
    //        _logger.LogInformation("Fetching user from the database.");
    //        List<User> users = await _session.Query<User>().ToListAsync();
    //        List<Role> role = await _session.Query<Role>().ToListAsync();
    //        List<UserRole> userRoles = await _session.Query<UserRole>().ToListAsync();
    //        _logger.LogInformation("Fetching completed");
    //        return;
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Database error");

    //        Console.WriteLine(ex.ToString());

    //        if (ex.InnerException != null)
    //        {
    //            Console.WriteLine("INNER:");
    //            Console.WriteLine(ex.InnerException.ToString());
    //        }

    //        throw;
    //    }
    //}

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _session.Query<User>()
            .FirstOrDefaultAsync(x => x.Username == username);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _session.Query<User>()
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task SaveAsync(User user)
    {
        await _session.SaveAsync(user);
    }

    public async Task<string?> GetRoleNameByUserIdAsync(int userId)
    {
        var query =
            from ur in _session.Query<UserRole>()
            join r in _session.Query<Role>()
                on ur.RoleId equals r.RoleId
            where ur.UserId == userId
            select r.Name;

        return await query.FirstOrDefaultAsync();
    }

    public async Task<User?> GetByUseIdAsync(int userId)
    {
        return await _session.Query<User>()
            .FirstOrDefaultAsync(x => x.UserId == userId);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _session.Query<User>()
            .ToListAsync();
    }

    public async Task UpdateAsync(User user)
    {
        await _session.UpdateAsync(user);
    }

    public async Task DeleteAsync(User user)
    {

        await _session.DeleteAsync(user);
    }
}
