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
    public class StdFoodCategoryMasterServiceTest
    {
        private Mock<ICrud<StdFoodCategoryMaster, IdDTO>> _stdFoodCategoryMasterRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private Fixture _fixture;
        private StdFoodCategoryMasterService _service;

        public StdFoodCategoryMasterServiceTest()
        {
            _fixture = new Fixture();
            _stdFoodCategoryMasterRepoMock = new Mock<ICrud<StdFoodCategoryMaster, IdDTO>>();
            _mapperMock = new Mock<IMapper>();
            _service = new StdFoodCategoryMasterService(_stdFoodCategoryMasterRepoMock.Object, _mapperMock.Object);

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TestMethod]
        public async Task View_All_StdFoodCategoryMasters_ReturnsList()
        {
            // Arrange
            var stdFoodCategoryMasters = _fixture.CreateMany<StdFoodCategoryMaster>(3).ToList();

            var mappedResult = _fixture.CreateMany<ViewStdFoodCategoryMaster>(3).ToList();

            _stdFoodCategoryMasterRepoMock.Setup(repo =>
                repo.GetAll())
                .ReturnsAsync(stdFoodCategoryMasters);

            _mapperMock.Setup(mapper => mapper.Map<List<ViewStdFoodCategoryMaster>>(It.IsAny<IEnumerable<StdFoodCategoryMaster>>()))
                .Returns(mappedResult);

            // Act
            var result = await _service.View_All_StdFoodCategoryMasters();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<ViewStdFoodCategoryMaster>));
            Assert.AreEqual(mappedResult.Count, result.Count);
        }

    }
}
