using NHibernate;

namespace Fuel.Manager.Server.Helper
{
    public interface INHibernateHelper
    {
        public NHibernate.ISession OpenSession();
    }
}
