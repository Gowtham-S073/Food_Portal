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
    public class GroupSizesControllerTest
    {
        private Mock<IGroupSizeService> _groupSizeServiceMock;
        private Fixture _fixture;
        private GroupSizesController _controller;

        public GroupSizesControllerTest()
        {
            _fixture = new Fixture();
            _groupSizeServiceMock = new Mock<IGroupSizeService>();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
             .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TestMethod]
        public async Task View_All_GroupSizes_ReturnsOk()
        {
            // Arrange
            var groupSizes = _fixture.CreateMany<ViewGroupSize>(3).ToList();

            _groupSizeServiceMock.Setup(service =>
                service.View_All_GroupSizes())
                .ReturnsAsync(groupSizes);

            _controller = new GroupSizesController(_groupSizeServiceMock.Object);

            // Act
            var result = await _controller.View_All_GroupSizes();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ViewGroupSize>>));

            var actionResult = result as ActionResult<List<ViewGroupSize>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));

            var okResult = actionResult.Result as OkObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.IsInstanceOfType(okResult.Value, typeof(List<ViewGroupSize>));

            var returnedGroupSizes = okResult.Value as List<ViewGroupSize>;
            Assert.AreEqual(groupSizes.Count, returnedGroupSizes.Count);
        }

        [TestMethod]
        public async Task View_All_GroupSizes_ReturnsBadRequestOnNullException()
        {
            // Arrange
            var errorMessage = "Null exception happened";
            var nullexception = new NullException(errorMessage);

            _groupSizeServiceMock.Setup(service =>
                service.View_All_GroupSizes())
                .ThrowsAsync(nullexception);

            _controller = new GroupSizesController(_groupSizeServiceMock.Object);

            // Act
            var result = await _controller.View_All_GroupSizes();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ViewGroupSize>>));

            var actionResult = result as ActionResult<List<ViewGroupSize>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundObjectResult));

            var notFoundResult = actionResult.Result as NotFoundObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
            Assert.IsInstanceOfType(notFoundResult.Value, typeof(Error));

            var error = notFoundResult.Value as Error;
            Assert.AreEqual(404, error.ID);
            Assert.AreEqual(errorMessage, error.Message);
        }


        [TestMethod]
        public async Task View_All_GroupSizes_ReturnsBadRequestOnSqlException()
        {
            // Arrange
            var errorMessage = "SQL connection failed";
            var sqlException = new InvalidSqlException(errorMessage);

            _groupSizeServiceMock.Setup(service =>
                service.View_All_GroupSizes())
                .ThrowsAsync(sqlException);

            _controller = new GroupSizesController(_groupSizeServiceMock.Object);

            // Act
            var result = await _controller.View_All_GroupSizes();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ViewGroupSize>>));

            var actionResult = result as ActionResult<List<ViewGroupSize>>;

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
