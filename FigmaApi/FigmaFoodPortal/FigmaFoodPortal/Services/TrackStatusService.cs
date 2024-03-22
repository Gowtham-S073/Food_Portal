using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using NuGet.DependencyResolver;
using Microsoft.CodeAnalysis.CSharp;
using System.Text.RegularExpressions;
using FoodPortal.Exceptions;
using FoodPortal.RequestModel;
using AutoMapper;
#nullable disable


namespace FoodPortal.Services
{
    public class TrackStatusService : ITrackStatusService
    {
        #region Fields
        private readonly ICrud<TrackStatus, IdDTO> _trackStatusRepo;
        private readonly ICrud<Order, IdDTO> _orderRepo;
        private readonly ICrud<TimeSlot, IdDTO> _timeSlotRepo;
        private readonly ICrud<User, UserDTO> _userRepo;
        private readonly ICrud<DeliveryOption, IdDTO> _deliveryOptionRepo;
        private readonly ICrud<GroupSize, IdDTO> _groupSizeRepo;
        private readonly IAdditionalOrderDetailService _additionalorderservice;
        private readonly IPlateCostService _plateCostService;
        private readonly IStdFoodOrderDetailService _stdFoodOrderDetailService;
        private readonly IAllergyDetailService _allergyDetailService;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private List<TrackStatus> track;
        private List<Order> order;
        #endregion

        #region Paramerterized Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="trackStatusRepo"></param>
        /// <param name="userRepo"></param>
        /// <param name="orderRepo"></param>
        /// <param name="timeSlotRepo"></param>
        /// <param name="deliveryOptionRepo"></param>
        /// <param name="groupSizeRepo"></param>
        /// <param name="allergyDetailService"></param>
        /// <param name="stdFoodOrderDetailService"></param>
        /// <param name="plateCostService"></param>
        /// <param name="allergyDetailService"></param>
        /// <param name="additionalorderservice"></param>
        /// <param name="orderService"></param>

        public TrackStatusService(ICrud<TrackStatus, IdDTO> trackStatusRepo, ICrud<User, UserDTO> userRepo,
            ICrud<Order, IdDTO> orderRepo, ICrud<TimeSlot, IdDTO> timeSlotRepo,
            ICrud<DeliveryOption, IdDTO> deliveryOptionRepo, ICrud<GroupSize, IdDTO> groupSizeRepo,
            IAdditionalOrderDetailService additionalorderservice, IPlateCostService plateCostService,
            IStdFoodOrderDetailService stdFoodOrderDetailService, IAllergyDetailService allergyDetailService, IOrderService orderService, IMapper mapper)
        {
            _trackStatusRepo = trackStatusRepo;
            _orderRepo = orderRepo;
            _timeSlotRepo = timeSlotRepo;
            _userRepo = userRepo;
            _deliveryOptionRepo = deliveryOptionRepo;
            _groupSizeRepo = groupSizeRepo;
            _additionalorderservice = additionalorderservice;
            _plateCostService = plateCostService;
            _stdFoodOrderDetailService = stdFoodOrderDetailService;
            _allergyDetailService = allergyDetailService;
            _orderService = orderService;
            _mapper = mapper;

        }
        #endregion

        #region Service method set up data for tracking the order details
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task Setvalue()
        {
            order = await _orderRepo.GetAll();
            track = await _trackStatusRepo.GetAll();
        }
        #endregion

        #region Service method to add the track details of the order
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TrackStatus"></param>
        /// <returns> added track details</returns>
        public async Task<RequestTrackStatus> Add_TrackStatus(RequestTrackStatus TrackStatus)

        {
            TrackStatus newTrackDetail = _mapper.Map<TrackStatus>(TrackStatus);

            var myTrackStatus = await _trackStatusRepo.Add(newTrackDetail);
            var addedTrackStatus = _mapper.Map<RequestTrackStatus>(myTrackStatus);
            return addedTrackStatus;
        }
        #endregion

        #region Service method to get the Delivery Time
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the Delivery Time of the order</returns>
        /// <exception cref="NullException"></exception>
        public async Task<string> GetDeliveryTime(string id)
        {
            var time = await _timeSlotRepo.GetAll();
            if (order == null || track == null || time == null)
            {
                throw new NullException("No details are there for this track id");
            }
            var DeliveryTime = (from ts in track
                                join o in order on ts.OrderId equals o.Id
                                join t in time on o.TimeSlotId equals t.Id
                                where ts.Tid == id
                                select t.AddTimeSlot).FirstOrDefault();
            return DeliveryTime;
        }
        #endregion

