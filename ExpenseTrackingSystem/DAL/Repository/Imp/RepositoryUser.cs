using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Entities;
using DAL.Util;
using FluentNHibernate.Conventions;
using NHibernate.Criterion;
using NHibernate.Util;

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

        public bool LogIn(string login, string password)
        {
            return _unitOfWork.Session.CreateCriteria<User>()
                .Add(Restrictions.And(
                    Restrictions.Eq("Login", login), 
                    Restrictions.Eq("Password", EncryptUtil.EncodePassword(password)))).List().Any();
        }
    }
}
