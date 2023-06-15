using System.Collections;
using Fuel.Manager.Server.Models;
using Fuel.Manager.Server.Repositories.Interfaces;
using Fuel.Manager.Server.Services.Implementation;
using Moq;

namespace ServerTest.Services
{


    [TestClass]
    public class EmployeeServiceTests
    {
        private Mock<IEmployeeRepository> _employeeRepositoryMock;
        private EmployeeService _employeeService;

        [TestInitialize]
        public void Setup()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _employeeService = new EmployeeService(_employeeRepositoryMock.Object);
        }

        [TestMethod]
        public void GetAll_ReturnsAllEmployees()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee {
                    Id = 1,
                    Username = "Gustav",
                    Lastname = "Hartgas",
                    EmployeeNo = "1PCR9",
                    IsAdmin = true,
                },
                new Employee {
                    Id = 1,
                    Username = "Joachim",
                    Lastname = "Schnell",
                    EmployeeNo = "1234",
                    IsAdmin = true,
                },
            };
            _employeeRepositoryMock.Setup(x => x.GetAll()).Returns(employees);

            // Act
            var result = _employeeService.GetAll();

            // Assert
            CollectionAssert.AreEqual(employees, (ICollection)result);
        }

        [TestMethod]
        public void GetById_ReturnsEmployeeWithMatchingId()
        {
            // Arrange
            var employeeId = 1;
            var employee = new Employee
            {
                Id = 1,
                Username = "Hans",
                Lastname = "Peter",
                EmployeeNo = "1PCR9",
                IsAdmin = true,
            };

            _employeeRepositoryMock.Setup(x => x.GetById(employeeId)).Returns(employee);

            // Act
            var result = _employeeService.GetById(employeeId);

            // Assert
            Assert.AreEqual(employee, result);
        }

        [TestMethod]
        public void Create_CallsEmployeeRepositoryCreate()
        {
            // Arrange
            var employee = new Employee
            {
                Id = 1,
                Username = "Hans",
                Lastname = "Peter",
                EmployeeNo = "1PCR9",
                IsAdmin = true,
            };

            // Act
            _employeeService.Create(employee);

            // Assert
            _employeeRepositoryMock.Verify(x => x.Create(employee), Times.Once);
        }

        [TestMethod]
        public void Update_CallsEmployeeRepositoryUpdate()
        {
            // Arrange
            var employee = new Employee
            {
                Id = 1,
                Username = "Hans",
                Lastname = "Peter",
                EmployeeNo = "1PCR9",
                IsAdmin = true,
            };

            // Act
            _employeeService.Update(employee);

            // Assert
            _employeeRepositoryMock.Verify(x => x.Update(employee), Times.Once);
        }

        [TestMethod]
        public void Delete_CallsEmployeeRepositoryDelete()
        {
            // Arrange
            var employee = new Employee
            {
                Id = 1,
                Username = "Hans",
                Lastname = "Peter",
                EmployeeNo = "1PCR9",
                IsAdmin = true,
            };

            // Act
            _employeeService.Delete(employee);

            // Assert
            _employeeRepositoryMock.Verify(x => x.Delete(employee), Times.Once);
        }
    }

}
