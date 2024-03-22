using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using AutoMapper;
using FoodPortal.ViewModel;

namespace FoodPortal.Services
{
    public class PlateSizeService : IPlateSizeService
    {
        #region Field
        private readonly ICrud<PlateSize, IdDTO> _plateSizeRepo;
        private readonly IMapper _mapper;
        #endregion

        #region Parameterized Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="plateSizeRepo"></param>
        public PlateSizeService(ICrud<PlateSize, IdDTO> plateSizeRepo, IMapper mapper)
        {
            _plateSizeRepo = plateSizeRepo;
            _mapper = mapper;
        }
        #endregion

        #region Service method to get all the plate details
        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of palte size details</returns>
        public async Task<List<ViewPlateSize>> View_All_PlateSizes()
        {
            var PlateSizes = await _plateSizeRepo.GetAll();
            List<ViewPlateSize> viewResult = _mapper.Map<List<ViewPlateSize>>(PlateSizes);
            return viewResult;
        }
        #endregion
    }
}
