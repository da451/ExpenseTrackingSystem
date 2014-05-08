using DAL.Entities;
using FluentNHibernate.Mapping;

namespace DAL.Mapping
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("USERS");

            Id(x => x.UserID).Column("USER_ID");

            Map(x => x.FIO).Column("FIO");

            Map(x => x.Login).Column("LOGIN");

            Map(x => x.Password).Column("PASSWORD");

            HasMany<Tag>(x => x.Tags)
                .KeyColumn("USER_ID")
                .Inverse().AsBag();
        }
    }
}
