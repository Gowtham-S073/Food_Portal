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
    public class StdProductServiceTest
    {
        private Mock<ICrud<StdProduct, IdDTO>> _stdProductRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private Fixture _fixture;
        private StdProductService _service;

        public StdProductServiceTest()
        {
            _fixture = new Fixture();
            _stdProductRepoMock = new Mock<ICrud<StdProduct, IdDTO>>();
            _mapperMock = new Mock<IMapper>();
            _service = new StdProductService(_stdProductRepoMock.Object, _mapperMock.Object);
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
        [TestMethod]
        public async Task View_All_StdProducts_ReturnsList()
        {
            // Arrange
            var stdProducts = _fixture.CreateMany<StdProduct>(3).ToList();

            var mappedResult = _fixture.CreateMany<ViewStdProduct>(3).ToList();

            _stdProductRepoMock.Setup(repo =>
                repo.GetAll())
                .ReturnsAsync(stdProducts);

            _mapperMock.Setup(mapper => mapper.Map<List<ViewStdProduct>>(It.IsAny<IEnumerable<StdProduct>>()))
                .Returns(mappedResult);

            // Act
            var result = await _service.View_All_StdProducts();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<ViewStdProduct>));
            Assert.AreEqual(mappedResult.Count, result.Count);
        }
        [TestMethod]
        public async Task View_by_category_StdProducts_ReturnsList()
        {
            // Arrange
            var categoryId = 1; // Set your category ID here
            var stdProducts = _fixture.CreateMany<StdProduct>(5).ToList();
            stdProducts.ForEach(p => p.CategoriesId = categoryId); // Assign the specified category ID

            var mappedResult = _fixture.CreateMany<ViewStdProduct>(3).ToList();

            _stdProductRepoMock.Setup(repo =>
                repo.GetAll())
                .ReturnsAsync(stdProducts);

            _mapperMock.Setup(mapper => mapper.Map<List<ViewStdProduct>>(It.IsAny<IEnumerable<StdProduct>>()))
                .Returns(mappedResult);

            // Act
            var result = await _service.View_by_category_StdProducts(categoryId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<ViewStdProduct>));
            Assert.AreEqual(mappedResult.Count, result.Count);
        }

    }
}
