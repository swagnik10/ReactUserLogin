using Backend.Domain;
using FluentNHibernate.Mapping;

namespace Backend.Mappings;

public class UserRoleMap : ClassMap<UserRole>
{
    public UserRoleMap()
    {
        Schema("auth");
        Table("user_roles");

        CompositeId()
            .KeyProperty(x => x.UserId, x => x.ColumnName("user_id"))
            .KeyProperty(x => x.RoleId, x => x.ColumnName("role_id"));
    }
}
