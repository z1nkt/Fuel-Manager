using Fuel.Manager.Server;
using Fuel.Manager.Server.DTO;
using Fuel.Manager.Server.Models;
using Fuel.Manager.Server.Repositories.Interfaces;
using Fuel.Manager.Server.Services.Implementation;
using Fuel.Manager.Server.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ServerTest
{
    [TestClass]
    public class LoginTest
    {

        private APIController _apiController;
        private Mock<ICarService> _carServiceMock;
        private Mock<IEmployeeService> _employeeServiceMock;
        private Mock<IRefuelService> _refuelServiceMock;
        private Mock<IEmployeeToCarRelationService> _employeeToCarRelationServiceMock;
        private EmployeeService _employeeService;
        private Mock<IEmployeeRepository> _employeeRepositoryMock;

        [TestInitialize]
        public void Setup()
        {
            _carServiceMock = new Mock<ICarService>();
            _employeeServiceMock = new Mock<IEmployeeService>();
            _refuelServiceMock = new Mock<IRefuelService>();
            _employeeToCarRelationServiceMock = new Mock<IEmployeeToCarRelationService>();

            _apiController = new APIController(
                _carServiceMock.Object,
                _employeeServiceMock.Object,
                _refuelServiceMock.Object,
                _employeeToCarRelationServiceMock.Object
            );

            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _employeeService = new EmployeeService(_employeeRepositoryMock.Object);
        }


        //LoginTest-1
        [TestMethod]
        public void Login_ValidCredentials_ReturnsOkResult()
        {
            // Arrange
            Login validLogin = new Login { Username = "admin", Password = "admin" };

            // Act
            var result = _apiController.Login(validLogin);

            // Assert
            Assert.IsNotNull(result is Ok);
        }

        //LoginTest-2
        [TestMethod]
        public void Login_InvalidCredentials_ReturnsNotFoundResult()
        {
            // Arrange
            Login invalidLogin = new Login { Username = "invalid_username", Password = "invalid_password" };

            // Act
            var result = _apiController.Login(invalidLogin);

            // Assert
            Assert.IsTrue(result is NotFound);
        }

        //LoginTest-3
        [TestMethod]
        public void Login_NullLogin_ReturnsNotFoundResult()
        {
            // Arrange
            Login nullLogin = null;

            // Act
            var result = _apiController.Login(nullLogin);

            // Assert
            Assert.IsTrue(result is NotFound);
            
        }

        //LoginTest-4
        [TestMethod]
        public void Login_ValidCredentials_ReturnsEmployee()
        {
            // Arrange
            string username = "admin";
            string password = "admin";
            Employee mockEmployee = new Employee {
                Id = 1,
                Username = "admin",
                Lastname = "Administrator",
                EmployeeNo = "1PCR9",
                IsAdmin = true,
                Password = BCrypt.Net.BCrypt.HashPassword(password)
            };
            _employeeRepositoryMock.Setup(repo => repo.GetByUsername(username)).Returns(mockEmployee);

            // Act
            var result = _employeeService.Login(username, password);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(mockEmployee, result);
        }

        //LoginTest-5
        [TestMethod]
        public void Login_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            string username = "admin";
            string password = "invalid_password";
            Employee mockEmployee = new Employee {
                Id = 1,
                Username = "admin",
                Lastname = "Administrator",
                EmployeeNo = "1PCR9",
                IsAdmin = true,
                Password = BCrypt.Net.BCrypt.HashPassword("admin")
            };

            _employeeRepositoryMock.Setup(repo => repo.GetByUsername(username)).Returns(mockEmployee);

            // Act
           var result = _employeeService.Login(username, password);

            // Assert
            Assert.IsNull(result);
        }

    }
}
