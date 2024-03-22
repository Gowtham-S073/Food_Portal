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
    public class PlateSizeServiceTest
    {
        private Mock<ICrud<PlateSize, IdDTO>> _plateSizeRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private Fixture _fixture;
        private PlateSizeService _service;

        public PlateSizeServiceTest()
        {
            _fixture = new Fixture();
            _plateSizeRepoMock = new Mock<ICrud<PlateSize, IdDTO>>();
            _mapperMock = new Mock<IMapper>();
            _service = new PlateSizeService(_plateSizeRepoMock.Object, _mapperMock.Object);
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TestMethod]
        public async Task View_All_PlateSizes_ReturnsList()
        {
            // Arrange
            var plateSizes = _fixture.CreateMany<PlateSize>(3).ToList();

            var mappedResult = _fixture.CreateMany<ViewPlateSize>(3).ToList();

            _plateSizeRepoMock.Setup(repo =>
                repo.GetAll())
                .ReturnsAsync(plateSizes);

            _mapperMock.Setup(mapper => mapper.Map<List<ViewPlateSize>>(It.IsAny<IEnumerable<PlateSize>>()))
                .Returns(mappedResult);

            // Act
            var result = await _service.View_All_PlateSizes();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<ViewPlateSize>));
            Assert.AreEqual(mappedResult.Count, result.Count);
        }

    }
}
