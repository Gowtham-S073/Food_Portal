using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using FoodPortal.Repos;
using FoodPortal.Exceptions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using FoodPortal.RequestModel;
using AutoMapper;
#nullable disable


namespace FoodPortal.Services
{
    public class OrderService : IOrderService
    {
        #region Fields
        private readonly ICrud<Order, IdDTO> _orderRepo;
        private readonly ICrud<TimeSlot, IdDTO> _timeSlotRepo;
        private readonly ICrud<TrackStatus, IdDTO> _trackStatusRepo;
        private readonly IMapper _mapper;
        #endregion

        #region Parameterized Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderRepo"></param>
        /// <param name="timeSlotRepo"></param>
        /// <param name="trackStatusRepo"></param>
        public OrderService(ICrud<Order, IdDTO> orderRepo, ICrud<TimeSlot, IdDTO> timeSlotRepo, ICrud<TrackStatus, IdDTO> trackStatusRepo, IMapper mapper)
        {
            _orderRepo = orderRepo;
            _timeSlotRepo = timeSlotRepo;
            _trackStatusRepo = trackStatusRepo;
            _mapper = mapper;
        }
        #endregion

        #region service method to add the order details
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Order"></param>
        /// <returns>order details which was placed</returns>
        public async Task<RequestOrder> Add_Order(RequestOrder Order)

        {
            Order newOrder = _mapper.Map<Order>(Order);
            if (newOrder.DeliveryOptionId == 1)
            {
                newOrder.Address = "Ratha Tek Meadows #51 Tower A 1st Floor OMR Road Sholinganallur Chennai 600119";
            }
            var myOrder = await _orderRepo.Add(newOrder);
            var addedOrder = _mapper.Map<RequestOrder>(myOrder);
            return addedOrder;
        }
        #endregion

        #region Service method to get the UnAvailable Date
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns> List of AvailabilityDTO which has the UnAvailable Date</returns>
        /// <exception cref="NullException"></exception>
        public async Task<List<AvailabilityDTO>> GetUnAvailableDate(DateTime date)
        {
            DateTime targetDate = date;
            var orders = await _orderRepo.GetAll();
            var timeSlots = await _timeSlotRepo.GetAll();
            if (timeSlots == null)
            {
                throw new NullException("No data in the timeSlots table");
            }
            int targetTimeslotCount = timeSlots.Count();

            var result = (from order in orders
                          where order.Date > targetDate
                          group order by order.Date into g
                          where g.Select(o => o.TimeSlotId).Distinct().Count() == targetTimeslotCount
                          select new AvailabilityDTO
                          {
                              UnavailableDate = g.Key,
                              Count = g.Count()
                          }).ToList();
            return result;
        }
        #endregion

        #region Service method get the Available TimeSlot
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns>List of AvailabilityDTO which has the Available TimeSlot based on the date</returns>
        public async Task<List<AvailabilityDTO>> GetAvailableTimeSlot(DateTime date)
        {
            DateTime targetDate = date;
            var orders = await _orderRepo.GetAll();
            var timeSlots = await _timeSlotRepo.GetAll();
            if (orders == null || timeSlots == null)
            {
                return new List<AvailabilityDTO>();
            }
            var result = timeSlots
           .Where(ts => !orders.Any(o => o.TimeSlotId == ts.Id && o.Date == targetDate))
           .Select(ts => new FoodPortal.Models.DTO.AvailabilityDTO
           {
               UnavailableDate = null,
               Count = null,
               availableTimeSlot = ts.AddTimeSlot
           }).ToList();

            return result;
        }
        #endregion

        #region Service method to get the user details 
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="propertySelector"></param>
        /// <returns>the user details</returns>
        /// <exception cref="NullException"></exception>
        public async Task<UserDetailsDTO> GetUserDetails(string id)
        {
            var order = await _orderRepo.GetAll();
            var track = await _trackStatusRepo.GetAll();
            if (order == null || track == null)
            {
                throw new NullException("No details are there for this track id");
            }
            var result = (from o in order
                          join t in track on o.Id equals t.OrderId
                          where t.Tid == id
                          select new UserDetailsDTO
                          {
                              ContactName = o.ContactName,
                              ContactNumber = o.ContactNumber,
                              ContactEmail = o.ContactEmail,
                              Date = o.Date,
                              AdditionalNote = o.AdditionalNote,
                              Address = o.Address,
                              AdditionalAllergy = o.AdditionalAllergy,

                          }).FirstOrDefault();

            return result;
        }
        #endregion
    }
}
