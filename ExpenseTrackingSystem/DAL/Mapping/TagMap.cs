using DAL.Entities;
using FluentNHibernate.Mapping;

namespace DAL.Mapping
{
    public class TagMap : ClassMap<Tag>
    {
        public TagMap()
        {
            Table("TAGS");

            Id(x => x.TagID).Column("TAG_ID");

            Map(x => x.Name).Column("NAME");

            References<User>(x => x.User).Column("USER_ID");
        }
    }
}
