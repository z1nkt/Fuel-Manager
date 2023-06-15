using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fuel.Manager.Server.Models;
using Fuel.Manager.Server.Repositories.Interfaces;
using Fuel.Manager.Server.Services.Implementation;
using Moq;

namespace ServerTest.Services
{
    [TestClass]
    public class EmployeeToCarRelationServiceTests
    {
        private Mock<IEmployeeToCarRelationRepository> _employeeToCarRelationRepositoryMock;
        private Mock<IEmployeeRepository> _employeeRepositoryMock;
        private EmployeeToCarRelationService _employeeToCarRelationService;

        [TestInitialize]
        public void Setup()
        {
            _employeeToCarRelationRepositoryMock = new Mock<IEmployeeToCarRelationRepository>();
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _employeeToCarRelationService = new EmployeeToCarRelationService(
                _employeeToCarRelationRepositoryMock.Object,
                _employeeRepositoryMock.Object);
        }

        [TestMethod]
        public void GetAll_ReturnsAllEmployeeToCarRelations()
        {
            // Arrange
            var employee1 = new Employee
            {
                Id = 1,
                Username = "Gustav",
                Lastname = "Hartgas",
                EmployeeNo = "1PCR9",
                IsAdmin = true,
            };
            var employee2 = new Employee
            {
                Id = 2,
                Username = "Joachim",
                Lastname = "Schnell",
                EmployeeNo = "1234",
                IsAdmin = true,
            };

            var car1 = new Car { Id = 1, Vendor = "Toyota", Model = "Supra" };
            var car2 = new Car { Id = 2, Vendor = "Audi", Model = "R8" };



            var employeeToCarRelations = new List<EmployeeToCarRelation>
            {
                new EmployeeToCarRelation { Id = 1, Employee = employee1, Car = car1 },
                new EmployeeToCarRelation { Id = 2, Employee = employee2, Car = car2 }
            };
            _employeeToCarRelationRepositoryMock.Setup(x => x.GetAll()).Returns(employeeToCarRelations);

            // Act
            var result = _employeeToCarRelationService.GetAll();

            // Assert
            CollectionAssert.AreEqual(employeeToCarRelations, (ICollection)result);
        }

        [TestMethod]
        public void GetById_ReturnsEmployeeToCarRelationWithMatchingId()
        {
            // Arrange
            var employee = new Employee
            {
                Id = 1,
                Username = "Gustav",
                Lastname = "Hartgas",
                EmployeeNo = "1PCR9",
                IsAdmin = true,
            };
            var car = new Car { Id = 1, Vendor = "Toyota", Model = "Supra" };

            var relationId = 1;
            var employeeToCarRelation = new EmployeeToCarRelation { Id = relationId, Employee = employee, Car = car };
            _employeeToCarRelationRepositoryMock.Setup(x => x.GetById(relationId)).Returns(employeeToCarRelation);

            // Act
            var result = _employeeToCarRelationService.GetById(relationId);

            // Assert
            Assert.AreEqual(employeeToCarRelation, result);
        }

        [TestMethod]
        public void Create_CallsEmployeeToCarRelationRepositoryCreate()
        {
            // Arrange
            var employee = new Employee
            {
                Id = 1,
                Username = "Gustav",
                Lastname = "Hartgas",
                EmployeeNo = "1PCR9",
                IsAdmin = true,
            };
            var car = new Car { Id = 1, Vendor = "Toyota", Model = "Supra" };
            var relation = new EmployeeToCarRelation { Id = 1, Employee = employee, Car = car };

            // Act
            _employeeToCarRelationService.Create(relation);

            // Assert
            _employeeToCarRelationRepositoryMock.Verify(x => x.Create(relation), Times.Once);
        }

        [TestMethod]
        public void Update_CallsEmployeeToCarRelationRepositoryUpdate()
        {
            // Arrange
            var employee = new Employee
            {
                Id = 1,
                Username = "Gustav",
                Lastname = "Hartgas",
                EmployeeNo = "1PCR9",
                IsAdmin = true,
            };
            var car = new Car { Id = 1, Vendor = "Toyota", Model = "Supra" };
            var relation = new EmployeeToCarRelation { Id = 1, Employee = employee, Car = car };


            // Act
            _employeeToCarRelationService.Update(relation);

            // Assert
            _employeeToCarRelationRepositoryMock.Verify(x => x.Update(relation), Times.Once);
        }

        [TestMethod]
        public void Delete_CallsEmployeeToCarRelationRepositoryDelete()
        {
            // Arrange
            var employee = new Employee
            {
                Id = 1,
                Username = "Gustav",
                Lastname = "Hartgas",
                EmployeeNo = "1PCR9",
                IsAdmin = true,
            };
            var car = new Car { Id = 1, Vendor = "Toyota", Model = "Supra" };
            var relation = new EmployeeToCarRelation { Id = 1, Employee = employee, Car = car };

            // Act
            _employeeToCarRelationService.Delete(relation);

            // Assert
            _employeeToCarRelationRepositoryMock.Verify(x => x.Delete(relation), Times.Once);
        }

        [TestMethod]
        public void GetCarsByEmployeeId_ReturnsCarsForGivenEmployeeId()
        {
            // Arrange


        }
    }
}
