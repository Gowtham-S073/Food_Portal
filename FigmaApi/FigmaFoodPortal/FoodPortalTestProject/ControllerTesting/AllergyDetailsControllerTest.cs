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
    public class AllergyDetailsControllerTest
    {
        private Mock<IAllergyDetailService> _allergyDetailServiceMock;
        private Fixture _fixture;
        private AllergyDetailsController _controller;

        public AllergyDetailsControllerTest()
        {
            _fixture = new Fixture();
            _allergyDetailServiceMock = new Mock<IAllergyDetailService>();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
             .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TestMethod]
        public async Task Add_AllergyDetail_ReturnsCreated()
        {
            // Arrange
            var allergyDetails = _fixture.CreateMany<RequestAllergyDetail>(3).ToList();

            _allergyDetailServiceMock.Setup(service =>
                service.Add_AllergyDetail(It.IsAny<List<RequestAllergyDetail>>()))
                .ReturnsAsync(allergyDetails);

            _controller = new AllergyDetailsController(_allergyDetailServiceMock.Object);

            // Act
            var result = await _controller.Add_AllergyDetail(allergyDetails);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<RequestAllergyDetail>>));

            var actionResult = result as ActionResult<List<RequestAllergyDetail>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedResult));

            var createdResult = actionResult.Result as CreatedResult;

            Assert.AreEqual(StatusCodes.Status201Created, createdResult.StatusCode);
            Assert.IsInstanceOfType(createdResult.Value, typeof(List<RequestAllergyDetail>));
        }

        [TestMethod]
        public async Task Add_AllergyDetail_ReturnsBadRequestOnNullException()
        {
            // Arrange
            var errorMessage = "Null Exception happened";
            var nullexception = new NullException(errorMessage);

            _allergyDetailServiceMock.Setup(service =>
                service.Add_AllergyDetail(It.IsAny<List<RequestAllergyDetail>>()))
                .ThrowsAsync(nullexception);

            _controller = new AllergyDetailsController(_allergyDetailServiceMock.Object);

            // Act
            var result = await _controller.Add_AllergyDetail(new List<RequestAllergyDetail>());

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<RequestAllergyDetail>>));

            var actionResult = result as ActionResult<List<RequestAllergyDetail>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundObjectResult));

            var notFoundResult = actionResult.Result as NotFoundObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
            Assert.IsInstanceOfType(notFoundResult.Value, typeof(Error));

            var error = notFoundResult.Value as Error;
            Assert.AreEqual(404, error.ID);
            Assert.AreEqual(errorMessage, error.Message);
        }

        [TestMethod]
        public async Task Add_AllergyDetail_ReturnsBadRequestOnDuplicateRecordException()
        {
            // Arrange
            var errorMessage = "Duplicate Record Exception happened";
            var duplicate = new DuplicateRecordException(errorMessage);

            _allergyDetailServiceMock.Setup(service =>
                service.Add_AllergyDetail(It.IsAny<List<RequestAllergyDetail>>()))
                .ThrowsAsync(duplicate);

            _controller = new AllergyDetailsController(_allergyDetailServiceMock.Object);

            // Act
            var result = await _controller.Add_AllergyDetail(new List<RequestAllergyDetail>());

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<RequestAllergyDetail>>));

            var actionResult = result as ActionResult<List<RequestAllergyDetail>>;

            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));

            var badRequestResult = actionResult.Result as BadRequestObjectResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.IsInstanceOfType(badRequestResult.Value, typeof(Error));

            var error = badRequestResult.Value as Error;
            Assert.AreEqual(400, error.ID);
            Assert.AreEqual(errorMessage, error.Message);
        }

        [TestMethod]
        public async Task Add_AllergyDetail_ReturnsBadRequestOnSqlException()
        {
            // Arrange
            var errorMessage = "SQL connection failed";
            var sqlException = new InvalidSqlException(errorMessage);

            _allergyDetailServiceMock.Setup(service =>
                service.Add_AllergyDetail(It.IsAny<List<RequestAllergyDetail>>()))
                .ThrowsAsync(sqlException);

            _controller = new AllergyDetailsController(_allergyDetailServiceMock.Object);

            // Act
            var result = await _controller.Add_AllergyDetail(new List<RequestAllergyDetail>());

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<RequestAllergyDetail>>));

            var actionResult = result as ActionResult<List<RequestAllergyDetail>>;

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
