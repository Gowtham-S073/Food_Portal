using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using AutoMapper;
using FoodPortal.ViewModel;

namespace FoodPortal.Services
{
    public class DeliveryOptionService : IDeliveryOptionService
    {
        #region Fields
        private readonly ICrud<DeliveryOption, IdDTO> _deliveryOptionRepo;
        private readonly IMapper _mapper;
        #endregion

        #region Parameterized Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deliveryOptionRepo"></param>
        public DeliveryOptionService(ICrud<DeliveryOption, IdDTO> deliveryOptionRepo, IMapper mapper)
        {
            _deliveryOptionRepo = deliveryOptionRepo;
            _mapper = mapper;
        }
        #endregion

        #region Service method to get all DeliveryOptions
        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of DeliveryOptions</returns>
        public async Task<List<ViewDeliveryOption>> View_All_DeliveryOptions()
        {
            var DeliveryOptions = await _deliveryOptionRepo.GetAll();
            List<ViewDeliveryOption> viewResult = _mapper.Map<List<ViewDeliveryOption>>(DeliveryOptions);
            return viewResult;
        }
        #endregion
    }
}
