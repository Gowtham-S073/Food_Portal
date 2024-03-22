using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using FoodPortal.Exceptions;
using FoodPortal.RequestModel;
using AutoMapper;

namespace FoodPortal.Services
{
    public class StdFoodOrderDetailService : IStdFoodOrderDetailService
    {
        #region Fields
        private readonly ICrud<StdFoodOrderDetail, IdDTO> _stdFoodOrderDetailRepo;
        private readonly ICrud<StdProduct, IdDTO> _StdProductRepo;
        private readonly ICrud<TrackStatus, IdDTO> _trackStatusRepo;
        private readonly ICrud<Order, IdDTO> _orderRepo;
        private readonly IMapper _mapper;
        #endregion

        #region Parameterized Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stdFoodOrderDetailRepo"></param>
        /// <param name="stdProductRepo"></param>
        /// <param name="trackStatusRepo"></param>
        /// <param name="orderRepo"></param>
        public StdFoodOrderDetailService(ICrud<StdFoodOrderDetail, IdDTO> stdFoodOrderDetailRepo, ICrud<StdProduct, IdDTO> stdProductRepo,
                                         ICrud<TrackStatus, IdDTO> trackStatusRepo, ICrud<Order, IdDTO> orderRepo, IMapper mapper)
        {
            _stdFoodOrderDetailRepo = stdFoodOrderDetailRepo;
            _StdProductRepo = stdProductRepo;
            _trackStatusRepo = trackStatusRepo;
            _orderRepo = orderRepo;
            _mapper = mapper;
        }
        #endregion

        #region Service method to add the standard food order details
        /// <summary>
        /// 
        /// </summary>
        /// <param name="StdFoodOrderDetail"></param>
        /// <returns> List of add standard food order details</returns>
        public async Task<List<RequestStdFoodOrderDetail>> Add_StdFoodOrderDetail(List<RequestStdFoodOrderDetail> StdFoodOrderDetail)

        {

            List<RequestStdFoodOrderDetail> addedStdFoodOrderDetails = new List<RequestStdFoodOrderDetail>();

            List<StdFoodOrderDetail> newStdFoodOrderDetail = _mapper.Map<List<StdFoodOrderDetail>>(StdFoodOrderDetail);

            foreach (var stdFoodOrderDetail in newStdFoodOrderDetail)
            {
                var myStdFoodOrderDetail = await _stdFoodOrderDetailRepo.Add(stdFoodOrderDetail);

                if (myStdFoodOrderDetail != null)
                {
                    var addedStdFoodOrder = _mapper.Map<RequestStdFoodOrderDetail>(myStdFoodOrderDetail);
                    addedStdFoodOrderDetails.Add(addedStdFoodOrder);
                }
            }
            return addedStdFoodOrderDetails;
        }
        #endregion

        #region Service method to get the menu of the order
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the of the order</returns>
        /// <exception cref="NullException"></exception>
        public async Task<string> Getmenu(string id)
        {
            var order = await _orderRepo.GetAll();
            var track = await _trackStatusRepo.GetAll();
            var stdproducts = await _StdProductRepo.GetAll();
            var stdfooddetails = await _stdFoodOrderDetailRepo.GetAll();
            if (order == null || track == null || stdproducts == null || stdfooddetails == null)
            {
                throw new NullException("No details are there for this track id");
            }
            string Menu = string.Join(", ", (from s in stdproducts
                                             join sd in stdfooddetails on s.Id equals sd.ProductsId
                                             join o in order on sd.OrderId equals o.Id
                                             join t in track on o.Id equals t.OrderId
                                             where t.Tid == id
                                             select s.ProductsName));
            return Menu;
        }
        #endregion
    }
}
