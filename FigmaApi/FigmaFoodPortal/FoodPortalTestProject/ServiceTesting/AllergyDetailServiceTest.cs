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
    public class AllergyDetailServiceTest
    {
        private Mock<ICrud<AllergyDetail, IdDTO>> _allergyDetailRepoMock;
        private readonly Mock<ICrud<AllergyMaster, IdDTO>> _AllergyMasterRepo;
        private readonly Mock<ICrud<TrackStatus, IdDTO>> _trackStatusRepo;
        private readonly Mock<ICrud<Order, IdDTO>> _orderRepo;
        private Fixture _fixture;
        private AllergyDetailService _service;

        public AllergyDetailServiceTest()
        {
            _allergyDetailRepoMock = new Mock<ICrud<AllergyDetail, IdDTO>>();
            _AllergyMasterRepo = new Mock<ICrud<AllergyMaster, IdDTO>>();
            _trackStatusRepo = new Mock<ICrud<TrackStatus, IdDTO>>();
            _orderRepo = new Mock<ICrud<Order, IdDTO>>();
            _fixture = new Fixture();
            _service = new AllergyDetailService(_allergyDetailRepoMock.Object, _AllergyMasterRepo.Object,
                                                   _trackStatusRepo.Object, _orderRepo.Object);

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
           .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TestMethod]
        public async Task Add_AllergyDetail_ReturnsAddedAllergyDetailList()
        {
            // Arrange
            var allergyDetailsToAdd = _fixture.CreateMany<AllergyDetail>(3).ToList();

            _allergyDetailRepoMock
                .Setup(repo => repo.Add(It.IsAny<AllergyDetail>()))
                .ReturnsAsync((AllergyDetail allergyDetail) => allergyDetail);

            // Act
            var result = await _service.Add_AllergyDetail(allergyDetailsToAdd);

            // Assert
            CollectionAssert.AreEqual(allergyDetailsToAdd, result);
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
    public class AllergyDetailServiceTest
    {
        private Mock<ICrud<AllergyDetail, IdDTO>> _allergyDetailRepoMock;
        private Mock<ICrud<AllergyMaster, IdDTO>> _allergyMasterRepoMock;
        private Mock<ICrud<TrackStatus, IdDTO>> _trackStatusRepoMock;
        private Mock<ICrud<Order, IdDTO>> _orderRepoMock;
        private AllergyDetailService _service;
        private Fixture _fixture;
        private Mock<IMapper> _mapperMock;

        public AllergyDetailServiceTest()
        {
            _fixture = new Fixture();
            _allergyDetailRepoMock = new Mock<ICrud<AllergyDetail, IdDTO>>();
            _allergyMasterRepoMock = new Mock<ICrud<AllergyMaster, IdDTO>>();
            _trackStatusRepoMock = new Mock<ICrud<TrackStatus, IdDTO>>();
            _orderRepoMock = new Mock<ICrud<Order, IdDTO>>();
            _mapperMock = new Mock<IMapper>();
            _service = new AllergyDetailService(
                _allergyDetailRepoMock.Object,
                _allergyMasterRepoMock.Object,
                _trackStatusRepoMock.Object,
                _orderRepoMock.Object,
                _mapperMock.Object
            );
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TestMethod]
        public async Task Add_AllergyDetail_ReturnsListOfAddedDetails()
        {
            // Arrange
            var requestAllergyDetails = _fixture.CreateMany<RequestAllergyDetail>().ToList();
            var allergyDetails = new List<AllergyDetail>();

            // Arrange mock for IMapper
            _mapperMock
                .Setup(mapper => mapper.Map<List<AllergyDetail>>(It.IsAny<List<RequestAllergyDetail>>()))
                .Returns((List<RequestAllergyDetail> source) =>
                {
                    var result = new List<AllergyDetail>();

                    foreach (var requestDetail in source)
                    {
                        // Explicitly map properties from RequestAllergyDetail to AllergyDetail
                        var allergyDetail = new AllergyDetail
                        {
                            Id = requestDetail.Id,
                            OrdersId = requestDetail.OrdersId,
                            AllergyId = requestDetail.AllergyId
                        };

                        result.Add(allergyDetail);
                    }

                    return result;
                });

            _mapperMock
                .Setup(mapper => mapper.Map<RequestAllergyDetail>(It.IsAny<AllergyDetail>()))
                .Returns((AllergyDetail source) =>
                {
                    // Explicitly map properties from AllergyDetail to RequestAllergyDetail
                    var requestAllergyDetail = new RequestAllergyDetail
                    {
                        Id = source.Id,
                        OrdersId = source.OrdersId,
                        AllergyId = source.AllergyId
                    };

                    return requestAllergyDetail;
                });

            _allergyDetailRepoMock
                .Setup(repo => repo.Add(It.IsAny<AllergyDetail>()))
                .ReturnsAsync((AllergyDetail allergyDetail) =>
                {
                    allergyDetails.Add(allergyDetail);
                    return allergyDetail; // Return the added detail
                });

            // Act
            var result = await _service.Add_AllergyDetail(requestAllergyDetails);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<RequestAllergyDetail>));
            Assert.AreEqual(requestAllergyDetails.Count, result.Count);

            for (int i = 0; i < requestAllergyDetails.Count; i++)
            {
                Assert.AreEqual(requestAllergyDetails[i].Id, result[i].Id);
                Assert.AreEqual(requestAllergyDetails[i].OrdersId, result[i].OrdersId);
                Assert.AreEqual(requestAllergyDetails[i].AllergyId, result[i].AllergyId);
            }
        }
    }
}
