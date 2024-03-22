using FoodPortal.Exceptions;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using Microsoft.Data.SqlClient;
using FoodPortal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodPortal.Repos
{
    public class AdditionalProductRepo : ICrud<AdditionalProduct, IdDTO>
    {
        #region Field
        private readonly FoodPortal4Context _context;
        #endregion

        #region Paraneterized Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public AdditionalProductRepo(FoodPortal4Context context)
        {
            _context = context;
        }
        #endregion

        #region Repo method to add the additional product to the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>added the additional product</returns>
        /// <exception cref="DuplicateRecordException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<AdditionalProduct> Add(AdditionalProduct item)
        {
            try
            {
                var newAdditionalProduct = _context.AdditionalProducts.FirstOrDefault(h => h.AdditionalProductsName == item.AdditionalProductsName);
                if (newAdditionalProduct == null)
                {
                    await _context.AdditionalProducts.AddAsync(item);
                    await _context.SaveChangesAsync();
                    return item;
                }
                throw new DuplicateRecordException("this AdditionalProduct is already exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to delete the additional product from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>deleted additional product</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<AdditionalProduct> Delete(IdDTO item)
        {
            try
            {
                var AdditionalProducts = await _context.AdditionalProducts.ToListAsync();
                var myAdditionalProduct = AdditionalProducts.FirstOrDefault(h => h.Id == item.IdInt);
                if (myAdditionalProduct != null)
                {
                    _context.AdditionalProducts.Remove(myAdditionalProduct);
                    await _context.SaveChangesAsync();
                    return myAdditionalProduct;
                }
                throw new NullException("this AdditionalProduct is empty");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to get all additional products from the database
        /// <summary>
        /// 
        /// </summary>
        /// <returns>the list of additional product</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<List<AdditionalProduct>> GetAll()
        {
            try
            {
                var AdditionalProducts = await _context.AdditionalProducts.ToListAsync();
                if (AdditionalProducts != null)
                    return AdditionalProducts;
                throw new NullException("AdditionalProduct table is empty");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to get the additional products based on the id from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>the additional products based on the id from the database</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<AdditionalProduct> GetValue(IdDTO item)
        {
            try
            {
                var AdditionalProducts = await _context.AdditionalProducts.ToListAsync();
                var AdditionalProduct = AdditionalProducts.SingleOrDefault(h => h.Id == item.IdInt);
                if (AdditionalProduct != null)
                    return AdditionalProduct;
                throw new NullException("this AdditionalProduct is empty");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to update the additional product in the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>updated additional product</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<AdditionalProduct> Update(AdditionalProduct item)
        {
            try
            {
                var existingAdditionalProduct = await _context.AdditionalProducts.FindAsync(item.Id);
                if (existingAdditionalProduct == null)
                {
                    throw new NullException("this AdditionalProduct is empty");
                }
                existingAdditionalProduct.AdditionalCategoryId = item.AdditionalCategoryId != null ? item.AdditionalCategoryId : existingAdditionalProduct.AdditionalCategoryId;
                existingAdditionalProduct.AdditionalProductsName = item.AdditionalProductsName != null ? item.AdditionalProductsName : existingAdditionalProduct.AdditionalProductsName;
                existingAdditionalProduct.IsVeg = item.IsVeg != null ? item.IsVeg : existingAdditionalProduct.IsVeg;
                existingAdditionalProduct.UnitPrice = item.UnitPrice != null ? item.UnitPrice : existingAdditionalProduct.UnitPrice;
                existingAdditionalProduct.AdditionalProductsImages = item.AdditionalProductsImages != null ? item.AdditionalProductsImages : existingAdditionalProduct.AdditionalProductsImages;
                await _context.SaveChangesAsync();
                return existingAdditionalProduct;
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion
    }
}
