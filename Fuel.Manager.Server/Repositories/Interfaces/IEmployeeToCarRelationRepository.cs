using Fuel.Manager.Server.Models;

namespace Fuel.Manager.Server.Repositories.Interfaces
{
    public interface IEmployeeToCarRelationRepository : IRepository<EmployeeToCarRelation>
    {
        List<EmployeeToCarRelation> GetByEmployee (Employee employee);
    }
}
