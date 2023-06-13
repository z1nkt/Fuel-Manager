using Fuel.Manager.Server.Models;
using Fuel.Manager.Server.Repositories.Interfaces;
using NHibernate;

namespace Fuel.Manager.Server.Repositories.Implementation
{
    public class EmployeeToCarRelationRepository : BaseRepository<EmployeeToCarRelation> , IEmployeeToCarRelationRepository
    {
        private ISessionFactory _nhibernateHelper;
        public EmployeeToCarRelationRepository(ISessionFactory nhibernateHelper) : base(nhibernateHelper)
        {
            _nhibernateHelper = nhibernateHelper;
        }
        public List<EmployeeToCarRelation> GetByEmployee(Employee employee)
        {
            using (var session = _nhibernateHelper.OpenSession())
            {
                return session.Query<EmployeeToCarRelation>().Where(e => e.Employee == employee).ToList();
            }
        }

    }
}
