using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using FoodPortal.Repos;
using FoodPortal.Exceptions;
using AutoMapper;
using FoodPortal.ViewModel;

namespace FoodPortal.Services
{
    public class AdditionalProductService : IAdditionalProductService
    {
        #region Fields
        private readonly ICrud<AdditionalProduct, IdDTO> _additionalProductRepo;
        private readonly ICrud<AdditionalCategoryMaster, IdDTO> _additionalCategoryMasterRepo;
        private readonly IMapper _mapper;
        #endregion

        #region Parameterized Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="additionalProductRepo"></param>
        /// <param name="additionalCategoryMasterRepo"></param>
        public AdditionalProductService(ICrud<AdditionalProduct, IdDTO> additionalProductRepo,
            ICrud<AdditionalCategoryMaster, IdDTO> additionalCategoryMasterRepo, IMapper mapper)
        {
            _additionalProductRepo = additionalProductRepo;
            _additionalCategoryMasterRepo = additionalCategoryMasterRepo;
            _mapper = mapper;
        }
        #endregion

        #region Get  Additional products based on the category
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cat"></param>
        /// <returns> List of  Additional products based on the category</returns>
        /// <exception cref="NullException"></exception>
        public async Task<List<ViewAdditionalProduct>> View_by_category_AdditionalProducts(int cat)
        {
            var AdditionalProducts = await _additionalProductRepo.GetAll();
            var AdditionalProductsbycate = AdditionalProducts.Where(a => a.AdditionalCategoryId == cat).ToList();
            List<ViewAdditionalProduct> viewResult = _mapper.Map<List<ViewAdditionalProduct>>(AdditionalProductsbycate);
            if (AdditionalProductsbycate.Count == 0)
                throw new NullException("Additional product  is empty");
            return viewResult;
        }
        #endregion

        #region Get Additional products based on the food type
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isveg"></param>
        /// <param name="cat"></param>
        /// <returns>List of Additional products based on the food type </returns>
        /// <exception cref="NullException"></exception>
        public async Task<List<ViewAdditionalProduct>> View_by_foodtype_AdditionalProducts(bool isveg, int cat)
        {
            var AdditionalProducts = await _additionalProductRepo.GetAll();
            var AdditionalProductsbyfoodtype = AdditionalProducts.Where(f => f.IsVeg == isveg && f.AdditionalCategoryId == cat).ToList();
            List<ViewAdditionalProduct> viewResult = _mapper.Map<List<ViewAdditionalProduct>>(AdditionalProductsbyfoodtype);
            if (AdditionalProductsbyfoodtype.Count == 0)
                throw new NullException("Additional product  is empty");
            return viewResult;
        }
        #endregion

    }
}
