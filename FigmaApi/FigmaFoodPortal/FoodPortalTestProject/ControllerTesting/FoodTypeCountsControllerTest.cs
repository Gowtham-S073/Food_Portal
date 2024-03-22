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
    public class FoodTypeCountsControllerTest
    {
        private Mock<IFoodTypeCountService> _foodTypeCountServiceMock;
        private Fixture _fixture;
        private FoodTypeCountsController _controller;

        public FoodTypeCountsControllerTest()
        {
            _fixture = new Fixture();
            _foodTypeCountServiceMock = new Mock<IFoodTypeCountService>();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
             .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TestMethod]
        public async Task Add_FoodTypeCount_ReturnsCreated()
        {
            // Arrange
            var foodTypeCounts = _fixture.CreateMany<RequestFoodTypeCount>(3).ToList();

            _foodTypeCountServiceMock.Setup(service =>
                service.Add_FoodTypeCount(It.IsAny<List<RequestFoodTypeCount>>()))
                .ReturnsAsync(foodTypeCounts);

            _controller = new FoodTypeCountsController(_foodTypeCountServiceMock.Object);

            // Act
            var result = await _controller.Add_FoodTypeCount(foodTypeCounts);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<RequestFoodTypeCount>>));

            var actionResult = result as ActionResult<List<RequestFoodTypeCount>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedResult));

            var createdResult = actionResult.Result as CreatedResult;

            Assert.AreEqual(StatusCodes.Status201Created, createdResult.StatusCode);
            Assert.IsInstanceOfType(createdResult.Value, typeof(List<RequestFoodTypeCount>));
        }

        [TestMethod]
        public async Task Add_FoodTypeCount_ReturnsBadRequestOnNullException()
        {
            // Arrange
            var fixture = new Fixture();
            var errorMessage = "null exception happened";
            var nullException = new NullException(errorMessage);

            _foodTypeCountServiceMock.Setup(service =>
                service.Add_FoodTypeCount(It.IsAny<List<RequestFoodTypeCount>>()))
                .ThrowsAsync(nullException);

            _controller = new FoodTypeCountsController(_foodTypeCountServiceMock.Object);

            // Act
            var result = await _controller.Add_FoodTypeCount(new List<RequestFoodTypeCount>());

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<RequestFoodTypeCount>>));

            var actionResult = result as ActionResult<List<RequestFoodTypeCount>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundObjectResult));

            var notFoundResult = actionResult.Result as NotFoundObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
            Assert.IsInstanceOfType(notFoundResult.Value, typeof(Error));

            var error = notFoundResult.Value as Error;
            Assert.AreEqual(404, error.ID);
            Assert.AreEqual(errorMessage, error.Message);
        }
        [TestMethod]
        public async Task Add_FoodTypeCount_ReturnsBadRequestOnDuplicateRecordException()
        {
            // Arrange
            var fixture = new Fixture();
            var errorMessage = "Duplicate Record Exception happened";
            var duplicate = new DuplicateRecordException(errorMessage);

            _foodTypeCountServiceMock.Setup(service =>
                service.Add_FoodTypeCount(It.IsAny<List<RequestFoodTypeCount>>()))
                .ThrowsAsync(duplicate);

            _controller = new FoodTypeCountsController(_foodTypeCountServiceMock.Object);

            // Act
            var result = await _controller.Add_FoodTypeCount(new List<RequestFoodTypeCount>());

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<RequestFoodTypeCount>>));

            var actionResult = result as ActionResult<List<RequestFoodTypeCount>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));

            var badRequestResult = actionResult.Result as BadRequestObjectResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.IsInstanceOfType(badRequestResult.Value, typeof(Error));

            var error = badRequestResult.Value as Error;
            Assert.AreEqual(400, error.ID);
            Assert.AreEqual(errorMessage, error.Message);
        }
        [TestMethod]
        public async Task Add_FoodTypeCount_ReturnsBadRequestOnSqlException()
        {
            // Arrange
            var fixture = new Fixture();
            var errorMessage = "SQL connection failed";
            var sqlException = new InvalidSqlException(errorMessage);

            _foodTypeCountServiceMock.Setup(service =>
                service.Add_FoodTypeCount(It.IsAny<List<RequestFoodTypeCount>>()))
                .ThrowsAsync(sqlException);

            _controller = new FoodTypeCountsController(_foodTypeCountServiceMock.Object);

            // Act
            var result = await _controller.Add_FoodTypeCount(new List<RequestFoodTypeCount>());

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<RequestFoodTypeCount>>));

            var actionResult = result as ActionResult<List<RequestFoodTypeCount>>;

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
