using FoodPortal.Exceptions;
using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FoodPortal.Repos
{
    public class FoodTypeCountRepo : ICrud<FoodTypeCount, IdDTO>
    {
        #region Field
        private readonly FoodPortal4Context _context;
        #endregion

        #region Parameterized constructor
        public FoodTypeCountRepo(FoodPortal4Context context)
        {
            _context = context;
        }
        #endregion

        #region Repo method to add Foodtype count to the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>added food type count to the database</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="DuplicateRecordException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<FoodTypeCount> Add(FoodTypeCount item)
        {
            try
            {
                if (item == null)
                {
                    throw new NullException("FoodTypeCount must not be empty");
                }
                var newFoodTypeCount = _context.FoodTypeCounts.FirstOrDefault(h => h.Id == item.Id);
                if (newFoodTypeCount == null)
                {
                    await _context.FoodTypeCounts.AddAsync(item);
                    await _context.SaveChangesAsync();
                    return item;
                }
                throw new DuplicateRecordException("this FoodTypeCount is already exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to delete the food type from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>deleted food type count</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<FoodTypeCount> Delete(IdDTO item)
        {
            try
            {

                var FoodTypeCounts = await _context.FoodTypeCounts.ToListAsync();
                var myFoodTypeCount = FoodTypeCounts.FirstOrDefault(h => h.Id == item.IdInt);
                if (myFoodTypeCount != null)
                {
                    _context.FoodTypeCounts.Remove(myFoodTypeCount);
                    await _context.SaveChangesAsync();
                    return myFoodTypeCount;
                }
                throw new NullException("this FoodTypeCount does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to get all the food type count from the database
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Get food type count</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<List<FoodTypeCount>> GetAll()
        {
            try
            {
                var FoodTypeCounts = await _context.FoodTypeCounts.ToListAsync();
                if (FoodTypeCounts != null)
                    return FoodTypeCounts;
                throw new NullException("FoodTypeCount table is empty");

            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to Get food type count by id  from the database 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Get food type count</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<FoodTypeCount> GetValue(IdDTO item)
        {
            try
            {
                var FoodTypeCounts = await _context.FoodTypeCounts.ToListAsync();
                var FoodTypeCount = FoodTypeCounts.SingleOrDefault(h => h.Id == item.IdInt);
                if (FoodTypeCount != null)
                    return FoodTypeCount;
                throw new NullException("this AllergyDetail does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to Update food type count to the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Updated the food type count</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<FoodTypeCount> Update(FoodTypeCount item)
        {
            try
            {

                var FoodTypeCounts = await _context.FoodTypeCounts.ToListAsync();
                var FoodTypeCount = FoodTypeCounts.SingleOrDefault(h => h.Id == item.Id);
                if (FoodTypeCount != null)
                {
                    FoodTypeCount.OrderId = item.OrderId != null ? item.OrderId : FoodTypeCount.OrderId;
                    FoodTypeCount.FoodTypeCount1 = item.FoodTypeCount1 != null ? item.FoodTypeCount1 : FoodTypeCount.FoodTypeCount1;
                    FoodTypeCount.IsVeg = item.IsVeg != null ? item.IsVeg : FoodTypeCount.IsVeg;
                    FoodTypeCount.PlateSizeId = item.PlateSizeId != null ? item.PlateSizeId : FoodTypeCount.PlateSizeId;

                    _context.FoodTypeCounts.Update(FoodTypeCount);
                    await _context.SaveChangesAsync();
                    return FoodTypeCount;
                }
                throw new NullException("this FoodTypeCount does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion
    }
}
