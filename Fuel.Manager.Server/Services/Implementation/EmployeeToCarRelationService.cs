using Fuel.Manager.Server.Models;
using Fuel.Manager.Server.Repositories.Interfaces;
using Fuel.Manager.Server.Services.Interfaces;

namespace Fuel.Manager.Server.Services.Implementation
{
    public class EmployeeToCarRelationService : IEmployeeToCarRelationService
    {
        private IEmployeeToCarRelationRepository _employeeToCarRelationRepository;
        private IEmployeeRepository _employeeRepository;

        public EmployeeToCarRelationService(IEmployeeToCarRelationRepository employeeToCarRelationRepository, IEmployeeRepository employeeRepository)
        {
            _employeeToCarRelationRepository = employeeToCarRelationRepository;
            _employeeRepository = employeeRepository;
        }

        public IList<EmployeeToCarRelation> GetAll()
        {
            return _employeeToCarRelationRepository.GetAll();
        }

        public EmployeeToCarRelation GetById(int id)
        {
            return _employeeToCarRelationRepository.GetById(id);
        }

        public void Create(EmployeeToCarRelation relation)
        {
            _employeeToCarRelationRepository.Create(relation);
        }

        public void Update(EmployeeToCarRelation relation)
        {
            _employeeToCarRelationRepository.Update(relation);
        }

        public void Delete(EmployeeToCarRelation relation)
        {
            _employeeToCarRelationRepository.Delete(relation);
        }

        public List<Car> GetCarsByEmployeeId(int id)
        {
            List<Car> cars = new List<Car>();
            Employee employee = _employeeRepository.GetById(id);
            List<EmployeeToCarRelation> relations = _employeeToCarRelationRepository.GetByEmployee(employee);

            foreach (EmployeeToCarRelation employeeToCarRelation in relations)
            {
                cars.Add(employeeToCarRelation.Car);
            }
            return cars;
        }


    }
}
