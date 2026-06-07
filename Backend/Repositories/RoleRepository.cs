using Backend.Domain;
using NHibernate.Linq;

namespace Backend.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly NHibernate.ISession _session;

    public RoleRepository(
        NHibernate.ISession session)
    {
        _session = session;
    }

    public async Task<Role?> GetByNameAsync(string roleName)
    {
        return await _session.Query<Role>()
            .FirstOrDefaultAsync(
                x => x.Name == roleName);
    }

    public async Task<Role?> GetByRoleIdAsync(int roleId)
    {
        return await _session.Query<Role>()
            .FirstOrDefaultAsync(x => x.RoleId == roleId);
    }
}
