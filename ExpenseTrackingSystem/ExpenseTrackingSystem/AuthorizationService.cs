using System;
using System.Threading;
using DAL.Repository.Imp;

namespace ExpenseTrackingSystem
{
    class AuthorizationService
    {
        public static int LogIn(string login, string password)
        {
            UnitOfWork uow = new UnitOfWork();

            int userID;
            try
            {
                uow.BeginTransaction();

                RepositoryUser repositoryUser = new RepositoryUser(uow);

                userID = repositoryUser.LogIn(login, password);

                uow.Commit();

                if (userID > 0)
                {
                    Principal principal = Thread.CurrentPrincipal as Principal;
                    if (principal == null)
                        throw new ArgumentException("Check start up method. Principal must be init.");

                    principal.Identity = new Identity(userID, login);
                }
            }
            catch 
            {
                uow.RollBack();
                throw;
            }
            return userID;
        }

        public static bool RegisterUser(string fio, string login, string password)
        {
            UnitOfWork uow = new UnitOfWork();

            bool isRegistrated = false;
            try
            {
                uow.BeginTransaction();

                RepositoryUser repositoryUser = new RepositoryUser(uow);

                isRegistrated = repositoryUser.Registration(fio, login, password);

                uow.Commit();
            }
            catch
            {
                uow.RollBack();
                throw;
            }
            return isRegistrated;
        }

        public static bool IsValidLogin(string login)
        {
            UnitOfWork uow = new UnitOfWork();

            bool isValidLogin = false;

            try
            {
                uow.BeginTransaction();

                RepositoryUser repositoryUser = new RepositoryUser(uow);

                isValidLogin = repositoryUser.IsValidLogin(login);

                uow.Commit();
            }
            catch
            {
                uow.RollBack();
                throw;
            }

            return isValidLogin;
        }

        public static int GetUserID()
        {
            Principal principal = Thread.CurrentPrincipal as Principal;

            if (principal == null)
                throw new ArgumentException("User not logged!");

            Identity identety = principal.Identity as Identity;

            if (identety == null)
                throw new ArgumentException("User not logged!");

            return identety.UserID;

        }

    }
}
