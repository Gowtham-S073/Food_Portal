using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using AutoMapper;
using FoodPortal.ViewModel;

namespace FoodPortal.Services
{
    public class AddOnsMasterService : IAddOnsMasterService
    {
        #region Fields
        private readonly ICrud<AddOnsMaster, IdDTO> _AddOnsMasterRepo;
        private readonly IMapper _mapper;
        #endregion

        #region Paramerterized Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AddOnsMasterRepo"></param>
        public AddOnsMasterService(ICrud<AddOnsMaster, IdDTO> AddOnsMasterRepo, IMapper mapper)
        {
            _AddOnsMasterRepo = AddOnsMasterRepo;
            _mapper = mapper;
        }
        #endregion

        #region Get all the Addons details
        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of Addons details</returns>
        public async Task<List<ViewAddOnsMaster>> View_All_AddOnsMasters()
        {
            var AddOnsMasters = await _AddOnsMasterRepo.GetAll();
            List<ViewAddOnsMaster> Viewresult = _mapper.Map<List<ViewAddOnsMaster>>(AddOnsMasters);
            return Viewresult;
        }
        #endregion
    }
}
