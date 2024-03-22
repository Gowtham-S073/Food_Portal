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
    public class AdditionalCategoryMasterControllerTest
    {
        private Mock<IAdditionalCategoryMasterService> _additionalCategoryMasterServiceMock;
        private Fixture _fixture;
        private AdditionalCategoryMastersController _controller;

        public AdditionalCategoryMasterControllerTest()
        {
            _fixture = new Fixture();
            _additionalCategoryMasterServiceMock = new Mock<IAdditionalCategoryMasterService>();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
             .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }



        [TestMethod]
        public async Task View_All_AdditionalCategoryMasters_ReturnsOkWithList()
        {
            // Arrange
            var additionalCategoryMasters = _fixture.CreateMany<ViewAdditionalCategoryMaster>(3).ToList();

            _additionalCategoryMasterServiceMock.Setup(service =>
                service.View_All_AdditionalCategoryMasters())
                .ReturnsAsync(additionalCategoryMasters);

            _controller = new AdditionalCategoryMastersController(_additionalCategoryMasterServiceMock.Object);

            // Act
            var result = await _controller.View_All_AdditionalCategoryMasters();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ViewAdditionalCategoryMaster>>));

            var actionResult = result as ActionResult<List<ViewAdditionalCategoryMaster>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));

            var okResult = actionResult.Result as OkObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.IsInstanceOfType(okResult.Value, typeof(List<ViewAdditionalCategoryMaster>));

            var returnedAdditionalCategoryMasters = okResult.Value as List<ViewAdditionalCategoryMaster>;
            Assert.AreEqual(additionalCategoryMasters.Count, returnedAdditionalCategoryMasters.Count);
        }

        [TestMethod]
        public async Task View_All_AdditionalCategoryMasters_ReturnsBadRequestOnNullException()
        {
            // Arrange
            var errorMessage = "AdditionalCategoryMaster table is empty";
            var nullException = new NullException(errorMessage);

            _additionalCategoryMasterServiceMock.Setup(service =>
                service.View_All_AdditionalCategoryMasters())
                .ThrowsAsync(nullException);

            _controller = new AdditionalCategoryMastersController(_additionalCategoryMasterServiceMock.Object);

            // Act
            var result = await _controller.View_All_AdditionalCategoryMasters();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ViewAdditionalCategoryMaster>>));

            var actionResult = result as ActionResult<List<ViewAdditionalCategoryMaster>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundObjectResult));

            var notFoundResult = actionResult.Result as NotFoundObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
            Assert.IsInstanceOfType(notFoundResult.Value, typeof(Error));

            var error = notFoundResult.Value as Error;
            Assert.AreEqual(404, error.ID);
            Assert.AreEqual(errorMessage, error.Message);
        }



        [TestMethod]
        public async Task View_All_AdditionalCategoryMasters_ReturnsBadRequestOnSqlException()
        {
            // Arrange
            var errorMessage = "SQL connection failed";
            var sqlException = new InvalidSqlException(errorMessage);

            _additionalCategoryMasterServiceMock.Setup(service =>
                service.View_All_AdditionalCategoryMasters())
                .ThrowsAsync(sqlException);

            _controller = new AdditionalCategoryMastersController(_additionalCategoryMasterServiceMock.Object);

            // Act
            var result = await _controller.View_All_AdditionalCategoryMasters();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ViewAdditionalCategoryMaster>>));

            var actionResult = result as ActionResult<List<ViewAdditionalCategoryMaster>>;

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
