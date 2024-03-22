using FoodPortal.Exceptions;
using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FoodPortal.Repos
{
    public class AllergyMasterRepo : ICrud<AllergyMaster, IdDTO>
    {
        #region Field
        private readonly FoodPortal4Context _context;
        #endregion

        #region Parameterized Constructor
        public AllergyMasterRepo(FoodPortal4Context context)
        {
            _context = context;
        }
        #endregion

        #region Repo method to Add Allergy Master to the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>added allergy master</returns>
        /// <exception cref="DuplicateRecordException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<AllergyMaster> Add(AllergyMaster item)
        {
            try
            {
                var newAllergyMaster = _context.AllergyMasters.FirstOrDefault(h => h.AllergyName == item.AllergyName);
                if (newAllergyMaster == null)
                {
                    await _context.AllergyMasters.AddAsync(item);
                    await _context.SaveChangesAsync();
                    return item;
                }
                throw new DuplicateRecordException("this AllergyMaster is already exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to Delete Allergy Master from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>deleted allergy master</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<AllergyMaster> Delete(IdDTO item)
        {
            try
            {

                var AllergyMasters = await _context.AllergyMasters.ToListAsync();
                var myAllergyMaster = AllergyMasters.FirstOrDefault(h => h.Id == item.IdInt);
                if (myAllergyMaster != null)
                {
                    _context.AllergyMasters.Remove(myAllergyMaster);
                    await _context.SaveChangesAsync();
                    return myAllergyMaster;
                }
                throw new NullException("this AllergyDetail does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to Get All the Allergy Master from the database
        /// <summary>
        /// 
        /// </summary>
        /// <returns>all of the allergy masters</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<List<AllergyMaster>> GetAll()
        {
            try
            {
                var AllergyMasters = await _context.AllergyMasters.ToListAsync();
                if (AllergyMasters != null)
                    return AllergyMasters;
                throw new NullException("AllergyMaster table is empty");

            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to Get the value of allergy master by id from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>allergy master for the id</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<AllergyMaster> GetValue(IdDTO item)
        {
            try
            {
                var AllergyMasters = await _context.AllergyMasters.ToListAsync();
                var AllergyMaster = AllergyMasters.SingleOrDefault(h => h.Id == item.IdInt);
                if (AllergyMaster != null)
                    return AllergyMaster;
                throw new NullException("this AllergyDetail does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to Update the Allergy Master to the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>updated allergy master</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<AllergyMaster> Update(AllergyMaster item)
        {
            try
            {
                var AllergyMasters = await _context.AllergyMasters.ToListAsync();
                var AllergyMaster = AllergyMasters.SingleOrDefault(h => h.Id == item.Id);
                if (AllergyMaster != null)
                {
                    AllergyMaster.AllergyName = item.AllergyName != null ? item.AllergyName : AllergyMaster.AllergyName;
                    _context.AllergyMasters.Update(AllergyMaster);
                    await _context.SaveChangesAsync();
                    return AllergyMaster;
                }
                throw new NullException("this AllergyDetail does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion
    }
}
