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

        public override void Create(Car entity)
        {
            entity.LicensePlate = entity.LicensePlate.ToUpper();
            base.Create(entity);
        }

        public Car FindByLicensePlate(string licensePlate)
        {
            using var session = _nhibernateHelper.OpenSession();
            return session.Query<Car>().FirstOrDefault(v => v.LicensePlate.ToUpper() == licensePlate.ToUpper());
        }
    }
}
