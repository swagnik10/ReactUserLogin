using Backend.Domain;
using FluentNHibernate.Mapping;

namespace Backend.Mappings;

public class UserRoleMap : ClassMap<UserRole>
{
    public UserRoleMap()
    {
        Schema("auth");
        Table("UserRoles");

        CompositeId()
            .KeyProperty(x => x.UserId)
            .KeyProperty(x => x.RoleId);
    }
}
