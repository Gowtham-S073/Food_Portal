using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using FoodPortal.Repos;
using FoodPortal.Exceptions;
using FoodPortal.RequestModel;
using AutoMapper;

namespace FoodPortal.Services
{
    public class AllergyDetailService : IAllergyDetailService
    {
        #region Field
        private readonly ICrud<AllergyDetail, IdDTO> _AllergyDetailRepo;
        private readonly ICrud<AllergyMaster, IdDTO> _AllergyMasterRepo;
        private readonly ICrud<TrackStatus, IdDTO> _trackStatusRepo;
        private readonly ICrud<Order, IdDTO> _orderRepo;
        private readonly IMapper _mapper;
        #endregion

        #region Parameterized Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AllergyDetailRepo"></param>
        /// <param name="AllergyMasterRepo"></param>
        /// <param name="trackStatusRepo"></param>
        /// <param name="orderRepo"></param>
        public AllergyDetailService(ICrud<AllergyDetail, IdDTO> AllergyDetailRepo, ICrud<AllergyMaster, IdDTO> AllergyMasterRepo
                                    , ICrud<TrackStatus, IdDTO> trackStatusRepo, ICrud<Order, IdDTO> orderRepo, IMapper mapper)
        {
            _AllergyDetailRepo = AllergyDetailRepo;
            _AllergyMasterRepo = AllergyMasterRepo;
            _trackStatusRepo = trackStatusRepo;
            _orderRepo = orderRepo;
            _mapper = mapper;

        }
        #endregion

        #region Service method that adds the allery details
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AllergyDetail"></param>
        /// <returns> List of added AllergyDetail</returns>
        public async Task<List<RequestAllergyDetail>> Add_AllergyDetail(List<RequestAllergyDetail> AllergyDetail)
        {
            List<RequestAllergyDetail> addedAllergyDetail = new List<RequestAllergyDetail>();

            List<AllergyDetail> newAllergyDetail = _mapper.Map<List<AllergyDetail>>(AllergyDetail);

            foreach (var allergyDetail in newAllergyDetail)
            {
                var myAllergyDetail = await _AllergyDetailRepo.Add(allergyDetail);

                if (myAllergyDetail != null)
                {
                    var addedAllergy = _mapper.Map<RequestAllergyDetail>(myAllergyDetail);

                    addedAllergyDetail.Add(addedAllergy);
                }
            }
            return addedAllergyDetail;
        }
        #endregion

        #region Service method to get the allergy details of the order
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the allergy details of the order</returns>
        /// <exception cref="NullException"></exception>
        public async Task<string> Getallergies(string id)
        {
            var order = await _orderRepo.GetAll();
            var track = await _trackStatusRepo.GetAll();
            var allergyMaster = await _AllergyMasterRepo.GetAll();
            var allergy = await _AllergyDetailRepo.GetAll();
            if (order == null || track == null || allergyMaster == null || allergy == null)
            {
                throw new NullException("No details are there for this track id");
            }
            var Allergies = string.Join(", ", (from am in allergyMaster
                                               join a in allergy on am.Id equals a.AllergyId
                                               join t in track on a.OrdersId equals t.OrderId
                                               where t.Tid == id
                                               select am.AllergyName).ToList());
            return Allergies;
        }
        #endregion
    }
}
