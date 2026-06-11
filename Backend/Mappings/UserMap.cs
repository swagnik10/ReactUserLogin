using Backend.Domain;
using FluentNHibernate.Mapping;

namespace Backend.Mappings;

public class UserMap : ClassMap<User>
{
    public UserMap()
    {
        Schema("auth");
        Table("Users");

        Id(x => x.UserId)
            .Column("UserId")
            .GeneratedBy.Identity();

        Map(x => x.Username).Not.Nullable().Length(100);
        Map(x => x.FirstName).Not.Nullable().Length(100);
        Map(x => x.LastName).Not.Nullable().Length(100);
        Map(x => x.Email).Not.Nullable().Length(255);
        Map(x => x.PhoneNumber).Length(20);
        Map(x => x.Password).Not.Nullable().Length(255);
        Map(x => x.IsActive).Not.Nullable();
        Map(x => x.CreatedAt).Not.Nullable();
    }
}
