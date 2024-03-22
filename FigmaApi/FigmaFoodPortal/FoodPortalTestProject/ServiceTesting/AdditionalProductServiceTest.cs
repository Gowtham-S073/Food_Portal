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
    public class AdditionalProductServiceTest
    {
        private Mock<ICrud<AdditionalProduct, IdDTO>> _additionalProductRepoMock;
        private Fixture _fixture;
        private AdditionalProductService _service;
        private Mock<ICrud<AdditionalCategoryMaster, IdDTO>> _additionalCategoryMasterRepoMock;
        private readonly Mock<IMapper> _mapperMock;



        public AdditionalProductServiceTest()
        {
            _fixture = new Fixture();
            _additionalProductRepoMock = new Mock<ICrud<AdditionalProduct, IdDTO>>();
            _additionalCategoryMasterRepoMock = new Mock<ICrud<AdditionalCategoryMaster, IdDTO>>();
            _mapperMock = new Mock<IMapper>();
            _service = new AdditionalProductService(_additionalProductRepoMock.Object,
                                                    _additionalCategoryMasterRepoMock.Object, _mapperMock.Object);


            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
        [TestMethod]
        public async Task View_by_category_AdditionalProducts_ReturnsList()
        {
            // Arrange
            int categoryId = 1;
            var additionalProducts = _fixture.CreateMany<AdditionalProduct>(3).ToList();
            additionalProducts.ForEach(ap => ap.AdditionalCategoryId = categoryId);

            var mappedResult = _fixture.CreateMany<ViewAdditionalProduct>(3).ToList();

            _additionalProductRepoMock.Setup(repo => repo.GetAll()).ReturnsAsync(additionalProducts);
            _mapperMock.Setup(mapper => mapper.Map<List<ViewAdditionalProduct>>(It.IsAny<IEnumerable<AdditionalProduct>>()))
                .Returns(mappedResult);

            // Act
            var result = await _service.View_by_category_AdditionalProducts(categoryId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<ViewAdditionalProduct>));
            Assert.AreEqual(mappedResult.Count, result.Count);
        }
        [TestMethod]
        public async Task View_by_foodtype_AdditionalProducts_ReturnsList()
        {
            // Arrange
            bool isVeg = true;
            int categoryId = 1;
            var additionalProducts = _fixture.CreateMany<AdditionalProduct>(3).ToList();
            additionalProducts.ForEach(ap => ap.AdditionalCategoryId = categoryId);
            additionalProducts[0].IsVeg = isVeg;

            var mappedResult = _fixture.CreateMany<ViewAdditionalProduct>(3).ToList();

            _additionalProductRepoMock.Setup(repo => repo.GetAll()).ReturnsAsync(additionalProducts);
            _mapperMock.Setup(mapper => mapper.Map<List<ViewAdditionalProduct>>(It.IsAny<IEnumerable<AdditionalProduct>>()))
                .Returns(mappedResult);

            // Act
            var result = await _service.View_by_foodtype_AdditionalProducts(isVeg, categoryId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<ViewAdditionalProduct>));
            Assert.AreEqual(mappedResult.Count, result.Count);

        }
    }
}
