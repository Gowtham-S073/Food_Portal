using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using FoodPortal.Repos;
using AutoMapper;
using FoodPortal.RequestModel;

namespace FoodPortal.Services
{
    public class AddOnsDetailService : IAddOnsDetailService
    {
        #region Fields
        private readonly ICrud<AddOnsDetail, IdDTO> _addOnsDetailRepo;
        private readonly IMapper _mapper;
        #endregion

        #region Parameterized Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="addOnsDetailRepo"></param>
        public AddOnsDetailService(ICrud<AddOnsDetail, IdDTO> addOnsDetailRepo, IMapper mapper)
        {
            _addOnsDetailRepo = addOnsDetailRepo;
            _mapper = mapper;
        }
        #endregion

        #region Method that add Addon Details of the order 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AddOnsDetail"></param>
        /// <returns>List of addons details</returns>
        public async Task<List<RequestAddOnsDetail>> Add_AddOnsDetail(List<RequestAddOnsDetail> AddOnsDetail)
        {
            List<RequestAddOnsDetail> addedAddOnsDetail = new List<RequestAddOnsDetail>();

            List<AddOnsDetail> newAddOnsDetail = _mapper.Map<List<AddOnsDetail>>(AddOnsDetail);

            foreach (var addOnsDetail in newAddOnsDetail)
            {
                var myAddOnsDetail = await _addOnsDetailRepo.Add(addOnsDetail);

                if (myAddOnsDetail != null)
                {
                    var addedAddOns = _mapper.Map<RequestAddOnsDetail>(myAddOnsDetail);
                    addedAddOnsDetail.Add(addedAddOns);
                }
            }
            return addedAddOnsDetail;
        }
        #endregion
    }
}
