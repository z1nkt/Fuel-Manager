using Fuel.Manager.Server.Models;

namespace Fuel.Manager.Server.Repositories.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        public Employee GetByUsername(string username);
    }
}
