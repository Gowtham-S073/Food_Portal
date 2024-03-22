using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using AutoMapper;
using FoodPortal.ViewModel;
#nullable disable

namespace FoodPortal.Services
{
    public class StdProductService : IStdProductService
    {
        #region Field
        private readonly ICrud<StdProduct, IdDTO> _StdProductRepo;
        private readonly IMapper _mapper;
        #endregion

        #region Parameterized constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="StdProductRepo"></param>
        public StdProductService(ICrud<StdProduct, IdDTO> StdProductRepo, IMapper mapper)
        {
            _StdProductRepo = StdProductRepo;
            _mapper = mapper;
        }
        #endregion

        #region Service method to get all the standard products
        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of standard products</returns>
        public async Task<List<ViewStdProduct>> View_All_StdProducts()
        {
            var StdProducts = await _StdProductRepo.GetAll();
            List<ViewStdProduct> viewResult = _mapper.Map<List<ViewStdProduct>>(StdProducts);
            return viewResult;
        }
        #endregion

        #region Service method to get standard products details based on the category
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cat"></param>
        /// <returns>List of standard products details based on the category</returns>
        public async Task<List<ViewStdProduct>> View_by_category_StdProducts(int cat)
        {
            var StdProducts = await _StdProductRepo.GetAll();
            var stdProductsbycate = StdProducts?.Where(c => c.CategoriesId == cat);
            List<ViewStdProduct> viewResult = _mapper.Map<List<ViewStdProduct>>(stdProductsbycate);
            return viewResult;
        }
        #endregion
    }
}
