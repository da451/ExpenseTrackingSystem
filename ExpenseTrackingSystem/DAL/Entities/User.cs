using System.Collections.Generic;

namespace DAL.Entities
{
    public class User: IPrivateKey
    {
        public User()
        {
            Tags = new List<Tag>();
        }

        public User(int userId, string fio, string login, string password)
        {
            UserID = userId;
            FIO = fio;
            Login = login;
            Password = password;
        }

        public virtual int UserID { protected set; get; }

        public virtual string FIO { set; get; }

        public virtual string Login { set; get; }

        public virtual string Password { set; get; }

        public virtual IList<Tag> Tags { set; get; }

        public virtual int PrivateKey
        {
            get { return UserID; }
        }
    }
}
