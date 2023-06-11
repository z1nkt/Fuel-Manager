using Fuel.Manager.Server.Models;

namespace Fuel.Manager.Server.Repositories.Interfaces
{
    public interface IRefuelRepository : IRepository<Refuel>
    {
        public IList<Refuel> GetRefuelsByCar(Car car);
    }
}
