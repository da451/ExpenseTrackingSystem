using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Repository;
using Iesi.Collections;

namespace DAL.Entities
{
    public class User: IPrivateKey
    {
        public virtual int UserID { protected set; get; }

        public virtual string FIO { set; get; }

        public virtual string Login { set; get; }

        public virtual string Password { set; get; }

        public virtual int PrivateKey
        {
            get { return UserID; }
        }
    }
}
