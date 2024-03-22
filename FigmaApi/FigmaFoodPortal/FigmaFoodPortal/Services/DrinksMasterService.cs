using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using AutoMapper;
using FoodPortal.ViewModel;

namespace FoodPortal.Services
{
    public class DrinksMasterService : IDrinksMasterService
    {
        #region Fields
        private readonly ICrud<DrinksMaster, IdDTO> _DrinksMasterRepo;
        private readonly IMapper _mapper;
        #endregion

        #region Parameterized Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DrinksMasterRepo"></param>
        public DrinksMasterService(ICrud<DrinksMaster, IdDTO> DrinksMasterRepo, IMapper mapper)
        {
            _DrinksMasterRepo = DrinksMasterRepo;
            _mapper = mapper;
        }
        #endregion

        #region Service method to get all Drinks details
        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of Drink details</returns>
        public async Task<List<ViewDrinksMaster>> View_All_DrinksMasters()
        {
            var DrinksMasters = await _DrinksMasterRepo.GetAll();
            List<ViewDrinksMaster> viewResult = _mapper.Map<List<ViewDrinksMaster>>(DrinksMasters);
            return viewResult;
        }
        #endregion
    }
}
