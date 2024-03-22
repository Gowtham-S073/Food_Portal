using AutoFixture;
using FoodPortal.Controllers;
using FoodPortal.Exceptions;
using FoodPortal.Interfaces;
using FoodPortal.Models;
using FoodPortal.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
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
    public class TimeSlotsControllerTest
    {
        private Mock<ITimeSlotService> _timeSlotServiceMock;
        private Fixture _fixture;
        private TimeSlotsController _controller;

        public TimeSlotsControllerTest()
        {
            _fixture = new Fixture();
            _timeSlotServiceMock = new Mock<ITimeSlotService>();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TestMethod]
        public async Task View_All_TimeSlots_ReturnsOkWithTimeSlots()
        {
            var timeslots = _fixture.CreateMany<ViewTimeSlot>(3).ToList();

            _timeSlotServiceMock.Setup(service =>
                service.View_All_TimeSlots())
                .ReturnsAsync(timeslots);

            _controller = new TimeSlotsController(_timeSlotServiceMock.Object);

            // Act
            var result = await _controller.View_All_TimeSlots();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ViewTimeSlot>>));

            var actionResult = result as ActionResult<List<ViewTimeSlot>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));

            var okResult = actionResult.Result as OkObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.IsInstanceOfType(okResult.Value, typeof(List<ViewTimeSlot>));

            var returnedtimeslots = okResult.Value as List<ViewTimeSlot>;
            Assert.AreEqual(timeslots.Count, returnedtimeslots.Count);
        }
        [TestMethod]
        public async Task View_All_TimeSlots_ReturnsBadRequestOnNullException()
        {
            // Arrange
            var errorMessage = "Null Exception message";

            _timeSlotServiceMock.Setup(service =>
                service.View_All_TimeSlots())
                .ThrowsAsync(new NullException(errorMessage));

            _controller = new TimeSlotsController(_timeSlotServiceMock.Object);

            // Act
            var result = await _controller.View_All_TimeSlots();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ViewTimeSlot>>));

            var actionResult = result as ActionResult<List<ViewTimeSlot>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundObjectResult));

            var notFoundResult = actionResult.Result as NotFoundObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
            Assert.IsInstanceOfType(notFoundResult.Value, typeof(Error));

            var error = notFoundResult.Value as Error;
            Assert.AreEqual(404, error.ID);
            Assert.AreEqual(errorMessage, error.Message);
        }

        [TestMethod]
        public async Task View_All_TimeSlots_ReturnsBadRequestOnInvalidSqlException()
        {
            // Arrange
            var errorMessage = "Sql Exception message";

            _timeSlotServiceMock.Setup(service =>
                service.View_All_TimeSlots())
                .ThrowsAsync(new InvalidSqlException(errorMessage));

            _controller = new TimeSlotsController(_timeSlotServiceMock.Object);

            // Act
            var result = await _controller.View_All_TimeSlots();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ViewTimeSlot>>));

            var actionResult = result as ActionResult<List<ViewTimeSlot>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));

            var badRequestResult = actionResult.Result as BadRequestObjectResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.IsInstanceOfType(badRequestResult.Value, typeof(Error));

            var error = badRequestResult.Value as Error;
            Assert.AreEqual(500, error.ID);
            Assert.AreEqual(errorMessage, error.Message);
        }
    }
}
