/*using AutoFixture;
using FoodPortal.Interfaces;
using FoodPortal.Models;
using FoodPortal.Models.DTO;
using FoodPortal.Repos;
using FoodPortal.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPortalTestProject.ServiceTesting
{
    [TestClass]
    public class StdFoodOrderDetailServiceTest
    {
        private Mock<ICrud<StdFoodOrderDetail, IdDTO>> _stdFoodOrderDetailRepoMock;
        private readonly Mock<ICrud<TrackStatus, IdDTO>> _trackStatusRepo;
        private readonly Mock<ICrud<Order, IdDTO>> _orderRepo;
        private readonly Mock<ICrud<StdProduct, IdDTO>> _StdProductRepo;
        private Fixture _fixture;
        private StdFoodOrderDetailService _service;

        public StdFoodOrderDetailServiceTest()
        {
            _fixture = new Fixture();
            _stdFoodOrderDetailRepoMock = new Mock<ICrud<StdFoodOrderDetail, IdDTO>>();
            _StdProductRepo = new Mock<ICrud<StdProduct, IdDTO>>();
            _trackStatusRepo = new Mock<ICrud<TrackStatus, IdDTO>>();
            _orderRepo = new Mock<ICrud<Order, IdDTO>>();
            _service = new StdFoodOrderDetailService(_stdFoodOrderDetailRepoMock.Object, _StdProductRepo.Object,
                                                     _trackStatusRepo.Object, _orderRepo.Object);

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TestMethod]
        public async Task Add_StdFoodOrderDetail_ReturnsAddedStdFoodOrderDetails()
        {
            // Arrange
            var stdFoodOrderDetailsToAdd = _fixture.CreateMany<StdFoodOrderDetail>(3).ToList();
            foreach (var detail in stdFoodOrderDetailsToAdd)
            {
                _stdFoodOrderDetailRepoMock
                    .Setup(repo => repo.Add(detail))
                    .ReturnsAsync(detail); // Return the same detail as added
            }

            // Act
            var result = await _service.Add_StdFoodOrderDetail(stdFoodOrderDetailsToAdd);

            // Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEquivalent(stdFoodOrderDetailsToAdd, result);
        }

    }
}
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
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
    public class StdFoodOrderDetailServiceTest
    {
        private Mock<ICrud<StdFoodOrderDetail, IdDTO>> _stdFoodOrderDetailRepoMock;
        private Mock<ICrud<StdProduct, IdDTO>> _stdProductRepoMock;
        private Mock<ICrud<TrackStatus, IdDTO>> _trackStatusRepoMock;
        private Mock<ICrud<Order, IdDTO>> _orderRepoMock;
        private StdFoodOrderDetailService _service;
        private Fixture _fixture;
        private Mock<IMapper> _mapperMock;

        public StdFoodOrderDetailServiceTest()
        {
            _fixture = new Fixture();
            _stdFoodOrderDetailRepoMock = new Mock<ICrud<StdFoodOrderDetail, IdDTO>>();
            _stdProductRepoMock = new Mock<ICrud<StdProduct, IdDTO>>();
            _trackStatusRepoMock = new Mock<ICrud<TrackStatus, IdDTO>>();
            _orderRepoMock = new Mock<ICrud<Order, IdDTO>>();
            _mapperMock = new Mock<IMapper>();
            _service = new StdFoodOrderDetailService(
                _stdFoodOrderDetailRepoMock.Object,
                _stdProductRepoMock.Object,
                _trackStatusRepoMock.Object,
                _orderRepoMock.Object,
                _mapperMock.Object
            );
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TestMethod]
        public async Task Add_StdFoodOrderDetail_ReturnsAddedStdFoodOrderDetails()
        {
            // Arrange
            var requestStdFoodOrderDetails = _fixture.CreateMany<RequestStdFoodOrderDetail>().ToList();
            var stdFoodOrderDetails = new List<StdFoodOrderDetail>();

            // Arrange mock for IMapper
            _mapperMock.Setup(mapper => mapper.Map<List<StdFoodOrderDetail>>(It.IsAny<List<RequestStdFoodOrderDetail>>()))
                .Returns((List<RequestStdFoodOrderDetail> source) =>
                {
                    var result = new List<StdFoodOrderDetail>();

                    foreach (var requestDetail in source)
                    {
                        // Explicitly map properties from RequestStdFoodOrderDetail to StdFoodOrderDetail
                        var stdFoodOrderDetail = new StdFoodOrderDetail
                        {
                            Id = requestDetail.Id,
                            OrderId = requestDetail.OrderId,
                            ProductsId = requestDetail.ProductsId
                        };

                        result.Add(stdFoodOrderDetail);
                    }

                    return result;
                });

            _stdFoodOrderDetailRepoMock.Setup(repo => repo.Add(It.IsAny<StdFoodOrderDetail>()))
                .ReturnsAsync((StdFoodOrderDetail stdFoodOrderDetail) =>
                {
                    stdFoodOrderDetails.Add(stdFoodOrderDetail);
                    return stdFoodOrderDetail; // Return the added detail
                });

            // Act
            var result = await _service.Add_StdFoodOrderDetail(requestStdFoodOrderDetails);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<RequestStdFoodOrderDetail>));
            Assert.AreEqual(requestStdFoodOrderDetails.Count, result.Count);
        }

        [TestMethod]
        public async Task Getmenu_ReturnsMenu()
        {
            // Arrange
            var trackId = "12345"; // Example track ID
            var orders = _fixture.CreateMany<Order>().ToList();
            var trackStatus = _fixture.CreateMany<TrackStatus>().ToList();
            var stdProducts = _fixture.CreateMany<StdProduct>().ToList();
            var stdFoodDetails = _fixture.CreateMany<StdFoodOrderDetail>().ToList();

            // Arrange mock for the repositories
            _orderRepoMock.Setup(repo => repo.GetAll()).ReturnsAsync(orders);
            _trackStatusRepoMock.Setup(repo => repo.GetAll()).ReturnsAsync(trackStatus);
            _stdProductRepoMock.Setup(repo => repo.GetAll()).ReturnsAsync(stdProducts);
            _stdFoodOrderDetailRepoMock.Setup(repo => repo.GetAll()).ReturnsAsync(stdFoodDetails);

            // Act
            var result = await _service.Getmenu(trackId);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
