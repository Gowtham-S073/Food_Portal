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
    public class AddOnsDetailsControllerTest
    {
        private Mock<IAddOnsDetailService> _addOnsDetailServiceMock;
        private Fixture _fixture;
        private AddOnsDetailsController _controller;

        public AddOnsDetailsControllerTest()
        {
            _fixture = new Fixture();
            _addOnsDetailServiceMock = new Mock<IAddOnsDetailService>();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
             .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        }

        [TestMethod]
        public async Task Add_AddOnsDetail_ReturnsCreated()
        {
            // Arrange
            var addOnsDetailList = _fixture.CreateMany<RequestAddOnsDetail>(3).ToList();

            _addOnsDetailServiceMock.Setup(service =>
                service.Add_AddOnsDetail(It.IsAny<List<RequestAddOnsDetail>>()))
                .ReturnsAsync(addOnsDetailList);

            _controller = new AddOnsDetailsController(_addOnsDetailServiceMock.Object);

            // Act
            var result = await _controller.Add_AddOnsDetail(addOnsDetailList);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<RequestAddOnsDetail>>));

            var actionResult = result as ActionResult<List<RequestAddOnsDetail>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedResult));

            var createdResult = actionResult.Result as CreatedResult;

            Assert.AreEqual(StatusCodes.Status201Created, createdResult.StatusCode);
            Assert.IsInstanceOfType(createdResult.Value, typeof(List<RequestAddOnsDetail>));

            var createdAddOnsDetail = createdResult.Value as List<RequestAddOnsDetail>;
            Assert.AreEqual(addOnsDetailList.Count, createdAddOnsDetail.Count);

        }
        [TestMethod]
        public async Task Add_AddOnsDetail_ReturnsBadRequestOnNullException()
        {
            // Arrange
            var addOnsDetailList = _fixture.CreateMany<RequestAddOnsDetail>(3).ToList();
            var errorMessage = "Null Exception happened";
            var nullException = new NullException(errorMessage);

            _addOnsDetailServiceMock.Setup(service =>
                service.Add_AddOnsDetail(It.IsAny<List<RequestAddOnsDetail>>()))
                .ThrowsAsync(nullException);

            _controller = new AddOnsDetailsController(_addOnsDetailServiceMock.Object);

            // Act
            var result = await _controller.Add_AddOnsDetail(addOnsDetailList);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<RequestAddOnsDetail>>));

            var actionResult = result as ActionResult<List<RequestAddOnsDetail>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundObjectResult));

            var notFoundResult = actionResult.Result as NotFoundObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
            Assert.IsInstanceOfType(notFoundResult.Value, typeof(Error));

            var error = notFoundResult.Value as Error;
            Assert.AreEqual(404, error.ID);
            Assert.AreEqual(errorMessage, error.Message);
        }
        [TestMethod]
        public async Task Add_AddOnsDetail_ReturnsBadRequestOnDuplicateRecordException()
        {
            // Arrange
            var addOnsDetailList = _fixture.CreateMany<RequestAddOnsDetail>(3).ToList();
            var errorMessage = "Duplicate Record Exception happened";
            var duplicate = new DuplicateRecordException(errorMessage);

            _addOnsDetailServiceMock.Setup(service =>
                service.Add_AddOnsDetail(It.IsAny<List<RequestAddOnsDetail>>()))
                .ThrowsAsync(duplicate);

            _controller = new AddOnsDetailsController(_addOnsDetailServiceMock.Object);

            // Act
            var result = await _controller.Add_AddOnsDetail(addOnsDetailList);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<RequestAddOnsDetail>>));

            var actionResult = result as ActionResult<List<RequestAddOnsDetail>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));

            var badRequestResult = actionResult.Result as BadRequestObjectResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.IsInstanceOfType(badRequestResult.Value, typeof(Error));

            var error = badRequestResult.Value as Error;
            Assert.AreEqual(400, error.ID);
            Assert.AreEqual(errorMessage, error.Message);
        }
        [TestMethod]
        public async Task Add_AddOnsDetail_ReturnsBadRequestOnSqlException()
        {
            // Arrange
            var addOnsDetailList = _fixture.CreateMany<RequestAddOnsDetail>(3).ToList();
            var errorMessage = "SQL connection failed";
            var sqlException = new InvalidSqlException(errorMessage);

            _addOnsDetailServiceMock.Setup(service =>
                service.Add_AddOnsDetail(It.IsAny<List<RequestAddOnsDetail>>()))
                .ThrowsAsync(sqlException);

            _controller = new AddOnsDetailsController(_addOnsDetailServiceMock.Object);

            // Act
            var result = await _controller.Add_AddOnsDetail(addOnsDetailList);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<RequestAddOnsDetail>>));

            var actionResult = result as ActionResult<List<RequestAddOnsDetail>>;

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
