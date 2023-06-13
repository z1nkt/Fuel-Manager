using Fuel.Manager.Server.Helper;
using Fuel.Manager.Server.Models;
using Fuel.Manager.Server.Repositories.Interfaces;
using NHibernate;

namespace Fuel.Manager.Server.Repositories.Implementation
{
    public class CarRepository : BaseRepository<Car>, ICarRepository
    {
        private ISessionFactory _nhibernateHelper;

        public CarRepository(ISessionFactory nhibernateHelper) : base(nhibernateHelper)
        {
            _nhibernateHelper = nhibernateHelper;
        }

    }
}
