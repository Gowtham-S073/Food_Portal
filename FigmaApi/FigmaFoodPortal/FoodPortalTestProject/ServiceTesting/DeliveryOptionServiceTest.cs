using AutoFixture;
using AutoMapper;
using FoodPortal.Interfaces;
using FoodPortal.Models;
using FoodPortal.Models.DTO;
using FoodPortal.Services;
using FoodPortal.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPortalTestProject.ServiceTesting
{
    [TestClass]
    public class DeliveryOptionServiceTest
    {
        private Mock<ICrud<DeliveryOption, IdDTO>> _deliveryOptionRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private Fixture _fixture;
        private DeliveryOptionService _service;

        public DeliveryOptionServiceTest()
        {
            _fixture = new Fixture();
            _deliveryOptionRepoMock = new Mock<ICrud<DeliveryOption, IdDTO>>();
            _mapperMock = new Mock<IMapper>();
            _service = new DeliveryOptionService(_deliveryOptionRepoMock.Object, _mapperMock.Object);


            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TestMethod]
        public async Task View_All_DeliveryOptions_ReturnsList()
        {
            // Arrange
            var deliveryOptions = _fixture.CreateMany<DeliveryOption>(3).ToList();

            var mappedResult = _fixture.CreateMany<ViewDeliveryOption>(3).ToList();

            _deliveryOptionRepoMock.Setup(repo =>
                repo.GetAll())
                .ReturnsAsync(deliveryOptions);

            _mapperMock.Setup(mapper => mapper.Map<List<ViewDeliveryOption>>(It.IsAny<IEnumerable<DeliveryOption>>()))
                .Returns(mappedResult);

            // Act
            var result = await _service.View_All_DeliveryOptions();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<ViewDeliveryOption>));
            Assert.AreEqual(mappedResult.Count, result.Count);
        }
    }
}
