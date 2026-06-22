using Backend.Domain;
using FluentNHibernate.Mapping;

namespace Backend.Mappings;

public class RoleMap : ClassMap<Role>
{
    public RoleMap()
    {
        Schema("auth");
        Table("Roles");

        Id(x => x.RoleId)
            .Column("role_id")
            .GeneratedBy.Identity();

        Map(x => x.Name)
            .Column("name")
            .Not.Nullable()
            .Length(50);
    }
}
