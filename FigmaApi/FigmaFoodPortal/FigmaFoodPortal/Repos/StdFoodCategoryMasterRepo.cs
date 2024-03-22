using FoodPortal.Exceptions;
using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FoodPortal.Repos
{
    public class StdFoodCategoryMasterRepo : ICrud<StdFoodCategoryMaster, IdDTO>
    {
        #region Field
        private readonly FoodPortal4Context _context;
        #endregion

        #region Parameterized constructor
        public StdFoodCategoryMasterRepo(FoodPortal4Context context)
        {
            _context = context;
        }
        #endregion

        #region Repo method to add the food category to the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Added the food category</returns>
        /// <exception cref="DuplicateRecordException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<StdFoodCategoryMaster> Add(StdFoodCategoryMaster item)
        {
            try
            {
                var newStdFoodCategoryMaster = _context.StdFoodCategoryMasters.SingleOrDefault(h => h.Category == item.Category);
                if (newStdFoodCategoryMaster == null)
                {
                    await _context.StdFoodCategoryMasters.AddAsync(item);
                    await _context.SaveChangesAsync();
                    return item;
                }
                throw new DuplicateRecordException("this StdFoodCategoryMaster is already exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to delete the food category from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>deleted the food category</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<StdFoodCategoryMaster> Delete(IdDTO item)
        {
            try
            {

                var StdFoodCategoryMasters = await _context.StdFoodCategoryMasters.ToListAsync();
                var myStdFoodCategoryMaster = StdFoodCategoryMasters.FirstOrDefault(h => h.Id == item.IdInt);
                if (myStdFoodCategoryMaster != null)
                {
                    _context.StdFoodCategoryMasters.Remove(myStdFoodCategoryMaster);
                    await _context.SaveChangesAsync();
                    return myStdFoodCategoryMaster;
                }
                throw new NullException("this StdFoodCategoryMaster does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to get all the food category from the database
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Get all the food category</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<List<StdFoodCategoryMaster>> GetAll()
        {
            try
            {
                var StdFoodCategoryMasters = await _context.StdFoodCategoryMasters.ToListAsync();
                if (StdFoodCategoryMasters != null)
                    return StdFoodCategoryMasters;
                throw new NullException("StdFoodCategoryMaster table is empty");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to get the value in food category from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>get food category</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<StdFoodCategoryMaster> GetValue(IdDTO item)
        {
            try
            {
                var StdFoodCategoryMasters = await _context.StdFoodCategoryMasters.ToListAsync();
                var StdFoodCategoryMaster = StdFoodCategoryMasters.SingleOrDefault(h => h.Id == item.IdInt);
                if (StdFoodCategoryMaster != null)
                    return StdFoodCategoryMaster;
                throw new NullException("this StdFoodCategoryMaster does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to update the foodcategory to the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>updated the food category</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<StdFoodCategoryMaster> Update(StdFoodCategoryMaster item)
        {
            try
            {
                var StdFoodCategoryMasters = await _context.StdFoodCategoryMasters.ToListAsync();
                var StdFoodCategoryMaster = StdFoodCategoryMasters.SingleOrDefault(h => h.Id == item.Id);
                if (StdFoodCategoryMaster != null)
                {
                    StdFoodCategoryMaster.Category = item.Category != null ? item.Category : StdFoodCategoryMaster.Category;
                    _context.StdFoodCategoryMasters.Update(StdFoodCategoryMaster);
                    await _context.SaveChangesAsync();
                    return StdFoodCategoryMaster;
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
