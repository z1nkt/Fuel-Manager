using Fuel.Manager.Server.Models;
using Fuel.Manager.Server.Repositories.Interfaces;
using Fuel.Manager.Server.Services.Interfaces;

namespace Fuel.Manager.Server.Services.Implementation
{
    public class RefuelService : IRefuelService
    {
        private IRefuelRepository _refuelRepository;
        public RefuelService(IRefuelRepository refuelRepository)
        {
            _refuelRepository = refuelRepository;
        }

        public void Create(Refuel refuel)
        {
            _refuelRepository.Create(refuel);
        }

        public void Delete(Refuel refuel)
        {
            _refuelRepository.Delete(refuel);
        }

        public IList<Refuel> GetAll()
        {
            return _refuelRepository.GetAll();
        }

        public Refuel GetById(int id)
        {
            return _refuelRepository.GetById(id);
        }

        public IList<Refuel> GetRefuelsByCar(Car car)
        {
            return _refuelRepository.GetRefuelsByCar(car);
        }

        public void Update(Refuel refuel)
        {
            _refuelRepository.Update(refuel);
        }
    }
}
