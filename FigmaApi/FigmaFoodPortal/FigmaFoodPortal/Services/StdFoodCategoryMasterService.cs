using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using AutoMapper;
using FoodPortal.ViewModel;

namespace FoodPortal.Services
{
    public class StdFoodCategoryMasterService : IStdFoodCategoryMasterService
    {
        #region Field
        private readonly ICrud<StdFoodCategoryMaster, IdDTO> _stdFoodCategoryMasterRepo;
        private readonly IMapper _mapper;
        #endregion

        #region Parameterized Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stdFoodCategoryMasterRepo"></param>
        public StdFoodCategoryMasterService(ICrud<StdFoodCategoryMaster, IdDTO> stdFoodCategoryMasterRepo, IMapper mapper)
        {
            _stdFoodCategoryMasterRepo = stdFoodCategoryMasterRepo;
            _mapper = mapper;
        }
        #endregion

        #region Service method to get all the standard food category
        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of standard food category</returns>
        public async Task<List<ViewStdFoodCategoryMaster>> View_All_StdFoodCategoryMasters()
        {
            var StdFoodCategoryMasters = await _stdFoodCategoryMasterRepo.GetAll();
            List<ViewStdFoodCategoryMaster> viewResult = _mapper.Map<List<ViewStdFoodCategoryMaster>>(StdFoodCategoryMasters);
            return viewResult;
        }
        #endregion
    }
}
