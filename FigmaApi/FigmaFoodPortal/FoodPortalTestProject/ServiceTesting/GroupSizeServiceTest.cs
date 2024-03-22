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
    public class GroupSizeServiceTest
    {
        private Mock<ICrud<GroupSize, IdDTO>> _groupSizeRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private Fixture _fixture;
        private GroupSizeService _service;

        public GroupSizeServiceTest()
        {
            _fixture = new Fixture();
            _groupSizeRepoMock = new Mock<ICrud<GroupSize, IdDTO>>();
            _mapperMock = new Mock<IMapper>();
            _service = new GroupSizeService(_groupSizeRepoMock.Object, _mapperMock.Object);


            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TestMethod]
        public async Task View_All_GroupSizes_ReturnsList()
        {
            // Arrange
            var groupSizes = _fixture.CreateMany<GroupSize>(3).ToList();

            var mappedResult = _fixture.CreateMany<ViewGroupSize>(3).ToList();

            _groupSizeRepoMock.Setup(repo =>
                repo.GetAll())
                .ReturnsAsync(groupSizes);

            _mapperMock.Setup(mapper => mapper.Map<List<ViewGroupSize>>(It.IsAny<IEnumerable<GroupSize>>()))
                .Returns(mappedResult);

            // Act
            var result = await _service.View_All_GroupSizes();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<ViewGroupSize>));
            Assert.AreEqual(mappedResult.Count, result.Count);
        }
    }
}
