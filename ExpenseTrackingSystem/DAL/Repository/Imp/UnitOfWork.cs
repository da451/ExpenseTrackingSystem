using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace DAL.Repository.Imp
{
    public class UnitOfWork : IUoW
    {
        //private static UnitOfWork _curentUnitOfWork;

        //internal static UnitOfWork CurentUnitOfWork
        //{
        //    get { return _curentUnitOfWork; }
        //}

        private ITransaction _transaction;

        private ISession _session;

        public ISession Session
        {
            get { return _session; }
        }



        public void BeginTransaction()
        {
            _session = FNHHelper.Instance.GetSession;
            _transaction = _session.BeginTransaction();

        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            finally
            {
                Session.Close();
            }
        }

        public void RollBack()
        {
            try
            {
                _transaction.Rollback();
            }
            finally
            {
                Session.Close();
            }
        }
    }
}
