using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Repository;
using DAL.Repository.Imp;

namespace DAL
{
    class Class1
    {
        public static void Main(string[] args)
        {
            string str =
                @"Data Source = C:\Documents and Settings\Jim\Мои документы\Visual Studio 2005\Projects\TEST\ExpenseTrackingSystem\ExpenseTrackingSystem\MyDB.sdf";
            FNHHelper.CreateInstance(str);

            UnitOfWork uow = new UnitOfWork();
            uow.BeginTransaction();
            RepositoryUser r = new RepositoryUser(uow);
            var fl = r.LogIn("da451", "451452");
            try
            {
                r.Registration("da451", "da451", "451451");

                uow.Commit();

            }
            catch (Exception ex)
            {
                uow.RollBack();
            }


        }
    }
}
