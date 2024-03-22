/*using AutoFixture;
using FoodPortal.Interfaces;
using FoodPortal.Models;
using FoodPortal.Models.DTO;
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
    public class FoodTypeCountServiceTest
    {
        private Mock<ICrud<FoodTypeCount, IdDTO>> _foodTypeCountRepoMock;
        private Fixture _fixture;
        private FoodTypeCountService _service;

        public FoodTypeCountServiceTest()
        {
            _fixture = new Fixture();
            _foodTypeCountRepoMock = new Mock<ICrud<FoodTypeCount, IdDTO>>();
            _service = new FoodTypeCountService(_foodTypeCountRepoMock.Object);


            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TestMethod]
        public async Task Add_FoodTypeCount_ReturnsAddedFoodTypeCounts()
        {
            // Arrange
            var foodTypeCountsToAdd = _fixture.CreateMany<FoodTypeCount>(3).ToList();

            // Mock setup
            foreach (var count in foodTypeCountsToAdd)
            {
                _foodTypeCountRepoMock
                    .Setup(repo => repo.Add(count))
                    .ReturnsAsync(count); // Return the same count as added
            }

            // Act
            var result = await _service.Add_FoodTypeCount(foodTypeCountsToAdd);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(foodTypeCountsToAdd.Count, result.Count);
            // You can further validate the contents of the returned list if needed
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
    public class FoodTypeCountServiceTest
    {
        private Mock<ICrud<FoodTypeCount, IdDTO>> _foodTypeCountRepoMock;
        private FoodTypeCountService _service;
        private Fixture _fixture;
        private Mock<IMapper> _mapperMock;

        public FoodTypeCountServiceTest()
        {
            _fixture = new Fixture();
            _foodTypeCountRepoMock = new Mock<ICrud<FoodTypeCount, IdDTO>>();
            _mapperMock = new Mock<IMapper>();
            _service = new FoodTypeCountService(_foodTypeCountRepoMock.Object, _mapperMock.Object);
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TestMethod]
        public async Task Add_FoodTypeCount_ReturnsListOfAddedDetails()
        {
            // Arrange
            var requestFoodTypeCounts = _fixture.CreateMany<RequestFoodTypeCount>().ToList();
            var foodTypeCounts = new List<FoodTypeCount>();

            // Arrange mock for IMapper
            _mapperMock
                .Setup(mapper => mapper.Map<List<FoodTypeCount>>(It.IsAny<List<RequestFoodTypeCount>>()))
                .Returns((List<RequestFoodTypeCount> source) =>
                {
                    var result = new List<FoodTypeCount>();

                    foreach (var requestDetail in source)
                    {
                        // Explicitly map properties from RequestFoodTypeCount to FoodTypeCount
                        var foodTypeCount = new FoodTypeCount
                        {
                            Id = requestDetail.Id,
                            OrderId = requestDetail.OrderId,
                            FoodTypeCount1 = requestDetail.FoodTypeCount1,
                            IsVeg = requestDetail.IsVeg,
                            PlateSizeId = requestDetail.PlateSizeId
                        };

                        result.Add(foodTypeCount);
                    }

                    return result;
                });

            _mapperMock
                .Setup(mapper => mapper.Map<RequestFoodTypeCount>(It.IsAny<FoodTypeCount>()))
                .Returns((FoodTypeCount source) =>
                {
                    // Explicitly map properties from FoodTypeCount to RequestFoodTypeCount
                    var requestFoodTypeCount = new RequestFoodTypeCount
                    {
                        Id = source.Id,
                        OrderId = source.OrderId,
                        FoodTypeCount1 = source.FoodTypeCount1,
                        IsVeg = source.IsVeg,
                        PlateSizeId = source.PlateSizeId
                    };

                    return requestFoodTypeCount;
                });

            _foodTypeCountRepoMock
                .Setup(repo => repo.Add(It.IsAny<FoodTypeCount>()))
                .ReturnsAsync((FoodTypeCount foodTypeCount) =>
                {
                    foodTypeCounts.Add(foodTypeCount);
                    return foodTypeCount; // Return the added detail
                });

            // Act
            var result = await _service.Add_FoodTypeCount(requestFoodTypeCounts);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<RequestFoodTypeCount>));
            Assert.AreEqual(requestFoodTypeCounts.Count, result.Count);

            for (int i = 0; i < requestFoodTypeCounts.Count; i++)
            {
                Assert.AreEqual(requestFoodTypeCounts[i].Id, result[i].Id);
                Assert.AreEqual(requestFoodTypeCounts[i].OrderId, result[i].OrderId);
                Assert.AreEqual(requestFoodTypeCounts[i].FoodTypeCount1, result[i].FoodTypeCount1);
                Assert.AreEqual(requestFoodTypeCounts[i].IsVeg, result[i].IsVeg);
                Assert.AreEqual(requestFoodTypeCounts[i].PlateSizeId, result[i].PlateSizeId);
            }
        }
    }
}
