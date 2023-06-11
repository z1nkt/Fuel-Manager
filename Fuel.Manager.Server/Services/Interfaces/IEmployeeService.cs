using Fuel.Manager.Server.Models;

namespace Fuel.Manager.Server.Services.Interfaces
{
    public interface IEmployeeService
    {
        IList<Employee> GetAll();
        Employee GetById(int id);
        void Create(Employee employee);
        void Update(Employee employee);
        void Delete(Employee employee);
        Employee Login(string username, string password);
        void ChangePassword(string username, string oldP, string newP);
    }
}
