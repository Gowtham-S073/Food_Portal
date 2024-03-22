using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using AutoMapper;
using FoodPortal.ViewModel;

namespace FoodPortal.Services
{
    public class AllergyMasterService : IAllergyMasterService
    {
        #region Field
        private readonly ICrud<AllergyMaster, IdDTO> _AllergyMasterRepo;
        private readonly IMapper _mapper;
        #endregion

        #region Parameterized Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AllergyMasterRepo"></param>
        public AllergyMasterService(ICrud<AllergyMaster, IdDTO> AllergyMasterRepo, IMapper mapper)
        {
            _AllergyMasterRepo = AllergyMasterRepo;
            _mapper = mapper;
        }
        #endregion

        #region Service method to get all Allergy
        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of Allergy</returns>
        public async Task<List<ViewAllergyMaster>> View_All_AllergyMasters()

        {
            var AllergyMasters = await _AllergyMasterRepo.GetAll();
            List<ViewAllergyMaster> viewResult = _mapper.Map<List<ViewAllergyMaster>>(AllergyMasters);
            return viewResult;
        }
        #endregion
    }
}
