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
            .Column("RoleId")
            .GeneratedBy.Identity();

        Map(x => x.Name)
            .Not.Nullable()
            .Length(50);
    }
}
