using Fuel.Manager.Server.Models;

namespace Fuel.Manager.Server.Services.Interfaces
{
    public interface IRefuelService
    {
        IList<Refuel> GetAll();
        Refuel GetById(int id);
        void Create(Refuel refuel);
        void Update(Refuel refuel);
        void Delete(Refuel refuel);
        IList<Refuel> GetRefuelsByCar(Car car);
    }
}
