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
    public class CarServiceTests
    {
        private Mock<ICarRepository> _carRepositoryMock;
        private CarService _carService;

        [TestInitialize]
        public void Setup()
        {
            _carRepositoryMock = new Mock<ICarRepository>();
            _carService = new CarService(_carRepositoryMock.Object);
        }

        [TestMethod]
        public void GetAll_ReturnsAllCars()
        {
            // Arrange
            var cars = new List<Car>
        {
            new Car { Id = 1, Vendor = "Toyota", Model = "Supra" },
            new Car { Id = 2, Vendor = "Honda", Model = "Civic" }
        };
            _carRepositoryMock.Setup(x => x.GetAll()).Returns(cars);

            // Act
            var result = _carService.GetAll();

            // Assert
            CollectionAssert.AreEqual(cars, (ICollection)result);
        }

        [TestMethod]
        public void GetById_ReturnsCarWithMatchingId()
        {
            // Arrange
            var carId = 1;
            var car = new Car { Id = carId, Vendor = "Toyota", Model = "Supra" };
            _carRepositoryMock.Setup(x => x.GetById(carId)).Returns(car);

            // Act
            var result = _carService.GetById(carId);

            // Assert
            Assert.AreEqual(car, result);
        }

        [TestMethod]
        public void Create_CallsCarRepositoryCreate()
        {
            // Arrange
            var car = new Car { Id = 1, Vendor = "Toyota", Model = "Supra" };

            // Act
            _carService.Create(car);

            // Assert
            _carRepositoryMock.Verify(x => x.Create(car), Times.Once);
        }

        [TestMethod]
        public void Update_CallsCarRepositoryUpdate()
        {
            // Arrange
            var car = new Car { Id = 1, Vendor = "Toyota", Model = "Supra" };

            // Act
            _carService.Update(car);

            // Assert
            _carRepositoryMock.Verify(x => x.Update(car), Times.Once);
        }

        [TestMethod]
        public void Delete_CallsCarRepositoryDelete()
        {
            // Arrange
            var car = new Car { Id = 1, Vendor = "Toyota", Model = "Supra" };

            // Act
            _carService.Delete(car);

            // Assert
            _carRepositoryMock.Verify(x => x.Delete(car), Times.Once);
        }
    }

}
