using Backend.Domain;
using NHibernate.Linq;

namespace Backend.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly NHibernate.ISession _session;
    private readonly ILogger<RoleRepository> _logger;

    public RoleRepository(NHibernate.ISession session, ILogger<RoleRepository> logger)
    {
        _session = session;
        _logger = logger;
    }

    public async Task<Role?> GetByNameAsync(string roleName)
    {
        try
        {
            _logger.LogInformation("Fetching all roles from the database.");

            return await _session.Query<Role>()
                .FirstOrDefaultAsync(
                    x => x.Name == roleName);
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

    public async Task<Role?> GetByRoleIdAsync(int roleId)
    {
        try
        {
            _logger.LogInformation("Fetching roleid details from the database.");

            return await _session.Query<Role>()
                .FirstOrDefaultAsync(x => x.RoleId == roleId);
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
