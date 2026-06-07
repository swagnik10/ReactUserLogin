using Backend.Domain;
using NHibernate.Linq;

namespace Backend.Repositories;

public class UserRoleRepository : IUserRoleRepository
{
    private readonly NHibernate.ISession _session;

    public UserRoleRepository(
        NHibernate.ISession session)
    {
        _session = session;
    }

    public async Task SaveAsync(UserRole userRole)
    {
        await _session.SaveAsync(userRole);
    }

    public async Task DeleteByUserIdAsync(int userId)
    {
        var userRoles =
            await _session.Query<UserRole>()
                .Where(x => x.UserId == userId)
                .ToListAsync();

        foreach (var userRole in userRoles)
        {
            await _session.DeleteAsync(userRole);
        }
    }

    public async Task<UserRole?> GetByUserIdAsync(int userId)
    {
        return await _session.Query<UserRole>()
            .FirstOrDefaultAsync(x => x.UserId == userId);
    }

    public async Task UpdateAsync(UserRole userRole)
    {
        await _session.UpdateAsync(userRole);
    
    }

    public async Task DeleteAsync(UserRole userRole)
    {
        await _session.DeleteAsync(userRole);
    }
}
