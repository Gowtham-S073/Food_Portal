using FoodPortal.Exceptions;
using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FoodPortal.Repos
{
    public class AddOnsMasterRepo : ICrud<AddOnsMaster, IdDTO>
    {
        #region Field
        private readonly FoodPortal4Context _context;
        #endregion

        #region Parameterized Constructor
        public AddOnsMasterRepo(FoodPortal4Context context)
        {
            _context = context;
        }
        #endregion

        #region Repo method to add AddOnsMaster to the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>added add ons master</returns>
        /// <exception cref="DuplicateRecordException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<AddOnsMaster> Add(AddOnsMaster item)
        {
            try
            {
                var newAddOnsMaster = _context.AddOnsMasters.SingleOrDefault(h => h.AddOnsName == item.AddOnsName);
                if (newAddOnsMaster == null)
                {
                    await _context.AddOnsMasters.AddAsync(item);
                    await _context.SaveChangesAsync();
                    return item;
                }
                throw new DuplicateRecordException("this AddOnsMaster is already exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to delete AddOnsMaster from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>deleted AddOnsMaster</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<AddOnsMaster> Delete(IdDTO item)
        {
            try
            {

                var addOnsMasters = await _context.AddOnsMasters.ToListAsync();
                var myAddOnsMaster = addOnsMasters.FirstOrDefault(h => h.Id == item.IdInt);
                if (myAddOnsMaster != null)
                {
                    _context.AddOnsMasters.Remove(myAddOnsMaster);
                    await _context.SaveChangesAsync();
                    return myAddOnsMaster;
                }
                throw new NullException("AddOnsMaster table is empty");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to Get all the AddOnsMaster from the database
        /// <summary>
        /// 
        /// </summary>
        /// <returns>list of add ons master</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<List<AddOnsMaster>> GetAll()
        {
            try
            {
                var AddOnsMasters = await _context.AddOnsMasters.ToListAsync();
                if (AddOnsMasters != null)
                    return AddOnsMasters;
                throw new NullException("AddOnsMaster table is empty");

            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to Get the AddOnsMaster by id from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>add ons master for the id</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<AddOnsMaster> GetValue(IdDTO item)
        {
            try
            {
                var AddOnsMasters = await _context.AddOnsMasters.ToListAsync();
                var AddOnsMaster = AddOnsMasters.SingleOrDefault(h => h.Id == item.IdInt);
                if (AddOnsMaster != null)
                    return AddOnsMaster;
                throw new NullException("this AddOnsMaster  is not exist in database");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to update the AddOnsMaster to the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>updated add ons master</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>

        public async Task<AddOnsMaster> Update(AddOnsMaster item)
        {
            try
            {
                var existingAddOnsMaster = await _context.AddOnsMasters.FindAsync(item.Id);
                if (existingAddOnsMaster == null)
                {
                    throw new NullException("this AddOnsMaster  is not exist in database");
                }

                existingAddOnsMaster.AddOnsName = item.AddOnsName != null ? item.AddOnsName : existingAddOnsMaster.AddOnsName;
                existingAddOnsMaster.UnitPrice = item.UnitPrice != null ? item.UnitPrice : existingAddOnsMaster.UnitPrice;
                existingAddOnsMaster.AddOnsImage = item.AddOnsImage != null ? item.AddOnsImage : existingAddOnsMaster.AddOnsImage;
                _context.AddOnsMasters.Update(existingAddOnsMaster);
                await _context.SaveChangesAsync();
                return existingAddOnsMaster;

            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion
    }
}
