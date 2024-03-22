using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using FoodPortal.Exceptions;
using FoodPortal.Interfaces;
using FoodPortal.Models;
using FoodPortal.Models.DTO;
using FoodPortal.RequestModel;
using FoodPortal.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FoodPortalTestProject.ServiceTesting
{
    [TestClass]
    public class OrderServiceTest
    {
        private Mock<ICrud<Order, IdDTO>> _orderRepoMock;
        private Mock<ICrud<TimeSlot, IdDTO>> _timeSlotRepoMock;
        private Mock<ICrud<TrackStatus, IdDTO>> _trackStatusRepoMock;
        private OrderService _service;
        private Fixture _fixture;
        private Mock<IMapper> _mapperMock;

        public OrderServiceTest()
        {
            _fixture = new Fixture();
            _orderRepoMock = new Mock<ICrud<Order, IdDTO>>();
            _timeSlotRepoMock = new Mock<ICrud<TimeSlot, IdDTO>>();
            _trackStatusRepoMock = new Mock<ICrud<TrackStatus, IdDTO>>();
            _mapperMock = new Mock<IMapper>();
            _service = new OrderService(
                _orderRepoMock.Object,
                _timeSlotRepoMock.Object,
                _trackStatusRepoMock.Object,
                _mapperMock.Object
            );
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TestMethod]
        public async Task Add_Order_ReturnsAddedOrderDetails()
        {
            // Arrange
            var requestOrder = _fixture.Create<RequestOrder>();
            var order = _fixture.Create<Order>();
            var addedOrder = _fixture.Create<RequestOrder>();

            // Arrange mock for IMapper
            _mapperMock.Setup(mapper => mapper.Map<Order>(It.IsAny<RequestOrder>()))
                .Returns(order);

            _orderRepoMock.Setup(repo => repo.Add(It.IsAny<Order>()))
                .ReturnsAsync(order);

            _mapperMock.Setup(mapper => mapper.Map<RequestOrder>(It.IsAny<Order>()))
                .Returns(addedOrder);

            // Act
            var result = await _service.Add_Order(requestOrder);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RequestOrder));
            Assert.AreEqual(addedOrder.Id, result.Id);
            Assert.AreEqual(addedOrder.DeliveryOptionId, result.DeliveryOptionId);
        }

        [TestMethod]
        public async Task GetUnAvailableDate_ReturnsListOfUnavailableDates()
        {
            // Arrange
            var targetDate = DateTime.Now;
            var orders = _fixture.CreateMany<Order>().ToList();
            var timeSlots = _fixture.CreateMany<TimeSlot>().ToList();

            // Arrange mock for the repositories
            _orderRepoMock.Setup(repo => repo.GetAll()).ReturnsAsync(orders);
            _timeSlotRepoMock.Setup(repo => repo.GetAll()).ReturnsAsync(timeSlots);

            // Act
            var result = await _service.GetUnAvailableDate(targetDate);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<AvailabilityDTO>));
            // Add assertions for the expected result based on your test data
        }

        [TestMethod]
        public async Task GetAvailableTimeSlot_ReturnsListOfAvailableTimeSlots()
        {
            // Arrange
            var targetDate = DateTime.Now;
            var orders = _fixture.CreateMany<Order>().ToList();
            var timeSlots = _fixture.CreateMany<TimeSlot>().ToList();

            // Arrange mock for the repositories
            _orderRepoMock.Setup(repo => repo.GetAll()).ReturnsAsync(orders);
            _timeSlotRepoMock.Setup(repo => repo.GetAll()).ReturnsAsync(timeSlots);

            // Act
            var result = await _service.GetAvailableTimeSlot(targetDate);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<AvailabilityDTO>));
        }
    }
}
