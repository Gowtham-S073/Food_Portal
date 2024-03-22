using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using FoodPortal.Repos;
using FoodPortal.RequestModel;
using AutoMapper;
using FoodPortal.ViewModel;

namespace FoodPortal.Services
{
    public class AdditionalProductsDetailService : IAdditionalProductsDetailService
    {
        #region Fields
        private readonly ICrud<AdditionalProductsDetail, IdDTO> _additionalProductsDetailRepo;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor
        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="additionalProductsDetailRepo"></param>
        public AdditionalProductsDetailService(ICrud<AdditionalProductsDetail, IdDTO> additionalProductsDetailRepo, IMapper mapper)
        {
            _additionalProductsDetailRepo = additionalProductsDetailRepo;
            _mapper = mapper;
        }
        #endregion

        #region Add AdditionalProductsDetail Service
        /// <summary>
        /// adds AdditionalProducts details
        /// </summary>
        /// <param name="AdditionalProductsDetail"></param>
        /// <returns></returns>
        public async Task<List<RequestAdditionalProductsDetail>> Add_AdditionalProductsDetail(List<RequestAdditionalProductsDetail> AdditionalProductsDetail)
        {

            List<RequestAdditionalProductsDetail> addedAdditionalProductsDetail = new List<RequestAdditionalProductsDetail>();

            List<AdditionalProductsDetail> newAdditionalProductsDetail = _mapper.Map<List<AdditionalProductsDetail>>(AdditionalProductsDetail);

            foreach (var additionalProductsDetail in newAdditionalProductsDetail)
            {
                var myAdditionalProductsDetail = await _additionalProductsDetailRepo.Add(additionalProductsDetail);

                if (myAdditionalProductsDetail != null)
                {
                    var addedAdditionalProduct = _mapper.Map<RequestAdditionalProductsDetail>(myAdditionalProductsDetail);
                    addedAdditionalProductsDetail.Add(addedAdditionalProduct);
                }
            }
            return addedAdditionalProductsDetail;
        }
        #endregion
    }
}
