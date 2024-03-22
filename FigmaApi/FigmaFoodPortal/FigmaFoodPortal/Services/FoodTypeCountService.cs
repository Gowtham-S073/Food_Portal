using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using FoodPortal.Repos;
using FoodPortal.RequestModel;
using AutoMapper;

namespace FoodPortal.Services
{
    public class FoodTypeCountService : IFoodTypeCountService
    {
        #region Field
        private readonly ICrud<FoodTypeCount, IdDTO> _FoodTypeCountRepo;
        private readonly IMapper _mapper;
        #endregion

        #region Parameterized Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FoodTypeCountRepo"></param>
        public FoodTypeCountService(ICrud<FoodTypeCount, IdDTO> FoodTypeCountRepo, IMapper mapper)
        {
            _FoodTypeCountRepo = FoodTypeCountRepo;
            _mapper = mapper;
        }
        #endregion

        #region service method to add food type count details 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FoodTypeCount"></param>
        /// <returns>List of add food type details</returns>
        public async Task<List<RequestFoodTypeCount>> Add_FoodTypeCount(List<RequestFoodTypeCount> FoodTypeCount)
        {

            List<RequestFoodTypeCount> addedFoodTypeCount = new List<RequestFoodTypeCount>();

            List<FoodTypeCount> newFoodTypeCount = _mapper.Map<List<FoodTypeCount>>(FoodTypeCount);

            foreach (var foodTypeCount in newFoodTypeCount)
            {
                var myFoodTypeCount = await _FoodTypeCountRepo.Add(foodTypeCount);

                if (myFoodTypeCount != null)
                {
                    var addedFoodType = _mapper.Map<RequestFoodTypeCount>(myFoodTypeCount);

                    addedFoodTypeCount.Add(addedFoodType);
                }
            }
            return addedFoodTypeCount;
        }
        #endregion
    }
}
