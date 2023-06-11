using System.Reflection;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using NHibernate;
using ISession = NHibernate.ISession;

namespace Fuel.Manager.Server.Helper
{
    public class NHibernateHelper : INHibernateHelper
    {

        private static ISessionFactory mSessionFactory;
        public static readonly string DatabaseFile = "database.db";

        public NHibernateHelper()
        {
            mSessionFactory = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.UsingFile(DatabaseFile).ShowSql())
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly())
                    .Conventions.Add(FluentNHibernate.Conventions.Helpers.DefaultLazy.Never()))
                .BuildSessionFactory();
        }

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (mSessionFactory == null)
                {
                    InitializeSessionFactory();
                }

                return mSessionFactory;
            }
        }

        public ISession OpenSession() => SessionFactory.OpenSession();
       
        private static void InitializeSessionFactory()
        {
            mSessionFactory = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.UsingFile(DatabaseFile).ShowSql())
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly())
                    .Conventions.Add(FluentNHibernate.Conventions.Helpers.DefaultLazy.Never()))
                .BuildSessionFactory();
        }

        public ISessionFactory CreateSession()
        {
            return mSessionFactory;
        }
    }
}

