using Fuel.Manager.Server.Models;

namespace Fuel.Manager.Server.Services.Interfaces
{
    public interface IEmployeeToCarRelationService
    {
        IList<EmployeeToCarRelation> GetAll();
        EmployeeToCarRelation GetById(int id);
        void Create(EmployeeToCarRelation relation);
        void Update(EmployeeToCarRelation relation);
        void Delete(EmployeeToCarRelation relation);
        List<Car> GetCarsByEmployeeId(int id);

    }
}
