using Backend.Domain;
using NHibernate.Linq;

namespace Backend.Repositories;

public class UserRoleRepository : IUserRoleRepository
{
    private readonly NHibernate.ISession _session;
    private readonly ILogger<UserRoleRepository> _logger;

    public UserRoleRepository(NHibernate.ISession session, ILogger<UserRoleRepository> logger)
    {
        _session = session;
        _logger = logger;
    }

    public async Task SaveAsync(UserRole userRole)
    {
        try
        {
            _logger.LogInformation("Save user role details in the database...");

            await _session.SaveAsync(userRole);
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

    public async Task DeleteByUserIdAsync(int userId)
    {
        try
        {
            _logger.LogInformation("Save user role details in the database...");

            var userRoles = await _session.Query<UserRole>()
                                .Where(x => x.UserId == userId)
                                .ToListAsync();

            foreach (var userRole in userRoles)
            {
                await _session.DeleteAsync(userRole);
            }
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

    public async Task<UserRole?> GetByUserIdAsync(int userId)
    {
        try
        {
            _logger.LogInformation("Get role of that userid...");

            return await _session.Query<UserRole>()
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

    public async Task UpdateAsync(UserRole userRole)
    {
        try
        {
            _logger.LogInformation("Update the user's role...");

            await _session.UpdateAsync(userRole);
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

    public async Task DeleteAsync(UserRole userRole)
    {
        try
        {
            _logger.LogInformation("Delete the user's role...");

            await _session.DeleteAsync(userRole);
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
