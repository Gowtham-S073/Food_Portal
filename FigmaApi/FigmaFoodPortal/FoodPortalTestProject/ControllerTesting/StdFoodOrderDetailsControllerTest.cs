using AutoFixture;
using FoodPortal.Controllers;
using FoodPortal.Exceptions;
using FoodPortal.Interfaces;
using FoodPortal.Models;
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
    public class StdFoodOrderDetailsControllerTest
    {
        private Mock<IStdFoodOrderDetailService> _stdFoodOrderDetailServiceMock;
        private Fixture _fixture;
        private StdFoodOrderDetailsController _controller;

        public StdFoodOrderDetailsControllerTest()
        {
            _fixture = new Fixture();
            _stdFoodOrderDetailServiceMock = new Mock<IStdFoodOrderDetailService>();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
             .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TestMethod]
        public async Task Add_StdFoodOrderDetail_ReturnsCreated()
        {
            // Arrange
            var stdFoodOrderDetails = _fixture.CreateMany<RequestStdFoodOrderDetail>().ToList();

            _stdFoodOrderDetailServiceMock.Setup(service =>
                service.Add_StdFoodOrderDetail(It.IsAny<List<RequestStdFoodOrderDetail>>()))
                .ReturnsAsync(stdFoodOrderDetails);

            _controller = new StdFoodOrderDetailsController(_stdFoodOrderDetailServiceMock.Object);

            // Act
            var result = await _controller.Add_StdFoodOrderDetail(stdFoodOrderDetails);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<RequestStdFoodOrderDetail>>));

            var actionResult = result as ActionResult<List<RequestStdFoodOrderDetail>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedResult));

            var createdResult = actionResult.Result as CreatedResult;

            Assert.AreEqual(StatusCodes.Status201Created, createdResult.StatusCode);
            Assert.IsInstanceOfType(createdResult.Value, typeof(List<RequestStdFoodOrderDetail>));

            var createdStdFoodOrderDetails = createdResult.Value as List<RequestStdFoodOrderDetail>;
            Assert.AreEqual(stdFoodOrderDetails.Count, createdStdFoodOrderDetails.Count);
        }
        [TestMethod]
        public async Task Add_StdFoodOrderDetail_ReturnsBadRequestOnNullException()
        {
            // Arrange
            var stdFoodOrderDetails = _fixture.CreateMany<RequestStdFoodOrderDetail>().ToList();

            _stdFoodOrderDetailServiceMock.Setup(service =>
                service.Add_StdFoodOrderDetail(It.IsAny<List<RequestStdFoodOrderDetail>>()))
                .ThrowsAsync(new NullException("Null exception"));

            _controller = new StdFoodOrderDetailsController(_stdFoodOrderDetailServiceMock.Object);

            // Act
            var result = await _controller.Add_StdFoodOrderDetail(stdFoodOrderDetails);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<RequestStdFoodOrderDetail>>));

            var actionResult = result as ActionResult<List<RequestStdFoodOrderDetail>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundObjectResult));

            var notFoundResult = actionResult.Result as NotFoundObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
            Assert.IsInstanceOfType(notFoundResult.Value, typeof(Error));

            var error = notFoundResult.Value as Error;
            Assert.AreEqual(404, error.ID);
            Assert.AreEqual("Null exception", error.Message);
        }
        [TestMethod]
        public async Task Add_StdFoodOrderDetail_ReturnsBadRequestOnDuplicateRecordException()
        {
            // Arrange
            var stdFoodOrderDetails = _fixture.CreateMany<RequestStdFoodOrderDetail>().ToList();

            _stdFoodOrderDetailServiceMock.Setup(service =>
                service.Add_StdFoodOrderDetail(It.IsAny<List<RequestStdFoodOrderDetail>>()))
                .ThrowsAsync(new DuplicateRecordException("Duplicate Record Exception"));

            _controller = new StdFoodOrderDetailsController(_stdFoodOrderDetailServiceMock.Object);

            // Act
            var result = await _controller.Add_StdFoodOrderDetail(stdFoodOrderDetails);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<RequestStdFoodOrderDetail>>));

            var actionResult = result as ActionResult<List<RequestStdFoodOrderDetail>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));

            var badRequestResult = actionResult.Result as BadRequestObjectResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.IsInstanceOfType(badRequestResult.Value, typeof(Error));

            var error = badRequestResult.Value as Error;
            Assert.AreEqual(400, error.ID);
            Assert.AreEqual("Duplicate Record Exception", error.Message);
        }

        [TestMethod]
        public async Task Add_StdFoodOrderDetail_ReturnsBadRequestOnInvalidSqlException()
        {
            // Arrange
            var stdFoodOrderDetails = _fixture.CreateMany<RequestStdFoodOrderDetail>().ToList();

            _stdFoodOrderDetailServiceMock.Setup(service =>
                service.Add_StdFoodOrderDetail(It.IsAny<List<RequestStdFoodOrderDetail>>()))
                .ThrowsAsync(new InvalidSqlException("Invalid SQL"));

            _controller = new StdFoodOrderDetailsController(_stdFoodOrderDetailServiceMock.Object);

            // Act
            var result = await _controller.Add_StdFoodOrderDetail(stdFoodOrderDetails);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<RequestStdFoodOrderDetail>>));

            var actionResult = result as ActionResult<List<RequestStdFoodOrderDetail>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));

            var badRequestResult = actionResult.Result as BadRequestObjectResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.IsInstanceOfType(badRequestResult.Value, typeof(Error));

            var error = badRequestResult.Value as Error;
            Assert.AreEqual(500, error.ID);
            Assert.AreEqual("Invalid SQL", error.Message);
        }
    }
}