        #region Service method to get the delivery type of the order
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the delivery type of the order</returns>
        /// <exception cref="NullException"></exception>
        public async Task<string> GetDeliveryType(string id)
        {
            var delivery = await _deliveryOptionRepo.GetAll();
            if (order == null || track == null || delivery == null)
            {
                throw new NullException("No details are there for this track id");
            }
            string DeliveryType = (from d in delivery
                                   join o in order on d.Id equals o.DeliveryOptionId
                                   join t in track on o.Id equals t.OrderId
                                   where t.Tid == id
                                   select d.DeliveryOption1).FirstOrDefault();
            return DeliveryType;
        }
        #endregion

        #region Service method to get the group size of the order
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the group size of the order</returns>
        /// <exception cref="NullException"></exception>
        public async Task<string> GetgroupSize(string id)
        {
            var groups = await _groupSizeRepo.GetAll();
            if (order == null || track == null || groups == null)
            {
                throw new NullException("No details are there for this track id");
            }
            var GroupSize = (from g in groups
                             join o in order on g.Id equals o.GroupSizeId
                             join t in track on o.Id equals t.OrderId
                             where t.Tid == id
                             select g.GroupType).FirstOrDefault();
            return GroupSize;
        }
        #endregion

        #region Service method to get the order Summary
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>get the order Summary</returns>
        /// <exception cref="NullException"></exception>
        public async Task<TrackDTO> Get_Order_Summary(string id)
        {
            if (id == "")
                throw new NullException("Track Id must not be empty");
            await Setvalue();
            if (order == null || track == null)
            {
                throw new NullException("No details are there for this track id");
            }
            var orderId = track?
                    .Where(ts => ts.Tid == id)
                    .Select(ts => ts.OrderId);
            var count = await _plateCostService.calculateCount(id);
            PlatesizeDTO platecostwithfoodtype = _plateCostService.GetPlatecostWithFoodType(count, id);
            var deliveryTime = await GetDeliveryTime(id);
            UserDetailsDTO userdetails = await _orderService.GetUserDetails(id);
            string allergies = await _allergyDetailService.Getallergies(id);
            string bothAllergies = allergies + " " + userdetails.AdditionalAllergy;
            string platesize = _plateCostService.GetPlatesize(id);
            string deliverytype = await GetDeliveryType(id);
            string groupsize = await GetgroupSize(id);
            int guestCount = _plateCostService.GetGuestCount(id);
            string menu = await _stdFoodOrderDetailService.Getmenu(id);
            var addons = await _additionalorderservice.Getaddons(id);
            var additional = await _additionalorderservice.GetAdditionalProduct(id);
            var result = (from ts in track
                          join o in order on ts.OrderId equals o.Id
                          where ts.Tid == id
                          select new TrackDTO
                          {
                              OrderId = o.Oid,
                              TrackId = id,
                              DeliveryDate = userdetails.Date,
                              DeliveryTime = deliveryTime,
                              Name = userdetails.ContactName,
                              PhoneNumber = userdetails.ContactNumber,
                              Email = userdetails.ContactEmail,
                              Address = userdetails.Address,
                              Allergies = allergies,
                              PlateSize = platesize,
                              AdditionalNote = userdetails.AdditionalNote,
                              DeliveryType = deliverytype,
                              GroupSize = groupsize,
                              Count = guestCount,
                              PlateCost = platecostwithfoodtype.Cost,
                              Foodtype = platecostwithfoodtype.foodtype,
                              Menu = menu,
                              Additional = addons,
                              AdditionalProductInfo = additional,
                          }).FirstOrDefault();
            return result == null ? throw new NullException("order summary  is empty") : result;
        }
        #endregion

        #region Service method to check the track belongs to the user
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        /// <returns>the track id of the user</returns>
        /// <exception cref="NullException"></exception>
        public async Task<IdDTO> check_trackid(IdDTO id, string username)
        {
            await Setvalue();
            var user = await _userRepo.GetAll();
            if (order == null || track == null || user == null)
            {
                throw new NullException("No details are there for this track id");
            }
            var is_trackid_there = (from t in track
                                    join o in order on t.OrderId equals o.Id
                                    join u in user on o.UserName equals u.UserName
                                    where t.Tid == id.IdString && o.UserName == username
                                    select t).Count();
            return is_trackid_there == 1 ? id : throw new NullException("this track id does not exist for this user name");
        }
        #endregion
    }
}
