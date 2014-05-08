using System.Linq;
using DAL.Entities;
using DAL.Util;
using NHibernate.Criterion;

namespace DAL.Repository.Imp
{
    public class RepositoryUser: RepositoryBase<User>
    {
        public RepositoryUser(UnitOfWork uow) : base(uow)
        {
        }

        public bool Registration(string fio, string login, string password)
        {
            if (IsValidLogin(login))
            {
                User newUser = new User()
                {
                    FIO = fio,
                    Login = login,
                    Password = EncryptUtil.EncodePassword(password)
                };
                this.Save(newUser);

                return true;
            }
            return false;
        }

        public bool IsValidLogin(string login)
        {
            return !_unitOfWork.Session.CreateCriteria<User>()
                .Add(Restrictions.Eq("Login", login)).List<User>().Any(); 
        }

        public int LogIn(string login, string password)
        {
            var user = _unitOfWork.Session.CreateCriteria<User>()
                .Add(Restrictions.And(
                    Restrictions.Eq("Login", login),
                    Restrictions.Eq("Password", EncryptUtil.EncodePassword(password)))).SetMaxResults(1)
                .UniqueResult<User>();
            var ex = _unitOfWork.Session.CreateCriteria<Expense>().List<Expense>();
            return user != null ? user.UserID : -1;
        }
    }
}
