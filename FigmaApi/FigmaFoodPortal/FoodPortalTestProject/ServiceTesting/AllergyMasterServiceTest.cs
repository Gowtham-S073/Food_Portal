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
    public class AllergyMasterServiceTest
    {
        private Mock<ICrud<AllergyMaster, IdDTO>> _allergyMasterRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private Fixture _fixture;
        private AllergyMasterService _service;

        public AllergyMasterServiceTest()
        {
            _fixture = new Fixture();
            _allergyMasterRepoMock = new Mock<ICrud<AllergyMaster, IdDTO>>();
            _mapperMock = new Mock<IMapper>();
            _service = new AllergyMasterService(_allergyMasterRepoMock.Object, _mapperMock.Object);


            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TestMethod]
        public async Task View_All_AllergyMasters_ReturnsList()
        {
            // Arrange
            var allergyMasters = _fixture.CreateMany<AllergyMaster>(3).ToList();

            var mappedResult = _fixture.CreateMany<ViewAllergyMaster>(3).ToList();

            _allergyMasterRepoMock.Setup(repo =>
                repo.GetAll())
                .ReturnsAsync(allergyMasters);

            _mapperMock.Setup(mapper => mapper.Map<List<ViewAllergyMaster>>(It.IsAny<IEnumerable<AllergyMaster>>()))
                .Returns(mappedResult);

            // Act
            var result = await _service.View_All_AllergyMasters();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<ViewAllergyMaster>));
            Assert.AreEqual(mappedResult.Count, result.Count);
        }
    }
}
