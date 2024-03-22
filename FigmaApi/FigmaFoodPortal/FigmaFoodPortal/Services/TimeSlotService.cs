using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using AutoMapper;
using FoodPortal.ViewModel;

namespace FoodPortal.Services
{
    public class TimeSlotService : ITimeSlotService
    {
        #region Field
        private readonly ICrud<TimeSlot, IdDTO> _timeSlotRepo;
        private readonly IMapper _mapper;
        #endregion

        #region Parameterized Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeSlotRepo"></param>
        public TimeSlotService(ICrud<TimeSlot, IdDTO> timeSlotRepo, IMapper mapper)
        {
            _timeSlotRepo = timeSlotRepo;
            _mapper = mapper;
        }
        #endregion

        #region service method to get all the time slots details
        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of time slots details</returns>
        public async Task<List<ViewTimeSlot>> View_All_TimeSlots()
        {
            var TimeSlots = await _timeSlotRepo.GetAll();
            List<ViewTimeSlot> viewResult = _mapper.Map<List<ViewTimeSlot>>(TimeSlots);
            return viewResult;
        }
        #endregion
    }
}
