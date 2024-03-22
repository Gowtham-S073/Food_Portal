using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using FoodPortal.ViewModel;
using AutoMapper;

namespace FoodPortal.Services
{
    public class AdditionalCategoryMasterService : IAdditionalCategoryMasterService
    {
        #region Fields
        private readonly ICrud<AdditionalCategoryMaster, IdDTO> _additionalCategoryMasterRepo;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor
        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="AdditionalCategoryMasterRepo"></param>
        public AdditionalCategoryMasterService(ICrud<AdditionalCategoryMaster, IdDTO> AdditionalCategoryMasterRepo,
                                                IMapper mapper)
        {
            _additionalCategoryMasterRepo = AdditionalCategoryMasterRepo;
            _mapper = mapper;
        }
        #endregion

        #region GetAllAdditionalCategories Service
        /// <summary>
        /// To get all the details AdditionalCategoryMaster
        /// </summary>
        /// <returns> list of AdditionalCategoryMaster details</returns>
        public async Task<List<ViewAdditionalCategoryMaster>> View_All_AdditionalCategoryMasters()

        {
            var AdditionalCategoryMasters = await _additionalCategoryMasterRepo.GetAll();
            List<ViewAdditionalCategoryMaster> Viewresult = _mapper.Map<List<ViewAdditionalCategoryMaster>>(AdditionalCategoryMasters);
            return Viewresult;
        }
        #endregion
    }
}
