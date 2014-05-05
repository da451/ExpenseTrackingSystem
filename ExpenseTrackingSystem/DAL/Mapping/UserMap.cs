using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using DAL.Entities;
using FluentNHibernate.Mapping;
using NHibernate.Mapping;

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
        }
    }
}
