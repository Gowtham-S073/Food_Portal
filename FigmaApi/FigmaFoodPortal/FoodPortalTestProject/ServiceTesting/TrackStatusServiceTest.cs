using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using FoodPortal.Exceptions;
using FoodPortal.Interfaces;
using FoodPortal.Models;
using FoodPortal.Models.DTO;
using FoodPortal.RequestModel;
using FoodPortal.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FoodPortalTestProject.ServiceTesting
{
    [TestClass]
    public class TrackStatusServiceTest
    {
        private Mock<ICrud<TrackStatus, IdDTO>> _trackStatusRepoMock;
        private Mock<ICrud<Order, IdDTO>> _orderRepoMock;
        private Mock<ICrud<TimeSlot, IdDTO>> _timeSlotRepoMock;
        private Mock<ICrud<User, UserDTO>> _userRepoMock;
        private Mock<ICrud<DeliveryOption, IdDTO>> _deliveryOptionRepoMock;
        private Mock<ICrud<GroupSize, IdDTO>> _groupSizeRepoMock;
        private Mock<IAdditionalOrderDetailService> _additionalOrderDetailServiceMock;
        private Mock<IPlateCostService> _plateCostServiceMock;
        private Mock<IStdFoodOrderDetailService> _stdFoodOrderDetailServiceMock;
        private Mock<IAllergyDetailService> _allergyDetailServiceMock;
        private Mock<IOrderService> _orderServiceMock;
        private TrackStatusService _service;
        private Fixture _fixture;
        private Mock<IMapper> _mapperMock;

        public TrackStatusServiceTest()
        {
            _fixture = new Fixture();
            _trackStatusRepoMock = new Mock<ICrud<TrackStatus, IdDTO>>();
            _orderRepoMock = new Mock<ICrud<Order, IdDTO>>();
            _timeSlotRepoMock = new Mock<ICrud<TimeSlot, IdDTO>>();
            _userRepoMock = new Mock<ICrud<User, UserDTO>>();
            _deliveryOptionRepoMock = new Mock<ICrud<DeliveryOption, IdDTO>>();
            _groupSizeRepoMock = new Mock<ICrud<GroupSize, IdDTO>>();
            _additionalOrderDetailServiceMock = new Mock<IAdditionalOrderDetailService>();
            _plateCostServiceMock = new Mock<IPlateCostService>();
            _stdFoodOrderDetailServiceMock = new Mock<IStdFoodOrderDetailService>();
            _allergyDetailServiceMock = new Mock<IAllergyDetailService>();
            _orderServiceMock = new Mock<IOrderService>();
            _mapperMock = new Mock<IMapper>();
            _service = new TrackStatusService(
                _trackStatusRepoMock.Object,
                _userRepoMock.Object,
                _orderRepoMock.Object,
                _timeSlotRepoMock.Object,
                _deliveryOptionRepoMock.Object,
                _groupSizeRepoMock.Object,
                _additionalOrderDetailServiceMock.Object,
                _plateCostServiceMock.Object,
                _stdFoodOrderDetailServiceMock.Object,
                _allergyDetailServiceMock.Object,
                _orderServiceMock.Object,
                _mapperMock.Object
            );
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TestMethod]
        public async Task Add_TrackStatus_ReturnsAddedTrackStatus()
        {
            // Arrange
            var requestTrack = _fixture.Create<RequestTrackStatus>();
            var track = _fixture.Create<TrackStatus>();
            var addedtrack = _fixture.Create<RequestTrackStatus>();

            // Arrange mock for IMapper
            _mapperMock.Setup(mapper => mapper.Map<TrackStatus>(It.IsAny<RequestTrackStatus>()))
                .Returns(track);

            _trackStatusRepoMock.Setup(repo => repo.Add(It.IsAny<TrackStatus>()))
                .ReturnsAsync(track);

            _mapperMock.Setup(mapper => mapper.Map<RequestTrackStatus>(It.IsAny<TrackStatus>()))
                .Returns(addedtrack);


            // Act
            var result = await _service.Add_TrackStatus(requestTrack);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RequestTrackStatus));
            Assert.AreEqual(addedtrack.OrderId, result.OrderId);
            Assert.AreEqual(addedtrack.Status, result.Status);
            Assert.AreEqual(addedtrack.Tid, result.Tid);
        }
    }
}
