using FoodPortal.Exceptions;
using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FoodPortal.Repos
{
    public class StdFoodOrderDetailRepo : ICrud<StdFoodOrderDetail, IdDTO>
    {
        #region Field
        private readonly FoodPortal4Context _context;
        #endregion

        #region Parameterized constructor
        public StdFoodOrderDetailRepo(FoodPortal4Context context)
        {
            _context = context;
        }
        #endregion

        #region Repo method to add the std food order detail to the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>added the std food order</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="DuplicateRecordException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<StdFoodOrderDetail> Add(StdFoodOrderDetail item)
        {
            try
            {
                if (item == null)
                {
                    throw new NullException("StdFoodOrderDetail must not be empty");
                }
                var newStdFoodOrderDetail = _context.StdFoodOrderDetails.FirstOrDefault(h => h.Id == item.Id);
                if (newStdFoodOrderDetail == null)
                {
                    await _context.StdFoodOrderDetails.AddAsync(item);
                    await _context.SaveChangesAsync();
                    return item;
                }
                throw new DuplicateRecordException("this StdFoodOrderDetail is already exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to delete the food order detail from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Deleted the std food order </returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<StdFoodOrderDetail> Delete(IdDTO item)
        {
            try
            {

                var StdFoodOrderDetails = await _context.StdFoodOrderDetails.ToListAsync();
                var myStdFoodOrderDetail = StdFoodOrderDetails.FirstOrDefault(h => h.Id == item.IdInt);
                if (myStdFoodOrderDetail != null)
                {
                    _context.StdFoodOrderDetails.Remove(myStdFoodOrderDetail);
                    await _context.SaveChangesAsync();
                    return myStdFoodOrderDetail;
                }
                throw new NullException("this StdFoodOrderDetail does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to get all the std food order from the database
        /// <summary>
        /// 
        /// </summary>
        /// <returns>get the std food order</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<List<StdFoodOrderDetail>> GetAll()
        {
            try
            {
                var StdFoodOrderDetails = await _context.StdFoodOrderDetails.ToListAsync();
                if (StdFoodOrderDetails != null)
                    return StdFoodOrderDetails;
                throw new NullException("StdFoodOrderDetail table is empty");

            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to get food order from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>get the food order detail</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<StdFoodOrderDetail> GetValue(IdDTO item)
        {
            try
            {
                var StdFoodOrderDetails = await _context.StdFoodOrderDetails.ToListAsync();
                var StdFoodOrderDetail = StdFoodOrderDetails.SingleOrDefault(h => h.Id == item.IdInt);
                if (StdFoodOrderDetail != null)
                    return StdFoodOrderDetail;
                throw new NullException("this StdFoodCategoryMaster does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to update the std food order to the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>updated the std food order</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<StdFoodOrderDetail> Update(StdFoodOrderDetail item)
        {
            try
            {

                var StdFoodOrderDetails = await _context.StdFoodOrderDetails.ToListAsync();
                var StdFoodOrderDetail = StdFoodOrderDetails.SingleOrDefault(h => h.Id == item.Id);
                if (StdFoodOrderDetail != null)
                {
                    StdFoodOrderDetail.OrderId = item.OrderId != null ? item.OrderId : StdFoodOrderDetail.OrderId;
                    StdFoodOrderDetail.ProductsId = item.ProductsId != null ? item.ProductsId : StdFoodOrderDetail.ProductsId;

                    _context.StdFoodOrderDetails.Update(StdFoodOrderDetail);
                    await _context.SaveChangesAsync();
                    return StdFoodOrderDetail;
                }
                throw new NullException("this StdFoodCategoryMaster does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion
    }
}
