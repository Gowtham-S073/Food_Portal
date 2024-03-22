using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using FoodPortal.Exceptions;

namespace FoodPortal.Services
{
    public class AdditionalOrderDetailService : IAdditionalOrderDetailService
    {
        private readonly ICrud<TrackStatus, IdDTO> _trackStatusRepo;
        private readonly ICrud<Order, IdDTO> _orderRepo;
        private readonly ICrud<AddOnsMaster, IdDTO> _AddOnsMasterRepo;
        private readonly ICrud<AddOnsDetail, IdDTO> _addOnsDetailRepo;
        private readonly ICrud<AdditionalProduct, IdDTO> _additionalProductRepo;
        private readonly ICrud<AdditionalProductsDetail, IdDTO> _additionalProductsDetailRepo;
        private List<TrackStatus> track = new List<TrackStatus>();
        private List<Order> order = new List<Order>();
        private List<AdditionalProduct> additionalProduct = new List<AdditionalProduct>();
        private List<AdditionalProductsDetail> additionalProductsDetail = new List<AdditionalProductsDetail>();
        private List<AddOnsMaster> addonmaster = new List<AddOnsMaster>();
        private List<AddOnsDetail> addondetail = new List<AddOnsDetail>();

        public AdditionalOrderDetailService(ICrud<TrackStatus, IdDTO> trackStatusRepo, ICrud<AddOnsDetail, IdDTO> addOnsDetailRepo,
                                            ICrud<Order, IdDTO> orderRepo, ICrud<AddOnsMaster, IdDTO> addOnsMasterRepo,
                                            ICrud<AdditionalProduct, IdDTO> additionalProductRepo, ICrud<AdditionalProductsDetail, IdDTO> additionalProductsDetailRepo)
        {
            _trackStatusRepo = trackStatusRepo;
            _orderRepo = orderRepo;
            _addOnsDetailRepo = addOnsDetailRepo;
            _AddOnsMasterRepo = addOnsMasterRepo;
            _additionalProductRepo = additionalProductRepo;
            _additionalProductsDetailRepo = additionalProductsDetailRepo;
        }

        public async Task Setvalue()
        {
            order = await _orderRepo.GetAll();
            track = await _trackStatusRepo.GetAll();
            addondetail = await _addOnsDetailRepo.GetAll();
            addonmaster = await _AddOnsMasterRepo.GetAll();
            additionalProduct = await _additionalProductRepo.GetAll();
            additionalProductsDetail = await _additionalProductsDetailRepo.GetAll();
        }

        #region Service method to get the addon details of the order
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the addon details of the order</returns>
        /// <exception cref="NullException"></exception>
        public async Task<List<Addons>> Getaddons(string id)
        {
            await Setvalue();
            if (order == null || track == null || addondetail == null || addonmaster == null)
            {
                throw new NullException("No details are there for this track id");
            }

            var Additional = (from am in addonmaster
                              join ad in addondetail on am.Id equals ad.AddOnsId
                              join o in order on ad.OrderId equals o.Id
                              join t in track on o.Id equals t.OrderId
                              where t.Tid == id
                              select new Addons
                              {
                                  AddonName = am.AddOnsName,
                                  Price = am.UnitPrice,
                                  Quantity = ad.Quantity,
                                  PriceTotal = am.UnitPrice * ad.Quantity
                              }).ToList();
            return Additional;
        }
        #endregion

        #region Service method to get the additional product details of the order
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the List additional product details of the order</returns>
        /// <exception cref="NullException"></exception>
        public async Task<List<AdditionalProductDTO>> GetAdditionalProduct(string id)
        {
            await Setvalue();
            if (order == null || track == null || additionalProduct == null || additionalProductsDetail == null)
            {
                throw new NullException("No details are there for this track id");
            }
            var AdditionalProductInfo = (from am in additionalProduct
                                         join ad in additionalProductsDetail on am.Id equals ad.AdditionalProductsId
                                         join o in order on ad.OrderId equals o.Id
                                         join t in track on o.Id equals t.OrderId
                                         where t.Tid == id
                                         select new AdditionalProductDTO
                                         {
                                             AdditionalProductName = am.AdditionalProductsName,
                                             Price = am.UnitPrice,
                                             Quantity = ad.Quantity,
                                             PriceTotal = am.UnitPrice * ad.Quantity
                                         }).ToList();
            return AdditionalProductInfo;
        }
        #endregion

    }
}
