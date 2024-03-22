using FoodPortal.Exceptions;
using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FoodPortal.Repos
{
    public class DeliveryOptionRepo : ICrud<DeliveryOption, IdDTO>
    {
        #region Field
        private readonly FoodPortal4Context _context;
        #endregion

        #region Parameterized Constructor
        public DeliveryOptionRepo(FoodPortal4Context context)
        {
            _context = context;
        }
        #endregion

        #region Repo method to Add DeliveryOption to the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>added delivery option</returns>
        /// <exception cref="DuplicateRecordException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<DeliveryOption> Add(DeliveryOption item)
        {
            try
            {
                var newDeliveryOption = _context.DeliveryOptions.SingleOrDefault(h => h.DeliveryOption1 == item.DeliveryOption1);
                if (newDeliveryOption == null)
                {
                    await _context.DeliveryOptions.AddAsync(item);
                    await _context.SaveChangesAsync();
                    return item;
                }
                throw new DuplicateRecordException("this DeliveryOption is already exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to Delete DeliveryOption from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>deleted delivery option</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<DeliveryOption> Delete(IdDTO item)
        {
            try
            {
                var DeliveryOptions = await _context.DeliveryOptions.ToListAsync();
                var myDeliveryOption = DeliveryOptions.FirstOrDefault(h => h.Id == item.IdInt);
                if (myDeliveryOption != null)
                {
                    _context.DeliveryOptions.Remove(myDeliveryOption);
                    await _context.SaveChangesAsync();
                    return myDeliveryOption;
                }
                throw new NullException("this DeliveryOption does not exist");

            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to Get All the Delivery Option from the database
        /// <summary>
        /// 
        /// </summary>
        /// <returns>All the delivery options</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<List<DeliveryOption>> GetAll()
        {
            try
            {
                var DeliveryOptions = await _context.DeliveryOptions.ToListAsync();
                if (DeliveryOptions != null)
                    return DeliveryOptions;
                throw new NullException("DeliveryOption table is empty");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to Get value of delivery option  by Id from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Delivery option for the given id</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<DeliveryOption> GetValue(IdDTO item)
        {
            try
            {
                var DeliveryOptions = await _context.DeliveryOptions.ToListAsync();
                var DeliveryOption = DeliveryOptions.SingleOrDefault(h => h.Id == item.IdInt);
                if (DeliveryOption != null)
                    return DeliveryOption;
                throw new NullException("this DeliveryOption does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to update value of the delivery option to the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Updated value of delivery option</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<DeliveryOption> Update(DeliveryOption item)
        {
            try
            {
                var DeliveryOptions = await _context.DeliveryOptions.ToListAsync();
                var DeliveryOption = DeliveryOptions.SingleOrDefault(h => h.Id == item.Id);
                if (DeliveryOption != null)
                {
                    DeliveryOption.DeliveryOption1 = item.DeliveryOption1 != null ? item.DeliveryOption1 : DeliveryOption.DeliveryOption1;
                    _context.DeliveryOptions.Update(DeliveryOption);
                    await _context.SaveChangesAsync();
                    return DeliveryOption;
                }
                throw new NullException("this DeliveryOption does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion
    }
}
