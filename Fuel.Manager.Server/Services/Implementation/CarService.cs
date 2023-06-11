using Fuel.Manager.Server.Models;
using Fuel.Manager.Server.Repositories.Interfaces;
using Fuel.Manager.Server.Services.Interfaces;

namespace Fuel.Manager.Server.Services.Implementation
{
    public class CarService : ICarService
    {
        private ICarRepository _carRepository;
        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public void Create(Car car)
        {
            _carRepository.Create(car);
        }

        public void Delete(Car car)
        {
            _carRepository.Delete(car);
        }

        public IList<Car> GetAll()
        {
            return _carRepository.GetAll();
        }

        public Car GetById(int id)
        {
            return _carRepository.GetById(id);
        }

        public void Update(Car car)
        {
            _carRepository.Update(car);
        }

    }
}
