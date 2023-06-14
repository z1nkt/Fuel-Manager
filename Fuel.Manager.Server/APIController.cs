using Fuel.Manager.Server.Models;
using Fuel.Manager.Server.DTO;
using Microsoft.AspNetCore.Mvc;
using Fuel.Manager.Server.Services.Interfaces;

namespace Fuel.Manager.Server
{
    public class APIController
    {
        private ICarService _carService;
        private IEmployeeService _employeeService;
        private IRefuelService _refuelService;
        private IEmployeeToCarRelationService _employeeToCarRelationService;
        public APIController(ICarService carService, IEmployeeService employeeService, IRefuelService refuelService, IEmployeeToCarRelationService employeeToCarRelationService)
        {
            _carService = carService;
            _employeeService = employeeService;
            _refuelService = refuelService;
            _employeeToCarRelationService = employeeToCarRelationService;
        }

        public void StartUp()
        {
            var app = WebApplication.Create();
            app.MapPost("/api/login", ([FromBody] Login login) => Login(login));

            app.MapGet("/api/cars", () => AllCars());

            app.MapGet("/api/refuels", () => AllRefuels());

            app.MapGet("/api/employees", () => AllEmployees());

            app.MapPost("/api/employee/cars", ([FromBody] EmployeeId e) => CarsFromEmployeeId(e));

            app.MapPost("/api/employee/refuels", ([FromBody] EmployeeId e) => RefuelsFromEmployeeId(e));

            

            app.MapPost("/api/car/new", ([FromBody] Car c) => SaveNewCar(c));

            app.MapPost("/api/car/edit", ([FromBody] Car c) => SaveEditedCar(c));

            app.MapPost("/api/car/delete", ([FromBody] Car c) => DeleteCar(c));


            app.MapPost("/api/refuel/new", ([FromBody] Refuel r) => SaveNewRefuel(r));

            app.MapPost("/api/refuel/edit", ([FromBody] Refuel r) => SaveEditedRefuel(r));

            app.MapPost("/api/refuel/delete", ([FromBody] Refuel r) => DeleteRefuel(r));


            app.MapPost("/api/employee/new", ([FromBody] AddEmployee oe) => SaveNewEmployee(oe));

            app.MapPost("/api/employee/edit", ([FromBody] AddEmployee oe) => SaveEditedEmployee(oe));

            app.MapPost("/api/employee/delete", ([FromBody] AddEmployee oe) => DeleteEmployee(oe));


            app.MapPost("/api/employee/car/add", ([FromBody] AddEmployeeCarRelation ecr) => AddEmployeeCarRelation(ecr));

            app.MapPost("/api/employee/car/delete", ([FromBody] AddEmployeeCarRelation ecr) => DeleteEmployeeCarRelation(ecr));

            app.Run("http://localhost:5115");
        }

        public IResult Login(Login login)
        {

            if (login == null)
            {
                return Results.NotFound(null);
            }

            Employee e = _employeeService.Login(login.Username, login.Password);


            if (e != null)
            {
                GetEmployee getEmployee = new GetEmployee();

                getEmployee.Id = e.Id;
                getEmployee.Username = e.Username;
                getEmployee.Firstname = e.Firstname;
                getEmployee.Lastname = e.Lastname;
                getEmployee.EmployeeNo = e.EmployeeNo;
                getEmployee.IsAdmin = e.IsAdmin;
                getEmployee.Version = e.Version;

                return Results.Ok(getEmployee);
            }
            else
            {
                return Results.NotFound(null);
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

        private IResult SaveNewRefuel(Refuel r)
        {
            try
            {
                _refuelService.Create(r);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem();
            }
        }

        private IResult SaveEditedRefuel(Refuel r)
        {
            try
            {
                _refuelService.Update(r);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem();
            }
        }

        private IResult DeleteRefuel(Refuel r)
        {
            try
            {
                _refuelService.Delete(r);
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

        private IResult DeleteEmployee(AddEmployee ie)
        {
            try
            {
                Employee e = new Employee();

                e.Id = ie.Id;
                e.EmployeeNo = ie.EmployeeNo;
                e.Username = ie.Username;
                e.Password = ie.Password;
                e.Firstname = ie.Firstname;
                e.Lastname = ie.Lastname;
                e.IsAdmin = Convert.ToBoolean(ie.IsAdmin);
                e.Version = ie.Version;

                _employeeService.Delete(e);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem();
            }
        }

        private IResult AddEmployeeCarRelation(AddEmployeeCarRelation ecr)
        {
            Employee e = _employeeService.GetById(ecr.EmployeeId);
            Car c = _carService.GetById(ecr.CarId);

            EmployeeToCarRelation employeeToCarRelation = new EmployeeToCarRelation();
            employeeToCarRelation.Employee = e;
            employeeToCarRelation.Car = c;

            try
            {
                _employeeToCarRelationService.Create(employeeToCarRelation);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem();
            }


        }

        private IResult DeleteEmployeeCarRelation(AddEmployeeCarRelation ecr)
        {
            List<EmployeeToCarRelation> listRelations = _employeeToCarRelationService.GetAll() as List<EmployeeToCarRelation>;

            foreach (EmployeeToCarRelation r in listRelations)
            {
                if (r.Car.Id == ecr.CarId && r.Employee.Id == ecr.EmployeeId)
                {
                    try
                    {
                        _employeeToCarRelationService.Delete(r);
                        return Results.Ok();
                    }
                    catch (Exception ex)
                    {
                        return Results.Problem();
                    }
                }
            }

            return Results.Conflict();
        }

        private IResult CarsFromEmployeeId(EmployeeId e)
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
        }
    }
}
