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
    public class StdProductsControllerTest
    {
        private Mock<IStdProductService> _stdProductServiceMock;
        private Fixture _fixture;
        private StdProductsController _controller;

        public StdProductsControllerTest()
        {
            _fixture = new Fixture();
            _stdProductServiceMock = new Mock<IStdProductService>();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TestMethod]
        public async Task View_All_StdProducts_ReturnsOkWithStdProducts()
        {
            // Arrange
            var expectedStdProducts = _fixture.CreateMany<ViewStdProduct>().ToList();

            _stdProductServiceMock.Setup(service =>
                service.View_All_StdProducts())
                .ReturnsAsync(expectedStdProducts);

            _controller = new StdProductsController(_stdProductServiceMock.Object);

            // Act
            var result = await _controller.View_All_StdProducts();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ViewStdProduct>>));

            var actionResult = result as ActionResult<List<ViewStdProduct>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));

            var okResult = actionResult.Result as OkObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.AreSame(expectedStdProducts, okResult.Value);
        }
        [TestMethod]
        public async Task View_All_StdProducts_ReturnsBadRequestOnNullException()
        {
            // Arrange
            var errorMessage = "Null Exception message";
            _stdProductServiceMock.Setup(service =>
                service.View_All_StdProducts())
                .ThrowsAsync(new NullException(errorMessage));

            _controller = new StdProductsController(_stdProductServiceMock.Object);

            // Act
            var result = await _controller.View_All_StdProducts();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ViewStdProduct>>));

            var actionResult = result as ActionResult<List<ViewStdProduct>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundObjectResult));

            var notFoundResult = actionResult.Result as NotFoundObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
            Assert.IsInstanceOfType(notFoundResult.Value, typeof(Error));

            var error = notFoundResult.Value as Error;
            Assert.AreEqual(404, error.ID);
            Assert.AreEqual(errorMessage, error.Message);
        }


        [TestMethod]
        public async Task View_All_StdProducts_ReturnsBadRequestOnInvalidSqlException()
        {
            // Arrange
            var errorMessage = "Invalid SQL Exception message";
            _stdProductServiceMock.Setup(service =>
                service.View_All_StdProducts())
                .ThrowsAsync(new InvalidSqlException(errorMessage));

            _controller = new StdProductsController(_stdProductServiceMock.Object);

            // Act
            var result = await _controller.View_All_StdProducts();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ViewStdProduct>>));

            var actionResult = result as ActionResult<List<ViewStdProduct>>;

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
