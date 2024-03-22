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
    public class TimeSlotServiceTest
    {
        private Mock<ICrud<TimeSlot, IdDTO>> _timeSlotRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private Fixture _fixture;
        private TimeSlotService _service;

        public TimeSlotServiceTest()
        {
            _fixture = new Fixture();
            _timeSlotRepoMock = new Mock<ICrud<TimeSlot, IdDTO>>();
            _mapperMock = new Mock<IMapper>();
            _service = new TimeSlotService(_timeSlotRepoMock.Object, _mapperMock.Object);
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
        [TestMethod]
        public async Task View_All_TimeSlots_ReturnsList()
        {
            // Arrange
            var timeSlots = _fixture.CreateMany<TimeSlot>(3).ToList();

            var mappedResult = _fixture.CreateMany<ViewTimeSlot>(3).ToList();

            _timeSlotRepoMock.Setup(repo =>
                repo.GetAll())
                .ReturnsAsync(timeSlots);

            _mapperMock.Setup(mapper => mapper.Map<List<ViewTimeSlot>>(It.IsAny<IEnumerable<TimeSlot>>()))
                .Returns(mappedResult);

            // Act
            var result = await _service.View_All_TimeSlots();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<ViewTimeSlot>));
            Assert.AreEqual(mappedResult.Count, result.Count);
        }

    }
}
