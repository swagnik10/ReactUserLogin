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

    public async Task<User?> GetByUsernameAsync(string username)
    {
        try
        {
            _logger.LogInformation("Fetching user from the database.");

            return await _session.Query<User>()
                .FirstOrDefaultAsync(x => x.Username == username);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Database error");

            Console.WriteLine(ex.ToString());

            if (ex.InnerException != null)
            {
                Console.WriteLine("INNER:");
                Console.WriteLine(ex.InnerException.ToString());
            }

            throw;
        }
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        try
        {
            _logger.LogInformation("Fetching user by email from the database.");

            return await _session.Query<User>()
                .FirstOrDefaultAsync(x => x.Email == email);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Database error");

            Console.WriteLine(ex.ToString());

            if (ex.InnerException != null)
            {
                Console.WriteLine("INNER:");
                Console.WriteLine(ex.InnerException.ToString());
            }

            throw;
        }
    }

    public async Task SaveAsync(User user)
    {
        try
        {
            _logger.LogInformation("Save user details in the database...");

            await _session.SaveAsync(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Database error");

            Console.WriteLine(ex.ToString());

            if (ex.InnerException != null)
            {
                Console.WriteLine("INNER:");
                Console.WriteLine(ex.InnerException.ToString());
            }

            throw;
        }
    }

    public async Task<string?> GetRoleNameByUserIdAsync(int userId)
    {
        try
        {
            _logger.LogInformation("Getting Role by the help of userid...");

            var query =
                from ur in _session.Query<UserRole>()
                join r in _session.Query<Role>()
                    on ur.RoleId equals r.RoleId
                where ur.UserId == userId
                select r.Name;

            return await query.FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Database error");

            Console.WriteLine(ex.ToString());

            if (ex.InnerException != null)
            {
                Console.WriteLine("INNER:");
                Console.WriteLine(ex.InnerException.ToString());
            }

            throw;
        }
    }

    public async Task<User?> GetByUseIdAsync(int userId)
    {
        try
        {
            _logger.LogInformation("Checking UserId exists in the database...");

            return await _session.Query<User>()
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Database error");

            Console.WriteLine(ex.ToString());

            if (ex.InnerException != null)
            {
                Console.WriteLine("INNER:");
                Console.WriteLine(ex.InnerException.ToString());
            }

            throw;
        }
    }

    public async Task<List<User>> GetAllAsync()
    {
        try
        {
            _logger.LogInformation("Identifing all the users...");

            return await _session.Query<User>()
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Database error");

            Console.WriteLine(ex.ToString());

            if (ex.InnerException != null)
            {
                Console.WriteLine("INNER:");
                Console.WriteLine(ex.InnerException.ToString());
            }

            throw;
        }
    }

    public async Task UpdateAsync(User user)
    {
        try
        {
            _logger.LogInformation("Update the user...");

            await _session.UpdateAsync(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Database error");

            Console.WriteLine(ex.ToString());

            if (ex.InnerException != null)
            {
                Console.WriteLine("INNER:");
                Console.WriteLine(ex.InnerException.ToString());
            }

            throw;
        }
    }

    public async Task DeleteAsync(User user)
    {
        try
        {
            _logger.LogInformation("Delete the user...");

            await _session.DeleteAsync(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Database error");

            Console.WriteLine(ex.ToString());

            if (ex.InnerException != null)
            {
                Console.WriteLine("INNER:");
                Console.WriteLine(ex.InnerException.ToString());
            }

            throw;
        }
    }
}
