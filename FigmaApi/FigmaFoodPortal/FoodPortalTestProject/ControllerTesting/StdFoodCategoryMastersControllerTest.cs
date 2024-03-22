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
    public class StdFoodCategoryMastersControllerTest
    {
        private Mock<IStdFoodCategoryMasterService> _stdFoodCategoryMasterServiceMock;
        private Fixture _fixture;
        private StdFoodCategoryMastersController _controller;

        public StdFoodCategoryMastersControllerTest()
        {
            _fixture = new Fixture();
            _stdFoodCategoryMasterServiceMock = new Mock<IStdFoodCategoryMasterService>();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
             .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TestMethod]
        public async Task View_All_StdFoodCategoryMasters_ReturnsOk()
        {
            // Arrange
            var stdFoodCategoryMasters = _fixture.CreateMany<ViewStdFoodCategoryMaster>(2).ToList();

            _stdFoodCategoryMasterServiceMock.Setup(service =>
                service.View_All_StdFoodCategoryMasters())
                .ReturnsAsync(stdFoodCategoryMasters);

            _controller = new StdFoodCategoryMastersController(_stdFoodCategoryMasterServiceMock.Object);

            // Act
            var result = await _controller.View_All_StdFoodCategoryMasters();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ViewStdFoodCategoryMaster>>));

            var actionResult = result as ActionResult<List<ViewStdFoodCategoryMaster>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));

            var okResult = actionResult.Result as OkObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.IsInstanceOfType(okResult.Value, typeof(List<ViewStdFoodCategoryMaster>));

            var returnedStdFoodCategoryMasters = okResult.Value as List<ViewStdFoodCategoryMaster>;
            Assert.AreEqual(stdFoodCategoryMasters.Count, returnedStdFoodCategoryMasters.Count);
        }
        [TestMethod]
        public async Task View_All_StdFoodCategoryMasters_ReturnsBadRequestOnNullException()
        {
            // Arrange
            _stdFoodCategoryMasterServiceMock.Setup(service =>
                service.View_All_StdFoodCategoryMasters())
                .ThrowsAsync(new NullException("null Exception"));

            _controller = new StdFoodCategoryMastersController(_stdFoodCategoryMasterServiceMock.Object);

            // Act
            var result = await _controller.View_All_StdFoodCategoryMasters();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ViewStdFoodCategoryMaster>>));

            var actionResult = result as ActionResult<List<ViewStdFoodCategoryMaster>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundObjectResult));

            var notFoundResult = actionResult.Result as NotFoundObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
            Assert.IsInstanceOfType(notFoundResult.Value, typeof(Error));

            var error = notFoundResult.Value as Error;
            Assert.AreEqual(404, error.ID);
            Assert.AreEqual("null Exception", error.Message);
        }



        [TestMethod]
        public async Task View_All_StdFoodCategoryMasters_ReturnsBadRequestOnInvalidSqlException()
        {
            // Arrange
            _stdFoodCategoryMasterServiceMock.Setup(service =>
                service.View_All_StdFoodCategoryMasters())
                .ThrowsAsync(new InvalidSqlException("Invalid SQL Exception"));

            _controller = new StdFoodCategoryMastersController(_stdFoodCategoryMasterServiceMock.Object);

            // Act
            var result = await _controller.View_All_StdFoodCategoryMasters();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ViewStdFoodCategoryMaster>>));

            var actionResult = result as ActionResult<List<ViewStdFoodCategoryMaster>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));

            var badRequestResult = actionResult.Result as BadRequestObjectResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.IsInstanceOfType(badRequestResult.Value, typeof(Error));

            var error = badRequestResult.Value as Error;
            Assert.AreEqual(500, error.ID);
            Assert.AreEqual("Invalid SQL Exception", error.Message);
        }
    }
}
