using Fuel.Manager.Server.Models;

namespace Fuel.Manager.Server.Services.Interfaces
{
    public interface ICarService
    {
        IList<Car> GetAll();
        Car GetById(int id);
        void Create(Car car);
        void Update(Car car);
        void Delete(Car car);
    }
}
