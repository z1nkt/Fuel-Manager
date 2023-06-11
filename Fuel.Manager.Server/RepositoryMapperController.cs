using Fuel.Manager.Server.Models;
using Fuel.Manager.Server.DTO;
using Microsoft.AspNetCore.Mvc;
using Fuel.Manager.Server.Services.Interfaces;

namespace Fuel.Manager.Server
{
    public class RepositoryMapperController
    {
        private ICarService _carService;
        private IEmployeeService _employeeService;
        private IRefuelService _refuelService;
        //private IEmployeeToCarRelationService _employeeToCarRelationService;
        public RepositoryMapperController(ICarService carService, IEmployeeService employeeService, IRefuelService refuelService/*, IEmployeeToCarRelationService employeeToCarRelationService*/)
        {
            _carService = carService;
            _employeeService = employeeService;
            _refuelService = refuelService;
            //_employeeToCarRelationService = employeeToCarRelationService;
        }

        public void StartUp()
        {
            var app = WebApplication.Create();
            app.MapPost("/api/login", ([FromBody] Login login) => Login(login));

            //app.MapPost("/api/employee/cars", ([FromBody] EmployeeId e) => CarsFromEmployeeId(e));

            //app.MapPost("/api/employee/refuels", ([FromBody] EmployeeId e) => RefuelsFromEmployeeId(e));

            app.MapGet("/api/cars", () => AllCars());

            app.MapPost("/api/car/new", ([FromBody] Car c) => SaveNewCar(c));

            app.MapPost("/api/car/overwrite", ([FromBody] Car c) => SaveEditedCar(c));

            app.MapPost("/api/car/delete", ([FromBody] Car c) => DeleteCar(c));

            app.MapGet("/api/refuels", () => AllRefuels());

            app.MapGet("/api/employees", () => AllEmployees());

            app.MapPost("/api/employee/new", ([FromBody] AddEmployee oe) => SaveNewEmployee(oe));

            app.Run("http://localhost:3000");
        }

        private IResult DeleteCar(Car car)
        {
            try
            {
                _carService.Delete(car);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem();
            }
        }

        private IResult SaveNewEmployee(AddEmployee addEmployee)
        {
            try
            {
                addEmployee.Password = BCrypt.Net.BCrypt.HashPassword(addEmployee.Password);
                Employee e = new Employee();

                e.EmployeeNo = addEmployee.EmployeeNo;
                e.Username = addEmployee.Username;
                e.Password = addEmployee.Password;
                e.Firstname = addEmployee.Firstname;
                e.Lastname = addEmployee.Lastname;
                e.IsAdmin = Convert.ToBoolean(addEmployee.IsAdmin);

                _employeeService.Create(e);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem();
            }
        }

        private IResult SaveNewCar(Car car)
        {
            try
            {
                _carService.Create(car);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem();
            }
        }

        private IResult SaveEditedCar(Car car)
        {
            try
            {
                _carService.Update(car);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem();
            }
        }

        private IResult SaveEditedEmployee(AddEmployee ie)
        {
            try
            {
                ie.Password = BCrypt.Net.BCrypt.HashPassword(ie.Password);
                Employee e = new Employee();
                e.Id = ie.Id;
                e.EmployeeNo = ie.EmployeeNo;
                e.Username = ie.Username;
                e.Password = ie.Password;
                e.Firstname = ie.Firstname;
                e.Lastname = ie.Lastname;
                e.IsAdmin = Convert.ToBoolean(ie.IsAdmin);
                _employeeService.Update(e);

                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem();
            }
        }

        private IResult AllCars()
        {
            return Results.Ok(_carService.GetAll());
        }

        private IResult AllRefuels()
        {
            return Results.Ok(_refuelService.GetAll());
        }

        private IResult AllEmployees()
        {
            return Results.Ok(_employeeService.GetAll());
        }

        private IResult Login(Login login)
        {
            Employee e = _employeeService.Login(login.Username, login.Password);

            if (e != null)
            {
                GetEmployee outgoingEmployee = new GetEmployee();

                outgoingEmployee.Id = e.Id;
                outgoingEmployee.Username = e.Username;
                outgoingEmployee.Firstname = e.Firstname;
                outgoingEmployee.Lastname = e.Lastname;
                outgoingEmployee.EmployeeNo = e.EmployeeNo;
                outgoingEmployee.IsAdmin = e.IsAdmin;
                outgoingEmployee.Version = e.Version;

                return Results.Ok(outgoingEmployee);
            }
            else
            {
                return Results.NotFound(null);
            }
        }

        /*private IResult CarsFromEmployeeId(EmployeeId e)
        {
            int employeeId = Convert.ToInt32(e.employeeId);
            return Results.Ok(_employeeToCarRelationService.GetCarsByEmployeeId(employeeId));
        }

        private IResult RefuelsFromEmployeeId(EmployeeId e)
        {
            int employeeId = Convert.ToInt32(e.employeeId);
            List<Car> c = _employeeToCarRelationService.GetCarsByEmployeeId(employeeId);
            List<Refuel> r = new List<Refuel>();

            foreach (Car car in c)
            {
                r.AddRange(_refuelService.GetRefuelsByCar(car));
            }

            return Results.Ok(r);
        }*/
    }
}
