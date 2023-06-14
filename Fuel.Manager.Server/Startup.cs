using Autofac;
using Fuel.Manager.Server.Helper;
using Fuel.Manager.Server.Repositories.Implementation;
using Fuel.Manager.Server.Services;
using Fuel.Manager.Server.Services.Implementation;
using Fuel.Manager.Server.Services.Interfaces;

namespace Fuel.Manager.Server
{
    public class Startup
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            NHibernateHelper nHibernateHelper = new NHibernateHelper();
            var session = nHibernateHelper.CreateSession();

            builder.RegisterInstance(new EmployeeRepository(session)).As<EmployeeRepository>();
            builder.RegisterInstance(new CarService(new CarRepository(session))).As<ICarService>();
            builder.RegisterInstance(new RefuelService(new RefuelRepository(session))).As<IRefuelService>();
            builder.RegisterInstance(new EmployeeService(new EmployeeRepository(session))).As<IEmployeeService>();
            builder.RegisterInstance(new EmployeeToCarRelationService(new EmployeeToCarRelationRepository(session), new EmployeeRepository(session))).As<IEmployeeToCarRelationService>();

            var container = builder.Build();
            using (var scope = container.BeginLifetimeScope())
            {
                APIController controller = new APIController(scope.Resolve<ICarService>(), scope.Resolve<IEmployeeService>(), scope.Resolve<IRefuelService>(), scope.Resolve<IEmployeeToCarRelationService>());

                controller.StartUp();
            }
        }

    }
}
