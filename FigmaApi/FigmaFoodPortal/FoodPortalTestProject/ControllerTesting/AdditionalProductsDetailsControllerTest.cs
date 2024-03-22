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
    public class AdditionalProductsDetailsControllerTest
    {
        private Mock<IAdditionalProductsDetailService> _additionalProductsDetailServiceMock;
        private Fixture _fixture;
        private AdditionalProductsDetailsController _controller;

        public AdditionalProductsDetailsControllerTest()
        {
            _fixture = new Fixture();
            _additionalProductsDetailServiceMock = new Mock<IAdditionalProductsDetailService>();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        }

        [TestMethod]
        public async Task Add_AdditionalProductsDetail_ReturnsCreated()
        {
            // Arrange
            var additionalProductsDetailList = _fixture.CreateMany<RequestAdditionalProductsDetail>(3).ToList();

            _additionalProductsDetailServiceMock.Setup(service =>
                service.Add_AdditionalProductsDetail(It.IsAny<List<RequestAdditionalProductsDetail>>()))
                .ReturnsAsync(additionalProductsDetailList);

            _controller = new AdditionalProductsDetailsController(_additionalProductsDetailServiceMock.Object);

            // Act
            var result = await _controller.Add_AdditionalProductsDetail(additionalProductsDetailList);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<RequestAdditionalProductsDetail>>));

            var actionResult = result as ActionResult<List<RequestAdditionalProductsDetail>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedResult));

            var createdResult = actionResult.Result as CreatedResult;

            Assert.AreEqual(StatusCodes.Status201Created, createdResult.StatusCode);
            Assert.IsInstanceOfType(createdResult.Value, typeof(List<RequestAdditionalProductsDetail>));

            var createdAdditionalProductsDetail = createdResult.Value as List<RequestAdditionalProductsDetail>;
            Assert.AreEqual(additionalProductsDetailList.Count, createdAdditionalProductsDetail.Count);

        }

        [TestMethod]
        public async Task Add_AdditionalProductsDetail_ReturnsBadRequestOnNullException()
        {
            // Arrange
            var additionalProductsDetailList = _fixture.CreateMany<RequestAdditionalProductsDetail>(3).ToList();
            var errorMessage = "Null exception happened";
            var nullException = new NullException(errorMessage);

            _additionalProductsDetailServiceMock.Setup(service =>
                service.Add_AdditionalProductsDetail(It.IsAny<List<RequestAdditionalProductsDetail>>()))
                .ThrowsAsync(nullException);

            _controller = new AdditionalProductsDetailsController(_additionalProductsDetailServiceMock.Object);

            // Act
            var result = await _controller.Add_AdditionalProductsDetail(additionalProductsDetailList);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<RequestAdditionalProductsDetail>>));

            var actionResult = result as ActionResult<List<RequestAdditionalProductsDetail>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundObjectResult));

            var notFoundResult = actionResult.Result as NotFoundObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
            Assert.IsInstanceOfType(notFoundResult.Value, typeof(Error));

            var error = notFoundResult.Value as Error;
            Assert.AreEqual(404, error.ID);
            Assert.AreEqual(errorMessage, error.Message);
        }
        [TestMethod]
        public async Task Add_AdditionalProductsDetail_ReturnsBadRequestOnDuplicateRecordException()
        {
            // Arrange
            var additionalProductsDetailList = _fixture.CreateMany<RequestAdditionalProductsDetail>(3).ToList();
            var errorMessage = "Duplicate Record Exception happened";
            var duplicate = new DuplicateRecordException(errorMessage);

            _additionalProductsDetailServiceMock.Setup(service =>
                service.Add_AdditionalProductsDetail(It.IsAny<List<RequestAdditionalProductsDetail>>()))
                .ThrowsAsync(duplicate);

            _controller = new AdditionalProductsDetailsController(_additionalProductsDetailServiceMock.Object);

            // Act
            var result = await _controller.Add_AdditionalProductsDetail(additionalProductsDetailList);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<RequestAdditionalProductsDetail>>));

            var actionResult = result as ActionResult<List<RequestAdditionalProductsDetail>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));

            var badRequestResult = actionResult.Result as BadRequestObjectResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.IsInstanceOfType(badRequestResult.Value, typeof(Error));

            var error = badRequestResult.Value as Error;
            Assert.AreEqual(400, error.ID);
            Assert.AreEqual(errorMessage, error.Message);
        }


        [TestMethod]
        public async Task Add_AdditionalProductsDetail_ReturnsBadRequestOnSqlException()
        {
            // Arrange
            var additionalProductsDetailList = _fixture.CreateMany<RequestAdditionalProductsDetail>(3).ToList();
            var errorMessage = "SQL connection failed";
            var sqlException = new InvalidSqlException(errorMessage);

            _additionalProductsDetailServiceMock.Setup(service =>
                service.Add_AdditionalProductsDetail(It.IsAny<List<RequestAdditionalProductsDetail>>()))
                .ThrowsAsync(sqlException);

            _controller = new AdditionalProductsDetailsController(_additionalProductsDetailServiceMock.Object);

            // Act
            var result = await _controller.Add_AdditionalProductsDetail(additionalProductsDetailList);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<RequestAdditionalProductsDetail>>));

            var actionResult = result as ActionResult<List<RequestAdditionalProductsDetail>>;

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
