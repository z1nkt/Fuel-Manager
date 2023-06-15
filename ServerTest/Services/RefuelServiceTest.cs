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
    public class RefuelServiceTests
    {
        private Mock<IRefuelRepository> _refuelRepositoryMock;
        private RefuelService _refuelService;

        [TestInitialize]
        public void Setup()
        {
            _refuelRepositoryMock = new Mock<IRefuelRepository>();
            _refuelService = new RefuelService(_refuelRepositoryMock.Object);
        }

        [TestMethod]
        public void GetById_ReturnsRefuelWithMatchingId()
        {
            // Arrange
            var refuelId = 1;
            var refuel = new Refuel { Id = refuelId, Amount = 50 };
            _refuelRepositoryMock.Setup(x => x.GetById(refuelId)).Returns(refuel);

            // Act
            var result = _refuelService.GetById(refuelId);

            // Assert
            Assert.AreEqual(refuel, result);
        }

        [TestMethod]
        public void GetRefuelsByCar_ReturnsRefuelsForGivenCar()
        {
            // Arrange
            var car = new Car { Id = 1, Vendor = "Toyota", Model = "Corolla" };
            var refuels = new List<Refuel>
        {
            new Refuel { Id = 1, Amount = 50, Car = car },
            new Refuel { Id = 2, Amount = 40, Car = car }
        };
            _refuelRepositoryMock.Setup(x => x.GetRefuelsByCar(car)).Returns(refuels);

            // Act
            var result = _refuelService.GetRefuelsByCar(car);

            // Assert
            CollectionAssert.AreEqual(refuels, (ICollection)result);
        }

        [TestMethod]
        public void Create_CallsRefuelRepositoryCreate()
        {
            // Arrange
            var refuel = new Refuel { Id = 1, Amount = 500 };

            // Act
            _refuelService.Create(refuel);

            // Assert
            _refuelRepositoryMock.Verify(x => x.Create(refuel), Times.Once);
        }

        [TestMethod]
        public void Update_CallsRefuelRepositoryUpdate()
        {
            // Arrange
            var refuel = new Refuel { Id = 1, Amount = 500 };

            // Act
            _refuelService.Update(refuel);

            // Assert
            _refuelRepositoryMock.Verify(x => x.Update(refuel), Times.Once);
        }

        [TestMethod]
        public void Delete_CallsRefuelRepositoryDelete()
        {
            // Arrange
            var refuel = new Refuel { Id = 1, Amount = 500 };

            // Act
            _refuelService.Delete(refuel);

            // Assert
            _refuelRepositoryMock.Verify(x => x.Delete(refuel), Times.Once);
        }
    }

}
