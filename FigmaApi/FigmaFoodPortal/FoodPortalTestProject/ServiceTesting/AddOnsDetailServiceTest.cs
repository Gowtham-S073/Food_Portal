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
using FoodPortal.Repos;
using FoodPortal.RequestModel;
using FoodPortal.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FoodPortalTestProject.ServiceTesting
{
    [TestClass]
    public class AddOnsDetailServiceTest
    {
        private Mock<ICrud<AddOnsDetail, IdDTO>> _addOnsDetailRepoMock;
        private AddOnsDetailService _service;
        private Fixture _fixture;
        private Mock<IMapper> _mapperMock;

        public AddOnsDetailServiceTest()
        {
            _fixture = new Fixture();
            _addOnsDetailRepoMock = new Mock<ICrud<AddOnsDetail, IdDTO>>();
            _mapperMock = new Mock<IMapper>();
            _service = new AddOnsDetailService(_addOnsDetailRepoMock.Object, _mapperMock.Object);
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TestMethod]
        public async Task Add_AddOnsDetail_ReturnsListOfAddedDetails()
        {
            // Arrange
            var requestAddOnsDetails = _fixture.CreateMany<RequestAddOnsDetail>().ToList();
            var addOnsDetails = new List<AddOnsDetail>();

            // Arrange mock for IMapper
            _mapperMock
                .Setup(mapper => mapper.Map<List<AddOnsDetail>>(It.IsAny<List<RequestAddOnsDetail>>()))
                .Returns((List<RequestAddOnsDetail> source) =>
                {
                    var result = new List<AddOnsDetail>();

                    foreach (var requestDetail in source)
                    {
                        // Explicitly map properties from RequestAddOnsDetail to AddOnsDetail
                        var addOnsDetail = new AddOnsDetail
                        {
                            Id = requestDetail.Id,
                            AddOnsId = requestDetail.AddOnsId,
                            OrderId = requestDetail.OrderId,
                            Quantity = requestDetail.Quantity,
                            Cost = requestDetail.Cost
                        };

                        result.Add(addOnsDetail);
                    }

                    return result;
                });

            _mapperMock
                .Setup(mapper => mapper.Map<RequestAddOnsDetail>(It.IsAny<AddOnsDetail>()))
                .Returns((AddOnsDetail source) =>
                {
                    // Explicitly map properties from AddOnsDetail to RequestAddOnsDetail
                    var requestAddOnsDetail = new RequestAddOnsDetail
                    {
                        Id = source.Id,
                        AddOnsId = source.AddOnsId,
                        OrderId = source.OrderId,
                        Quantity = source.Quantity,
                        Cost = source.Cost
                    };

                    return requestAddOnsDetail;
                });

            _addOnsDetailRepoMock
                .Setup(repo => repo.Add(It.IsAny<AddOnsDetail>()))
                .ReturnsAsync((AddOnsDetail addOnsDetail) =>
                {
                    addOnsDetails.Add(addOnsDetail);
                    return addOnsDetail; // Return the added detail
                });

            // Act
            var result = await _service.Add_AddOnsDetail(requestAddOnsDetails);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<RequestAddOnsDetail>));
            Assert.AreEqual(requestAddOnsDetails.Count, result.Count);

            for (int i = 0; i < requestAddOnsDetails.Count; i++)
            {
                Assert.AreEqual(requestAddOnsDetails[i].Id, result[i].Id);
                Assert.AreEqual(requestAddOnsDetails[i].AddOnsId, result[i].AddOnsId);
                Assert.AreEqual(requestAddOnsDetails[i].OrderId, result[i].OrderId);
                Assert.AreEqual(requestAddOnsDetails[i].Quantity, result[i].Quantity);
                Assert.AreEqual(requestAddOnsDetails[i].Cost, result[i].Cost);
            }
        }

    }
}
