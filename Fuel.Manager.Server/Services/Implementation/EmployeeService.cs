using Fuel.Manager.Server.Models;
using Fuel.Manager.Server.Repositories.Interfaces;
using Fuel.Manager.Server.Services.Interfaces;
using NHibernate;

namespace Fuel.Manager.Server.Services.Implementation
{
    public class EmployeeService : IEmployeeService
    {

        private IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void Create(Employee employee)
        {
            _employeeRepository.Create(employee);
        }

        public void Delete(Employee employee)
        {
            _employeeRepository.Delete(employee);
        }

        public IList<Employee> GetAll()
        {
            return _employeeRepository.GetAll();
        }

        public Employee GetById(int id)
        {
            return _employeeRepository.GetById(id);
        }

        public void Update(Employee employee)
        {
            _employeeRepository.Update(employee);
        }

        public Employee Login(string username, string password)
        {
            Employee e = _employeeRepository.GetByUsername(username);

            if (e == null)
            {
                return null;
            }

            if (BCrypt.Net.BCrypt.Verify(password, e.Password))
            {
                return e;
            }
            else
            {
                return null;
            }
        }
    }
}
