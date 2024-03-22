using FoodPortal.Exceptions;
using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FoodPortal.Repos
{
    public class AdditionalCategoryMasterRepo : ICrud<AdditionalCategoryMaster, IdDTO>
    {
        #region Field
        private readonly FoodPortal4Context _context;
        #endregion

        #region Parameterized Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public AdditionalCategoryMasterRepo(FoodPortal4Context context)
        {
            _context = context;
        }
        #endregion

        #region Repo method to add the Additional Category to the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Added the Additional Category</returns>
        /// <exception cref="DuplicateRecordException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<AdditionalCategoryMaster> Add(AdditionalCategoryMaster item)
        {
            try
            {
                var newAdditionalCategoryMaster = _context.AdditionalCategoryMasters.FirstOrDefault(h => h.AdditionalCategory == item.AdditionalCategory);
                if (newAdditionalCategoryMaster == null)
                {
                    await _context.AdditionalCategoryMasters.AddAsync(item);
                    await _context.SaveChangesAsync();
                    return item;
                }
                throw new DuplicateRecordException("this AdditionalCategoryMaster is already exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to delete the Additional Category from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>the delete data</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<AdditionalCategoryMaster> Delete(IdDTO item)
        {
            try
            {
                var additionalCategoryMasters = await _context.AdditionalCategoryMasters.ToListAsync();
                var myAdditionalCategoryMaster = additionalCategoryMasters.FirstOrDefault(h => h.Id == item.IdInt);
                if (myAdditionalCategoryMaster != null)
                {
                    _context.AdditionalCategoryMasters.Remove(myAdditionalCategoryMaster);
                    await _context.SaveChangesAsync();
                    return myAdditionalCategoryMaster;
                }
                throw new NullException("this AdditionalCategoryMaster is empty");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to get all the additional category
        /// <summary>
        /// 
        /// </summary>
        /// <returns>additional category from the data base</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<List<AdditionalCategoryMaster>> GetAll()
        {
            try
            {
                var additionalCategoryMasters = await _context.AdditionalCategoryMasters.ToListAsync();
                if (additionalCategoryMasters != null)
                    return additionalCategoryMasters;
                throw new NullException("AdditionalCategoryMaster table is empty");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to get the additional category based on the id
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>the additional category based on the id</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<AdditionalCategoryMaster> GetValue(IdDTO item)
        {
            try
            {
                var additionalCategoryMasters = await _context.AdditionalCategoryMasters.ToListAsync();
                var additionalCategoryMaster = additionalCategoryMasters.SingleOrDefault(h => h.Id == item.IdInt);
                if (additionalCategoryMaster != null)
                    return additionalCategoryMaster;
                throw new NullException("this AdditionalCategoryMaster is empty");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to update the additional category
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>updated the additional category</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<AdditionalCategoryMaster> Update(AdditionalCategoryMaster item)
        {
            try
            {
                var additionalCategoryMasters = await _context.AdditionalCategoryMasters.ToListAsync();
                var additionalCategoryMaster = additionalCategoryMasters.SingleOrDefault(h => h.Id == item.Id);
                if (additionalCategoryMaster != null)
                {
                    additionalCategoryMaster.AdditionalCategory = item.AdditionalCategory != null ? item.AdditionalCategory : additionalCategoryMaster.AdditionalCategory;
                    _context.AdditionalCategoryMasters.Update(additionalCategoryMaster);
                    await _context.SaveChangesAsync();
                    return additionalCategoryMaster;
                }
                throw new NullException("this AdditionalCategoryMaster is empty");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion
    }
}
