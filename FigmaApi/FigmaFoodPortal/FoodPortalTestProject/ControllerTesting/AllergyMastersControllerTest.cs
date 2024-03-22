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
    public class AllergyMastersControllerTest
    {
        private Mock<IAllergyMasterService> _allergyMasterServiceMock;
        private Fixture _fixture;
        private AllergyMastersController _controller;

        public AllergyMastersControllerTest()
        {
            _fixture = new Fixture();
            _allergyMasterServiceMock = new Mock<IAllergyMasterService>();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
             .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TestMethod]
        public async Task View_All_AllergyMasters_ReturnsOk()
        {
            // Arrange
            var allergyMasters = _fixture.CreateMany<ViewAllergyMaster>(3).ToList();

            _allergyMasterServiceMock.Setup(service =>
                service.View_All_AllergyMasters())
                .ReturnsAsync(allergyMasters);

            _controller = new AllergyMastersController(_allergyMasterServiceMock.Object);

            // Act
            var result = await _controller.View_All_AllergyMasters();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ViewAllergyMaster>>));

            var actionResult = result as ActionResult<List<ViewAllergyMaster>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));

            var okResult = actionResult.Result as OkObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.IsInstanceOfType(okResult.Value, typeof(List<ViewAllergyMaster>));
        }

        [TestMethod]
        public async Task View_All_AllergyMasters_ReturnsBadRequestOnNullException()
        {
            // Arrange
            var errorMessage = "Null exception happened";
            var nullException = new NullException(errorMessage);

            _allergyMasterServiceMock.Setup(service =>
                service.View_All_AllergyMasters())
                .ThrowsAsync(nullException);

            _controller = new AllergyMastersController(_allergyMasterServiceMock.Object);

            // Act
            var result = await _controller.View_All_AllergyMasters();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ViewAllergyMaster>>));

            var actionResult = result as ActionResult<List<ViewAllergyMaster>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundObjectResult));

            var notFoundResult = actionResult.Result as NotFoundObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
            Assert.IsInstanceOfType(notFoundResult.Value, typeof(Error));

            var error = notFoundResult.Value as Error;
            Assert.AreEqual(404, error.ID);
            Assert.AreEqual(errorMessage, error.Message);
        }

        [TestMethod]
        public async Task View_All_AllergyMasters_ReturnsBadRequestOnSqlException()
        {
            // Arrange
            var errorMessage = "SQL connection failed";
            var sqlException = new InvalidSqlException(errorMessage);

            _allergyMasterServiceMock.Setup(service =>
                service.View_All_AllergyMasters())
                .ThrowsAsync(sqlException);

            _controller = new AllergyMastersController(_allergyMasterServiceMock.Object);

            // Act
            var result = await _controller.View_All_AllergyMasters();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ViewAllergyMaster>>));

            var actionResult = result as ActionResult<List<ViewAllergyMaster>>;

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
