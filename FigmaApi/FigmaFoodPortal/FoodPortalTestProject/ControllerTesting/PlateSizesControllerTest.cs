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
    public class PlateSizesControllerTest
    {
        private Mock<IPlateSizeService> _plateSizeServiceMock;
        private Fixture _fixture;
        private PlateSizesController _controller;

        public PlateSizesControllerTest()
        {
            _fixture = new Fixture();
            _plateSizeServiceMock = new Mock<IPlateSizeService>();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
             .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
        [TestMethod]
        public async Task View_All_PlateSizes_ReturnsOk()
        {
            // Arrange
            var plateSizeList = _fixture.Create<List<ViewPlateSize>>();

            _plateSizeServiceMock.Setup(service =>
                service.View_All_PlateSizes())
                .ReturnsAsync(plateSizeList);

            _controller = new PlateSizesController(_plateSizeServiceMock.Object);

            // Act
            var result = await _controller.View_All_PlateSizes();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ViewPlateSize>>));

            var actionResult = result as ActionResult<List<ViewPlateSize>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));

            var okResult = actionResult.Result as OkObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.AreEqual(plateSizeList, okResult.Value);
        }
        [TestMethod]
        public async Task View_All_PlateSizes_ReturnsBadRequestOnNullException()
        {
            // Arrange
            _plateSizeServiceMock.Setup(service =>
                service.View_All_PlateSizes())
                .ThrowsAsync(new NullException("null Exception happened"));

            _controller = new PlateSizesController(_plateSizeServiceMock.Object);

            // Act
            var result = await _controller.View_All_PlateSizes();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ViewPlateSize>>));

            var actionResult = result as ActionResult<List<ViewPlateSize>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundObjectResult));

            var notFoundResult = actionResult.Result as NotFoundObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
            Assert.IsInstanceOfType(notFoundResult.Value, typeof(Error));

            var error = notFoundResult.Value as Error;
            Assert.AreEqual(404, error.ID);
            Assert.AreEqual("null Exception happened", error.Message);
        }


        [TestMethod]
        public async Task View_All_PlateSizes_ReturnsBadRequestOnSqlException()
        {
            // Arrange
            _plateSizeServiceMock.Setup(service =>
                service.View_All_PlateSizes())
                .ThrowsAsync(new InvalidSqlException("Test SQL Exception"));

            _controller = new PlateSizesController(_plateSizeServiceMock.Object);

            // Act
            var result = await _controller.View_All_PlateSizes();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ViewPlateSize>>));

            var actionResult = result as ActionResult<List<ViewPlateSize>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));

            var badRequestResult = actionResult.Result as BadRequestObjectResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.IsInstanceOfType(badRequestResult.Value, typeof(Error));

            var error = badRequestResult.Value as Error;
            Assert.AreEqual(500, error.ID);
            Assert.AreEqual("Test SQL Exception", error.Message);
        }
    }
}
