using Fuel.Manager.Server.Helper;
using Fuel.Manager.Server.Repositories.Interfaces;
using Microsoft.VisualBasic;
using NHibernate.Criterion;
using NHibernate;

namespace Fuel.Manager.Server.Repositories.Implementation
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        private ISessionFactory _nhibernateHelper;

        public BaseRepository(ISessionFactory nhibernateHelper)
        {
            _nhibernateHelper = nhibernateHelper;
        }

        public IList<T> GetAll()
        {
            using var session = _nhibernateHelper.OpenSession();
            return session.Query<T>().ToList();
        }

        public virtual T GetById(int id)
        {
            using (var session = _nhibernateHelper.OpenSession())
            {
                return session.CreateCriteria<T>().Add(Restrictions.Eq("Id", id)).List<T>().First<T>();
            }
        }

        public virtual void Create(T entity)
        { 
            using var session = _nhibernateHelper.OpenSession();
            using var transaction = session.BeginTransaction();
            session.Save(entity);
            transaction.Commit();
        }

        public void Update(T entity)
        {
            using var session = _nhibernateHelper.OpenSession();
            using var transaction = session.BeginTransaction();
            session.SaveOrUpdate(entity);
            transaction.Commit();
        }

        public void Delete(T entity)
        {
            using var session = _nhibernateHelper.OpenSession();
            using var transaction = session.BeginTransaction();
            session.Delete(entity);
            transaction.Commit();
        }
    }
}
