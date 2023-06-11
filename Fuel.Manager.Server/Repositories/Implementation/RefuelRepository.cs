using Fuel.Manager.Server.Helper;
using Fuel.Manager.Server.Models;
using Fuel.Manager.Server.Repositories.Interfaces;
using NHibernate;

namespace Fuel.Manager.Server.Repositories.Implementation
{
    public class RefuelRepository : BaseRepository<Refuel>, IRefuelRepository
    {
        private ISessionFactory _nhibernateHelper;
        public RefuelRepository(ISessionFactory nhibernateHelper) : base(nhibernateHelper)
        {
            _nhibernateHelper = nhibernateHelper;
        }

        public IList<Refuel> GetRefuelsByCar(Car car)
        {
            throw new NotImplementedException();
        }
    }
}
