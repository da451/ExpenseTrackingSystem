using System;
using DAL.Entities;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace DAL
{
    public class FNHHelper
    {
        #region Session

        private ISessionFactory _sessionFactory;

        public ISession GetSession
        {
            get
            {
                if (_instance == null)
                    throw new InvalidOperationException("Singleton not created");
                return _sessionFactory.OpenSession();
            }
        }

        private static FNHHelper _instance = null;

        public static FNHHelper Instance
        {
            get
            {
                if (_instance == null)
                    throw new InvalidOperationException("Singleton not created");
                return _instance;
            }
        }

        private FNHHelper(string connectionString)
        {
            if (_sessionFactory == null)
            {
                if (_sessionFactory == null)
                {
                    var dbConfig = MsSqlCeConfiguration.Standard
                        .ConnectionString(connectionString)
                        .Driver<SqlClientDriver>()
                        .Dialect<MsSqlCeDialect>()
                        .Driver<SqlServerCeDriver>()
                        .ShowSql();

                    _sessionFactory = Fluently.Configure()
                        .Database(dbConfig)
                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<User>())
                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Tag>())
                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Expense>())
                        .ExposeConfiguration(x => x.SetProperty("connection.release_mode", "on_close"))
                        .BuildSessionFactory(); 
                }
            }
        }

        public static FNHHelper CreateInstance(string connectionString)
        {
            if (_instance != null)
            {
                throw new InvalidOperationException("Singleton already created");
            }
            _instance = new FNHHelper(connectionString);

            return _instance;
        }

        #endregion

    }
}

