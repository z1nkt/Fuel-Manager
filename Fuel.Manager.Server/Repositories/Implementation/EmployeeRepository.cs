using Fuel.Manager.Server.Helper;
using Fuel.Manager.Server.Models;
using Fuel.Manager.Server.Repositories.Interfaces;
using NHibernate;

namespace Fuel.Manager.Server.Repositories.Implementation
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        private ISessionFactory _nhibernateHelper;
        public EmployeeRepository(ISessionFactory nhibernateHelper) : base(nhibernateHelper)
        {
            _nhibernateHelper = nhibernateHelper;
        }

        public Employee FindByEmployeeNumber(string employeeNumber)
        {
            using var sesssion = _nhibernateHelper.OpenSession();
            return sesssion.Query<Employee>().FirstOrDefault(e => e.EmployeeNo == employeeNumber);
        }

        public Employee GetByUsername(string username)
        {
            using (var session = _nhibernateHelper.OpenSession())
            {
                return session.Query<Employee>().FirstOrDefault(e => e.Username == username);
            }
        }
    }
}
