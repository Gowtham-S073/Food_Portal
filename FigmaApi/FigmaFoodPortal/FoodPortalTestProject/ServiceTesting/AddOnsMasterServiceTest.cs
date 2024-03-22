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
    public class AddOnsMasterServiceTest
    {
        private Mock<ICrud<AddOnsMaster, IdDTO>> _addOnsMasterRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private Fixture _fixture;
        private AddOnsMasterService _service;

        public AddOnsMasterServiceTest()
        {
            _fixture = new Fixture();
            _addOnsMasterRepoMock = new Mock<ICrud<AddOnsMaster, IdDTO>>();
            _mapperMock = new Mock<IMapper>();

            _service = new AddOnsMasterService(_addOnsMasterRepoMock.Object, _mapperMock.Object);


            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TestMethod]
        public async Task View_All_AddOnsMasters_ReturnsList()
        {
            // Arrange
            var addOnsMasters = _fixture.CreateMany<AddOnsMaster>(3).ToList();

            var mappedResult = _fixture.CreateMany<ViewAddOnsMaster>(3).ToList();

            _addOnsMasterRepoMock.Setup(repo =>
                repo.GetAll())
                .ReturnsAsync(addOnsMasters);

            _mapperMock.Setup(mapper => mapper.Map<List<ViewAddOnsMaster>>(It.IsAny<IEnumerable<AddOnsMaster>>()))
                .Returns(mappedResult);

            // Act
            var result = await _service.View_All_AddOnsMasters();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<ViewAddOnsMaster>));
            Assert.AreEqual(mappedResult.Count, result.Count);
        }
    }
}
