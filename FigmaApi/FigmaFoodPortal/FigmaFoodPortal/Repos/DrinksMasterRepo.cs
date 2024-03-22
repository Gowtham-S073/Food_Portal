using FoodPortal.Exceptions;
using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FoodPortal.Repos
{
    public class DrinksMasterRepo : ICrud<DrinksMaster, IdDTO>
    {
        #region Field
        private readonly FoodPortal4Context _context;
        #endregion

        #region Parameterized constructor
        public DrinksMasterRepo(FoodPortal4Context context)
        {
            _context = context;
        }
        #endregion

        #region Repo method to Add drinks to the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Added drinks</returns>
        /// <exception cref="DuplicateRecordException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<DrinksMaster> Add(DrinksMaster item)
        {
            try
            {
                var newDrinksMaster = _context.DrinksMasters.FirstOrDefault(h => h.Id == item.Id);
                if (newDrinksMaster == null)
                {
                    await _context.DrinksMasters.AddAsync(item);
                    await _context.SaveChangesAsync();
                    return item;
                }
                throw new DuplicateRecordException("this DrinksMaster is already exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to Delete the drinks by id from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>deletes the drinks by given id</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>

        public async Task<DrinksMaster> Delete(IdDTO item)
        {
            try
            {

                var DrinksMasters = await _context.DrinksMasters.ToListAsync();
                var myDrinksMaster = DrinksMasters.FirstOrDefault(h => h.Id == item.IdInt);
                if (myDrinksMaster != null)
                {
                    _context.DrinksMasters.Remove(myDrinksMaster);
                    await _context.SaveChangesAsync();
                    return myDrinksMaster;
                }
                throw new NullException("this DrinksMaster does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to get all the drinks from the database
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Get all drinks </returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<List<DrinksMaster>> GetAll()
        {
            try
            {
                var DrinksMasters = await _context.DrinksMasters.ToListAsync();
                if (DrinksMasters != null)
                    return DrinksMasters;
                throw new NullException("DrinksMaster table is empty");

            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to Get the drink from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Get the drink</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<DrinksMaster> GetValue(IdDTO item)
        {
            try
            {
                var DrinksMasters = await _context.DrinksMasters.ToListAsync();
                var DrinksMaster = DrinksMasters.SingleOrDefault(h => h.Id == item.IdInt);
                if (DrinksMaster != null)
                    return DrinksMaster;
                throw new NullException("this AllergyDetail does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to update the drink to the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Updated drink</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>


        public async Task<DrinksMaster> Update(DrinksMaster item)
        {
            try
            {
                var DrinksMasters = await _context.DrinksMasters.ToListAsync();
                var DrinksMaster = DrinksMasters.SingleOrDefault(h => h.Id == item.Id);
                if (DrinksMaster != null)
                {
                    DrinksMaster.DrinkName = item.DrinkName != null ? item.DrinkName : DrinksMaster.DrinkName;
                    _context.DrinksMasters.Update(DrinksMaster);
                    await _context.SaveChangesAsync();
                    return DrinksMaster;
                }
                throw new NullException("this DrinksMaster does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion
    }
}
