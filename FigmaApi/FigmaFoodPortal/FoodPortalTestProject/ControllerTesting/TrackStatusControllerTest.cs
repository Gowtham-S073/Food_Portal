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
    public class TrackStatusControllerTest
    {
        private Mock<ITrackStatusService> _trackStatusServiceMock;
        private Fixture _fixture;
        private TrackStatusController _controller;

        public TrackStatusControllerTest()
        {
            _fixture = new Fixture();
            _trackStatusServiceMock = new Mock<ITrackStatusService>();
            _controller = new TrackStatusController(_trackStatusServiceMock.Object);
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
             .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
        [TestMethod]
        public async Task Add_TrackStatus_ReturnsCreatedResult()
        {
            // Arrange
            var requestTrackStatus = _fixture.Create<RequestTrackStatus>();

            _trackStatusServiceMock.Setup(service =>
                service.Add_TrackStatus(requestTrackStatus))
                .ReturnsAsync(requestTrackStatus);

            // Act
            var result = await _controller.Add_TrackStatus(requestTrackStatus);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<RequestTrackStatus>));

            var actionResult = result as ActionResult<RequestTrackStatus>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedResult));

            var createdResult = actionResult.Result as CreatedResult;

            Assert.AreEqual(StatusCodes.Status201Created, createdResult.StatusCode);
            Assert.IsInstanceOfType(createdResult.Value, typeof(RequestTrackStatus));

            var createdOrder = createdResult.Value as RequestTrackStatus;
            Assert.AreEqual(requestTrackStatus.Id, createdOrder.Id); // Ensure IDs match or adjust accordingly
        }

        [TestMethod]
        public async Task Add_TrackStatus_ReturnsNotFoundResult()
        {
            // Arrange
            var requestTrackStatus = _fixture.Create<RequestTrackStatus>();

            _trackStatusServiceMock.Setup(service =>
                service.Add_TrackStatus(requestTrackStatus))
                .ThrowsAsync(new NullException("Not found"));

            // Act
            var result = await _controller.Add_TrackStatus(requestTrackStatus);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<RequestTrackStatus>));

            var actionResult = result as ActionResult<RequestTrackStatus>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundObjectResult));

            var notFoundResult = actionResult.Result as NotFoundObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
            Assert.IsInstanceOfType(notFoundResult.Value, typeof(Error));

            var error = notFoundResult.Value as Error;
            Assert.AreEqual(404, error.ID);
            Assert.AreEqual("Not found", error.Message);
        }

        [TestMethod]
        public async Task Add_TrackStatus_ReturnsBadRequestResult()
        {
            // Arrange
            var requestTrackStatus = _fixture.Create<RequestTrackStatus>();

            _trackStatusServiceMock.Setup(service =>
                service.Add_TrackStatus(requestTrackStatus))
                .ThrowsAsync(new DuplicateRecordException("Duplicate record"));

            // Act
            var result = await _controller.Add_TrackStatus(requestTrackStatus);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<RequestTrackStatus>));

            var actionResult = result as ActionResult<RequestTrackStatus>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));

            var badRequestResult = actionResult.Result as BadRequestObjectResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.IsInstanceOfType(badRequestResult.Value, typeof(Error));

            var error = badRequestResult.Value as Error;
            Assert.AreEqual(400, error.ID);
            Assert.AreEqual("Duplicate record", error.Message);
        }

        [TestMethod]
        public async Task Add_TrackStatus_ReturnsInternalServerErrorResult()
        {
            // Arrange
            var requestTrackStatus = _fixture.Create<RequestTrackStatus>();

            _trackStatusServiceMock.Setup(service =>
                service.Add_TrackStatus(requestTrackStatus))
                .ThrowsAsync(new InvalidSqlException("Invalid SQL operation"));

            // Act
            var result = await _controller.Add_TrackStatus(requestTrackStatus);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<RequestTrackStatus>));

            var actionResult = result as ActionResult<RequestTrackStatus>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));

            var badRequestResult = actionResult.Result as BadRequestObjectResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.IsInstanceOfType(badRequestResult.Value, typeof(Error));

            var error = badRequestResult.Value as Error;
            Assert.AreEqual(500, error.ID);
            Assert.AreEqual("Invalid SQL operation", error.Message);
        }


        [TestMethod]
        public async Task Get_Order_Summary_ReturnsOkWithTrackDTO()
        {
            // Arrange
            var id = _fixture.Create<IdDTO>();
            var trackDTO = _fixture.Create<TrackDTO>();

            _trackStatusServiceMock.Setup(service =>
                service.Get_Order_Summary(id.IdString))
                .ReturnsAsync(trackDTO);

            // Act
            var result = await _controller.Get_Order_Summary(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<TrackDTO>));

            var actionResult = result as ActionResult<TrackDTO>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));

            var okResult = actionResult.Result as OkObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.IsInstanceOfType(okResult.Value, typeof(TrackDTO));

            var returnedTrackDTO = okResult.Value as TrackDTO;
        }


        [TestMethod]
        public async Task Get_Order_Summary_ReturnsBadRequestOnNullException()
        {
            // Arrange
            var id = _fixture.Create<IdDTO>();
            var errorMessage = "Null Exception message";

            _trackStatusServiceMock.Setup(service =>
                service.Get_Order_Summary(id.IdString))
                .ThrowsAsync(new NullException(errorMessage));


            // Act
            var result = await _controller.Get_Order_Summary(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<TrackDTO>));

            var actionResult = result as ActionResult<TrackDTO>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundObjectResult));

            var notFoundResult = actionResult.Result as NotFoundObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
            Assert.IsInstanceOfType(notFoundResult.Value, typeof(Error));

            var error = notFoundResult.Value as Error;
            Assert.AreEqual(404, error.ID);
            Assert.AreEqual(errorMessage, error.Message);
        }


        [TestMethod]
        public async Task Get_Order_Summary_ReturnsBadRequestOnInvalidSqlException()
        {
            // Arrange
            var id = _fixture.Create<IdDTO>();
            var errorMessage = "Sql Exception message";

            _trackStatusServiceMock.Setup(service =>
                service.Get_Order_Summary(id.IdString))
                .ThrowsAsync(new InvalidSqlException(errorMessage));

            // Act
            var result = await _controller.Get_Order_Summary(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<TrackDTO>));

            var actionResult = result as ActionResult<TrackDTO>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));

            var badRequestResult = actionResult.Result as BadRequestObjectResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.IsInstanceOfType(badRequestResult.Value, typeof(Error));

            var error = badRequestResult.Value as Error;
            Assert.AreEqual(500, error.ID);
            Assert.AreEqual(errorMessage, error.Message);
        }


        [TestMethod]
        public async Task Check_TrackId_ReturnsOkWithIdDTO()
        {
            // Arrange
            var id = _fixture.Create<IdDTO>();
            var username = "sampleUsername";
            _trackStatusServiceMock.Setup(service =>
                service.check_trackid(It.IsAny<IdDTO>(), It.IsAny<string>()))
                .ReturnsAsync(id);


            // Act
            var result = await _controller.check_trackid(id, username);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<IdDTO>));

            var actionResult = result as ActionResult<IdDTO>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));

            var okResult = actionResult.Result as OkObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.IsInstanceOfType(okResult.Value, typeof(IdDTO));
        }
        [TestMethod]
        public async Task Check_TrackId_ReturnsBadRequestOnNullException()
        {
            // Arrange
            var id = _fixture.Create<IdDTO>();
            var username = "sampleUsername";
            var errorMessage = "Null Exception message";

            _trackStatusServiceMock.Setup(service =>
                service.check_trackid(id, username))
                .ThrowsAsync(new NullException(errorMessage));


            // Act
            var result = await _controller.check_trackid(id, username);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<IdDTO>));

            var actionResult = result as ActionResult<IdDTO>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundObjectResult));

            var notFoundResult = actionResult.Result as NotFoundObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
            Assert.IsInstanceOfType(notFoundResult.Value, typeof(Error));

            var error = notFoundResult.Value as Error;
            Assert.AreEqual(404, error.ID);
            Assert.AreEqual(errorMessage, error.Message);
        }


        [TestMethod]
        public async Task Check_TrackId_ReturnsBadRequestOnInvalidSqlException()
        {
            // Arrange
            var id = _fixture.Create<IdDTO>();
            var username = "sampleUsername";
            var errorMessage = "Sql Exception message";

            _trackStatusServiceMock.Setup(service =>
                service.check_trackid(id, username))
                .ThrowsAsync(new InvalidSqlException(errorMessage));


            // Act
            var result = await _controller.check_trackid(id, username);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<IdDTO>));

            var actionResult = result as ActionResult<IdDTO>;

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
