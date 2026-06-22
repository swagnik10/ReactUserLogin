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
            .Column("user_id")
            .GeneratedBy.Identity();

        Map(x => x.Username).Column("username").Not.Nullable().Length(100);
        Map(x => x.FirstName).Column("first_name").Not.Nullable().Length(100);
        Map(x => x.LastName).Column("last_name").Not.Nullable().Length(100);
        Map(x => x.Email).Column("email").Not.Nullable().Length(255);
        Map(x => x.PhoneNumber).Column("phone_number").Length(20);
        Map(x => x.Password).Column("password").Not.Nullable().Length(255);
        Map(x => x.IsActive).Column("is_active").Not.Nullable();
        Map(x => x.CreatedAt).Column("created_at").Not.Nullable();
    }
}
