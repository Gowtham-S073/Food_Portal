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
    public class DrinksMasterServiceTest
    {
        private Mock<ICrud<DrinksMaster, IdDTO>> _drinksMasterRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private Fixture _fixture;
        private DrinksMasterService _service;

        public DrinksMasterServiceTest()
        {
            _fixture = new Fixture();
            _drinksMasterRepoMock = new Mock<ICrud<DrinksMaster, IdDTO>>();
            _mapperMock = new Mock<IMapper>();
            _service = new DrinksMasterService(_drinksMasterRepoMock.Object, _mapperMock.Object);


            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TestMethod]
        public async Task View_All_DrinksMasters_ReturnsList()
        {
            // Arrange
            var drinksMasters = _fixture.CreateMany<DrinksMaster>(3).ToList();

            var mappedResult = _fixture.CreateMany<ViewDrinksMaster>(3).ToList();

            _drinksMasterRepoMock.Setup(repo =>
                repo.GetAll())
                .ReturnsAsync(drinksMasters);

            _mapperMock.Setup(mapper => mapper.Map<List<ViewDrinksMaster>>(It.IsAny<IEnumerable<DrinksMaster>>()))
                .Returns(mappedResult);

            // Act
            var result = await _service.View_All_DrinksMasters();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<ViewDrinksMaster>));
            Assert.AreEqual(mappedResult.Count, result.Count);
        }
    }
}
