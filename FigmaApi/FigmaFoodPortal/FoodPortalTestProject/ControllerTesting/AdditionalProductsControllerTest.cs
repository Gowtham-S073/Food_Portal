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
    public class AdditionalProductsControllerTest
    {
        private Mock<IAdditionalProductService> _additionalProductServiceMock;
        private Fixture _fixture;
        private AdditionalProductsController _controller;


        public AdditionalProductsControllerTest()
        {
            _fixture = new Fixture();
            _additionalProductServiceMock = new Mock<IAdditionalProductService>();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
             .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        }






        [TestMethod]
        public async Task View_by_category_AdditionalProducts_ReturnsOkWithList()
        {
            // Arrange
            var categoryId = 123;
            var additionalProducts = _fixture.CreateMany<ViewAdditionalProduct>(3).ToList();

            _additionalProductServiceMock.Setup(service =>
                service.View_by_category_AdditionalProducts(categoryId))
                .ReturnsAsync(additionalProducts);

            _controller = new AdditionalProductsController(_additionalProductServiceMock.Object);

            // Act
            var result = await _controller.View_by_category_AdditionalProducts(categoryId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ViewAdditionalProduct>>));

            var actionResult = result as ActionResult<List<ViewAdditionalProduct>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));

            var okResult = actionResult.Result as OkObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.IsInstanceOfType(okResult.Value, typeof(List<ViewAdditionalProduct>));

            var returnedAdditionalProducts = okResult.Value as List<ViewAdditionalProduct>;
            Assert.AreEqual(additionalProducts.Count, returnedAdditionalProducts.Count);
        }
        [TestMethod]
        public async Task View_by_category_AdditionalProducts_ReturnsBadRequestOnNullException()
        {
            // Arrange
            var categoryId = 123;
            var errorMessage = "null exception happened";
            var nullException = new NullException(errorMessage);

            _additionalProductServiceMock.Setup(service =>
                service.View_by_category_AdditionalProducts(categoryId))
                .ThrowsAsync(nullException);

            _controller = new AdditionalProductsController(_additionalProductServiceMock.Object);

            // Act
            var result = await _controller.View_by_category_AdditionalProducts(categoryId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ViewAdditionalProduct>>));

            var actionResult = result as ActionResult<List<ViewAdditionalProduct>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundObjectResult));

            var notFoundResult = actionResult.Result as NotFoundObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
            Assert.IsInstanceOfType(notFoundResult.Value, typeof(Error));

            var error = notFoundResult.Value as Error;
            Assert.AreEqual(404, error.ID);
            Assert.AreEqual(errorMessage, error.Message);
        }


        [TestMethod]
        public async Task View_by_category_AdditionalProducts_ReturnsBadRequestOnSqlException()
        {
            // Arrange
            var categoryId = 123;
            var errorMessage = "SQL connection failed";
            var sqlException = new InvalidSqlException(errorMessage);

            _additionalProductServiceMock.Setup(service =>
                service.View_by_category_AdditionalProducts(categoryId))
                .ThrowsAsync(sqlException);

            _controller = new AdditionalProductsController(_additionalProductServiceMock.Object);

            // Act
            var result = await _controller.View_by_category_AdditionalProducts(categoryId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ViewAdditionalProduct>>));

            var actionResult = result as ActionResult<List<ViewAdditionalProduct>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));

            var badRequestResult = actionResult.Result as BadRequestObjectResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.IsInstanceOfType(badRequestResult.Value, typeof(Error));

            var error = badRequestResult.Value as Error;
            Assert.AreEqual(500, error.ID);
            Assert.AreEqual(errorMessage, error.Message);
        }

        [TestMethod]
        public async Task View_by_foodtype_AdditionalProducts_ReturnsOkWithList()
        {
            // Arrange
            var isVeg = true;
            var categoryId = 123;
            var additionalProducts = _fixture.CreateMany<ViewAdditionalProduct>(3).ToList();

            _additionalProductServiceMock.Setup(service =>
                service.View_by_foodtype_AdditionalProducts(isVeg, categoryId))
                .ReturnsAsync(additionalProducts);

            _controller = new AdditionalProductsController(_additionalProductServiceMock.Object);

            // Act
            var result = await _controller.View_by_foodtype_AdditionalProducts(isVeg, categoryId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ViewAdditionalProduct>>));

            var actionResult = result as ActionResult<List<ViewAdditionalProduct>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));

            var okResult = actionResult.Result as OkObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.IsInstanceOfType(okResult.Value, typeof(List<ViewAdditionalProduct>));

            var returnedAdditionalProducts = okResult.Value as List<ViewAdditionalProduct>;
            Assert.AreEqual(additionalProducts.Count, returnedAdditionalProducts.Count);
        }

        [TestMethod]
        public async Task View_by_foodtype_AdditionalProducts_ReturnsBadRequestOnNullException()
        {
            // Arrange
            var isVeg = true;
            var categoryId = 123;
            var errorMessage = "Null exception happened";
            var exception = new NullException(errorMessage);

            _additionalProductServiceMock.Setup(service =>
                service.View_by_foodtype_AdditionalProducts(isVeg, categoryId))
                .ThrowsAsync(exception);

            _controller = new AdditionalProductsController(_additionalProductServiceMock.Object);

            // Act
            var result = await _controller.View_by_foodtype_AdditionalProducts(isVeg, categoryId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ViewAdditionalProduct>>));

            var actionResult = result as ActionResult<List<ViewAdditionalProduct>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundObjectResult));

            var notFoundResult = actionResult.Result as NotFoundObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
            Assert.IsInstanceOfType(notFoundResult.Value, typeof(Error));

            var error = notFoundResult.Value as Error;
            Assert.AreEqual(404, error.ID);
            Assert.AreEqual(errorMessage, error.Message);
        }



        [TestMethod]
        public async Task View_by_foodtype_AdditionalProducts_ReturnsBadRequestOnSqlException()
        {
            // Arrange
            var isVeg = true;
            var categoryId = 123;
            var errorMessage = "SQL connection failed";
            var sqlException = new InvalidSqlException(errorMessage);

            _additionalProductServiceMock.Setup(service =>
                service.View_by_foodtype_AdditionalProducts(isVeg, categoryId))
                .ThrowsAsync(sqlException);

            _controller = new AdditionalProductsController(_additionalProductServiceMock.Object);

            // Act
            var result = await _controller.View_by_foodtype_AdditionalProducts(isVeg, categoryId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ViewAdditionalProduct>>));

            var actionResult = result as ActionResult<List<ViewAdditionalProduct>>;

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
