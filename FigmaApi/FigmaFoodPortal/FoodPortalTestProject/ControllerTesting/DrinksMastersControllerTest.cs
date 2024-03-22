using AutoFixture;
using FoodPortal.Controllers;
using FoodPortal.Exceptions;
using FoodPortal.Interfaces;
using FoodPortal.Models;
using FoodPortal.ViewModel;
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
    public class DrinksMastersControllerTest
    {
        private Mock<IDrinksMasterService> _drinksMasterServiceMock;
        private Fixture _fixture;
        private DrinksMastersController _controller;

        public DrinksMastersControllerTest()
        {
            _fixture = new Fixture();
            _drinksMasterServiceMock = new Mock<IDrinksMasterService>();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
             .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TestMethod]
        public async Task View_All_DrinksMasters_ReturnsOk()
        {
            // Arrange
            var drinksMasters = _fixture.CreateMany<ViewDrinksMaster>(3).ToList();

            _drinksMasterServiceMock.Setup(service =>
                service.View_All_DrinksMasters())
                .ReturnsAsync(drinksMasters);

            _controller = new DrinksMastersController(_drinksMasterServiceMock.Object);

            // Act
            var result = await _controller.View_All_DrinksMasters();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ViewDrinksMaster>>));

            var actionResult = result as ActionResult<List<ViewDrinksMaster>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));

            var okResult = actionResult.Result as OkObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.IsInstanceOfType(okResult.Value, typeof(List<ViewDrinksMaster>));
        }
        [TestMethod]
        public async Task View_All_DrinksMasters_ReturnsBadRequestOnNullException()
        {
            // Arrange
            var errorMessage = "Null exception happend";
            var nullException = new NullException(errorMessage);

            _drinksMasterServiceMock.Setup(service =>
                service.View_All_DrinksMasters())
                .ThrowsAsync(nullException);

            _controller = new DrinksMastersController(_drinksMasterServiceMock.Object);

            // Act
            var result = await _controller.View_All_DrinksMasters();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ViewDrinksMaster>>));

            var actionResult = result as ActionResult<List<ViewDrinksMaster>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundObjectResult));

            var notFoundResult = actionResult.Result as NotFoundObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
            Assert.IsInstanceOfType(notFoundResult.Value, typeof(Error));

            var error = notFoundResult.Value as Error;
            Assert.AreEqual(404, error.ID);
            Assert.AreEqual(errorMessage, error.Message);
        }
        [TestMethod]
        public async Task View_All_DrinksMasters_ReturnsBadRequestOnSqlException()
        {
            // Arrange
            var errorMessage = "SQL connection failed";
            var sqlException = new InvalidSqlException(errorMessage);

            _drinksMasterServiceMock.Setup(service =>
                service.View_All_DrinksMasters())
                .ThrowsAsync(sqlException);

            _controller = new DrinksMastersController(_drinksMasterServiceMock.Object);

            // Act
            var result = await _controller.View_All_DrinksMasters();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ViewDrinksMaster>>));

            var actionResult = result as ActionResult<List<ViewDrinksMaster>>;

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
