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
using FoodPortal.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
#nullable disable


namespace FoodPortalTestProject.ServiceTesting
{
    [TestClass]
    public class AdditionalProductsDetailServiceTest
    {
        private Mock<ICrud<AdditionalProductsDetail, IdDTO>> _additionalProductsDetailRepoMock;
        private AdditionalProductsDetailService _service;
        private Fixture _fixture;
        private Mock<IMapper> _mapperMock;

        public AdditionalProductsDetailServiceTest()
        {
            _fixture = new Fixture();
            _additionalProductsDetailRepoMock = new Mock<ICrud<AdditionalProductsDetail, IdDTO>>();
            _mapperMock = new Mock<IMapper>();
            _service = new AdditionalProductsDetailService(_additionalProductsDetailRepoMock.Object, _mapperMock.Object);
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }


        [TestMethod]
        public async Task Add_AdditionalProductsDetail_ReturnsListOfAddedDetails()
        {
            // Arrange
            var requestAdditionalProductsDetails = _fixture.CreateMany<RequestAdditionalProductsDetail>().ToList();
            var additionalProductsDetails = new List<AdditionalProductsDetail>();

            // Arrange mock for IMapper
            _mapperMock
             .Setup(mapper => mapper.Map<List<AdditionalProductsDetail>>(It.IsAny<List<RequestAdditionalProductsDetail>>()))
            .Returns((List<RequestAdditionalProductsDetail> source) =>
    {
        var result = new List<AdditionalProductsDetail>();

        foreach (var requestDetail in source)
        {
            // Explicitly map properties from RequestAdditionalProductsDetail to AdditionalProductsDetail
            var additionalDetail = new AdditionalProductsDetail
            {
                Id = requestDetail.Id,
                AdditionalProductsId = requestDetail.AdditionalProductsId,
                OrderId = requestDetail.OrderId,
                Quantity = requestDetail.Quantity,
                Cost = requestDetail.Cost,
            };

            result.Add(additionalDetail);
        }

        return result;
    });


            _mapperMock
                .Setup(mapper => mapper.Map<RequestAdditionalProductsDetail>(It.IsAny<AdditionalProductsDetail>()))
                .Returns((AdditionalProductsDetail source) =>
                {
                    // Explicitly map properties from RequestAdditionalProductsDetail to AdditionalProductsDetail
                    var additionalDetail = new RequestAdditionalProductsDetail
                    {
                        Id = source.Id,
                        AdditionalProductsId = source.AdditionalProductsId,
                        OrderId = source.OrderId,
                        Quantity = source.Quantity,
                        Cost = source.Cost,
                    };

                    return additionalDetail;
                });
            _additionalProductsDetailRepoMock
                .Setup(repo => repo.Add(It.IsAny<AdditionalProductsDetail>()))
                .ReturnsAsync((AdditionalProductsDetail additionalProductsDetail) =>
                {
                    additionalProductsDetails.Add(additionalProductsDetail);
                    return additionalProductsDetail; // Return the added detail
                });

            // Act
            var result = await _service.Add_AdditionalProductsDetail(requestAdditionalProductsDetails);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<RequestAdditionalProductsDetail>));
            Assert.AreEqual(requestAdditionalProductsDetails.Count, result.Count);
            for (int i = 0; i < requestAdditionalProductsDetails.Count; i++)
            {
                Assert.AreEqual(requestAdditionalProductsDetails[i].Id, result[i].Id);
                Assert.AreEqual(requestAdditionalProductsDetails[i].AdditionalProductsId, result[i].AdditionalProductsId);
                Assert.AreEqual(requestAdditionalProductsDetails[i].OrderId, result[i].OrderId);
                Assert.AreEqual(requestAdditionalProductsDetails[i].Quantity, result[i].Quantity);
                Assert.AreEqual(requestAdditionalProductsDetails[i].Cost, result[i].Cost);
            }
        }

    }
}
