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
    public class AddOnsMastersControllerTest
    {
        private Mock<IAddOnsMasterService> _addOnsMasterServiceMock;
        private Fixture _fixture;
        private AddOnsMastersController _controller;

        public AddOnsMastersControllerTest()
        {
            _fixture = new Fixture();
            _addOnsMasterServiceMock = new Mock<IAddOnsMasterService>();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
              .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }


        [TestMethod]
        public async Task View_All_AddOnsMasters_ReturnsOk()
        {
            // Arrange
            var addOnsMasters = _fixture.CreateMany<ViewAddOnsMaster>(3).ToList();

            _addOnsMasterServiceMock.Setup(service =>
                service.View_All_AddOnsMasters())
                .ReturnsAsync(addOnsMasters);

            _controller = new AddOnsMastersController(_addOnsMasterServiceMock.Object);

            // Act
            var result = await _controller.View_All_AddOnsMasters();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ViewAddOnsMaster>>));

            var actionResult = result as ActionResult<List<ViewAddOnsMaster>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));

            var okResult = actionResult.Result as OkObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.IsInstanceOfType(okResult.Value, typeof(List<ViewAddOnsMaster>));
        }
        [TestMethod]
        public async Task View_All_AddOnsMasters_ReturnsBadRequestOnNullException()
        {
            // Arrange
            var errorMessage = "Null Exception happened";
            var nullexception = new NullException(errorMessage);

            _addOnsMasterServiceMock.Setup(service =>
                service.View_All_AddOnsMasters())
                .ThrowsAsync(nullexception);

            _controller = new AddOnsMastersController(_addOnsMasterServiceMock.Object);

            // Act
            var result = await _controller.View_All_AddOnsMasters();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ViewAddOnsMaster>>));

            var actionResult = result as ActionResult<List<ViewAddOnsMaster>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundObjectResult));

            var notFoundResult = actionResult.Result as NotFoundObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
            Assert.IsInstanceOfType(notFoundResult.Value, typeof(Error));

            var error = notFoundResult.Value as Error;
            Assert.AreEqual(404, error.ID);
            Assert.AreEqual(errorMessage, error.Message);
        }


        [TestMethod]
        public async Task View_All_AddOnsMasters_ReturnsBadRequestOnSqlException()
        {
            // Arrange
            var errorMessage = "SQL connection failed";
            var sqlException = new InvalidSqlException(errorMessage);

            _addOnsMasterServiceMock.Setup(service =>
                service.View_All_AddOnsMasters())
                .ThrowsAsync(sqlException);

            _controller = new AddOnsMastersController(_addOnsMasterServiceMock.Object);

            // Act
            var result = await _controller.View_All_AddOnsMasters();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ViewAddOnsMaster>>));

            var actionResult = result as ActionResult<List<ViewAddOnsMaster>>;

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
