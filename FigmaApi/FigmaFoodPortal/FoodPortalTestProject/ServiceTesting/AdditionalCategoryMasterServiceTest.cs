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
#nullable disable

namespace FoodPortalTestProject.ServiceTesting
{
    [TestClass]
    public class AdditionalCategoryMasterServiceTest
    {
        private Mock<ICrud<AdditionalCategoryMaster, IdDTO>> _additionalCategoryMasterRepoMock;
        private readonly Mock<IMapper> _mapperMock;

        private Fixture _fixture;
        private AdditionalCategoryMasterService _service;

        public AdditionalCategoryMasterServiceTest()
        {
            _fixture = new Fixture();
            _additionalCategoryMasterRepoMock = new Mock<ICrud<AdditionalCategoryMaster, IdDTO>>();
            _mapperMock = new Mock<IMapper>();
            _service = new AdditionalCategoryMasterService(_additionalCategoryMasterRepoMock.Object, _mapperMock.Object);
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TestMethod]
        public async Task View_All_AdditionalCategoryMasters_ReturnsList()
        {
            // Arrange
            var additionalCategoryMasters = _fixture.CreateMany<AdditionalCategoryMaster>(3).ToList();

            var mappedResult = _fixture.CreateMany<ViewAdditionalCategoryMaster>(3).ToList();

            _additionalCategoryMasterRepoMock.Setup(repo =>
                repo.GetAll())
                .ReturnsAsync(additionalCategoryMasters);

            _mapperMock.Setup(mapper => mapper.Map<List<ViewAdditionalCategoryMaster>>(It.IsAny<List<AdditionalCategoryMaster>>()))
                .Returns(mappedResult);

            // Act
            var result = await _service.View_All_AdditionalCategoryMasters();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<ViewAdditionalCategoryMaster>));
            Assert.AreEqual(mappedResult.Count, result.Count);
        }

    }
}
