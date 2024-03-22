using FoodPortal.Exceptions;
using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FoodPortal.Repos
{
    public class StdProductRepo : ICrud<StdProduct, IdDTO>
    {
        #region Field
        private readonly FoodPortal4Context _context;
        #endregion

        #region Parameterized constructor
        public StdProductRepo(FoodPortal4Context context)
        {
            _context = context;
        }
        #endregion

        #region Repo method to add the std product to the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>added the std product</returns>
        /// <exception cref="DuplicateRecordException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<StdProduct> Add(StdProduct item)
        {
            try
            {

                var newStdProduct = _context.StdProducts.FirstOrDefault(h => h.ProductsName == item.ProductsName);
                if (newStdProduct == null)
                {
                    await _context.StdProducts.AddAsync(item);
                    await _context.SaveChangesAsync();
                    return item;
                }
                throw new DuplicateRecordException("this StdProduct is already exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to delete the std product from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>deleted the std product</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<StdProduct> Delete(IdDTO item)
        {
            try
            {

                var StdProducts = await _context.StdProducts.ToListAsync();
                var myStdProduct = StdProducts.FirstOrDefault(h => h.Id == item.IdInt);
                if (myStdProduct != null)
                {
                    _context.StdProducts.Remove(myStdProduct);
                    await _context.SaveChangesAsync();
                    return myStdProduct;
                }
                throw new NullException("this StdProduct does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to get all the std product from the database
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <returns>Get all the std product</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<List<StdProduct>> GetAll()
        {
            try
            {
                var StdProducts = await _context.StdProducts.ToListAsync();
                if (StdProducts != null)
                    return StdProducts;
                throw new NullException("StdProduct table is empty");

            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to get the std product from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>get the std products</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<StdProduct> GetValue(IdDTO item)
        {
            try
            {
                var StdProducts = await _context.StdProducts.ToListAsync();
                var StdProduct = StdProducts.SingleOrDefault(h => h.Id == item.IdInt);
                if (StdProduct != null)
                    return StdProduct;
                throw new NullException("this Product  is empty");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to update the std product to the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>updated the std product</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<StdProduct> Update(StdProduct item)
        {
            try
            {

                var StdProducts = await _context.StdProducts.ToListAsync();
                var StdProduct = StdProducts.SingleOrDefault(h => h.Id == item.Id);
                if (StdProduct != null)
                {
                    StdProduct.CategoriesId = item.CategoriesId != null ? item.CategoriesId : StdProduct.CategoriesId;
                    StdProduct.ProductsName = item.ProductsName != null ? item.ProductsName : StdProduct.ProductsName;
                    StdProduct.IsVeg = item.IsVeg != null ? item.IsVeg : StdProduct.IsVeg;
                    StdProduct.FoodProductImage = item.FoodProductImage != null ? item.FoodProductImage : StdProduct.FoodProductImage;

                    _context.StdProducts.Update(StdProduct);
                    await _context.SaveChangesAsync();
                    return StdProduct;
                }
                throw new NullException("this StdProduct does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion
    }
}
