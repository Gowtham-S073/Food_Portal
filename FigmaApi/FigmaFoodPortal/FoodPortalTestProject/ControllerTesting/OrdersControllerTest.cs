using AutoFixture;
using FoodPortal.Controllers;
using FoodPortal.Exceptions;
using FoodPortal.Interfaces;
using FoodPortal.Models;
using FoodPortal.Models.DTO;
using FoodPortal.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable


namespace FoodPortalTestProject.ControllerTesting
{
    [TestClass]
    public class OrdersControllerTest
    {
        private Mock<IOrderService> _orderServiceMock;
        private Fixture _fixture;
        private OrdersController _controller;

        public OrdersControllerTest()
        {
            _fixture = new Fixture();
            _orderServiceMock = new Mock<IOrderService>();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
             .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TestMethod]
        public async Task Add_Order_ReturnsCreated()
        {
            // Arrange
            var order = _fixture.Create<RequestOrder>();

            _orderServiceMock.Setup(service =>
                service.Add_Order(It.IsAny<RequestOrder>()))
                .ReturnsAsync(order);

            _controller = new OrdersController(_orderServiceMock.Object);

            // Act
            var result = await _controller.Add_Order(order);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<RequestOrder>));

            var actionResult = result as ActionResult<RequestOrder>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedResult));

            var createdResult = actionResult.Result as CreatedResult;

            Assert.AreEqual(StatusCodes.Status201Created, createdResult.StatusCode);
            Assert.IsInstanceOfType(createdResult.Value, typeof(RequestOrder));

            var createdOrder = createdResult.Value as RequestOrder;
            Assert.AreEqual(order.Id, createdOrder.Id); // Ensure IDs match or adjust accordingly
        }
        [TestMethod]
        public async Task Add_Order_ReturnsBadRequestOnNullException()
        {
            // Arrange
            var order = _fixture.Create<RequestOrder>();
            var errorMessage = "Null exception happened";
            var nullException = new NullException(errorMessage);

            _orderServiceMock.Setup(service =>
                service.Add_Order(It.IsAny<RequestOrder>()))
                .ThrowsAsync(nullException);

            _controller = new OrdersController(_orderServiceMock.Object);

            // Act
            var result = await _controller.Add_Order(order);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<RequestOrder>));

            var actionResult = result as ActionResult<RequestOrder>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundObjectResult));

            var notFoundResult = actionResult.Result as NotFoundObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
            Assert.IsInstanceOfType(notFoundResult.Value, typeof(Error));

            var error = notFoundResult.Value as Error;
            Assert.AreEqual(404, error.ID);
            Assert.AreEqual(errorMessage, error.Message);
        }
        [TestMethod]
        public async Task Add_Order_ReturnsBadRequestOnDuplicateRecordException()
        {
            // Arrange
            var order = _fixture.Create<RequestOrder>();
            var errorMessage = "Duplicate Record Exception happened";
            var duplicate = new DuplicateRecordException(errorMessage);

            _orderServiceMock.Setup(service =>
                service.Add_Order(It.IsAny<RequestOrder>()))
                .ThrowsAsync(duplicate);

            _controller = new OrdersController(_orderServiceMock.Object);

            // Act
            var result = await _controller.Add_Order(order);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<RequestOrder>));

            var actionResult = result as ActionResult<RequestOrder>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));

            var badRequestResult = actionResult.Result as BadRequestObjectResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.IsInstanceOfType(badRequestResult.Value, typeof(Error));

            var error = badRequestResult.Value as Error;
            Assert.AreEqual(400, error.ID);
            Assert.AreEqual(errorMessage, error.Message);
        }


        [TestMethod]
        public async Task Add_Order_ReturnsBadRequestOnSqlException()
        {
            // Arrange
            var order = _fixture.Create<RequestOrder>();
            var errorMessage = "SQL connection failed";
            var sqlException = new InvalidSqlException(errorMessage);

            _orderServiceMock.Setup(service =>
                service.Add_Order(It.IsAny<RequestOrder>()))
                .ThrowsAsync(sqlException);

            _controller = new OrdersController(_orderServiceMock.Object);

            // Act
            var result = await _controller.Add_Order(order);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<RequestOrder>));

            var actionResult = result as ActionResult<RequestOrder>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));

            var badRequestResult = actionResult.Result as BadRequestObjectResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.IsInstanceOfType(badRequestResult.Value, typeof(Error));

            var error = badRequestResult.Value as Error;
            Assert.AreEqual(500, error.ID);
            Assert.AreEqual(errorMessage, error.Message);
        }

        [TestMethod]
        public async Task GetUnAvailableDate_ReturnsOk()
        {
            // Arrange
            var date = _fixture.Create<DateTime>();
            var availabilityList = _fixture.Create<List<AvailabilityDTO>>();

            _orderServiceMock.Setup(service =>
                service.GetUnAvailableDate(date))
                .ReturnsAsync(availabilityList);

            _controller = new OrdersController(_orderServiceMock.Object);

            // Act
            var result = await _controller.GetUnAvailableDate(date);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<AvailabilityDTO>>));

            var actionResult = result as ActionResult<List<AvailabilityDTO>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));

            var okResult = actionResult.Result as OkObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.AreEqual(availabilityList, okResult.Value);
        }

        [TestMethod]
        public async Task GetAvailableTimeSlot_ReturnsOk()
        {
            // Arrange
            var date = _fixture.Create<DateTime>();
            var availabilityList = _fixture.Create<List<AvailabilityDTO>>();

            _orderServiceMock.Setup(service =>
                service.GetAvailableTimeSlot(date))
                .ReturnsAsync(availabilityList);

            _controller = new OrdersController(_orderServiceMock.Object);

            // Act
            var result = await _controller.GetAvailableTimeSlot(date);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<AvailabilityDTO>>));

            var actionResult = result as ActionResult<List<AvailabilityDTO>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));

            var okResult = actionResult.Result as OkObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.AreEqual(availabilityList, okResult.Value);
        }
    }
}
