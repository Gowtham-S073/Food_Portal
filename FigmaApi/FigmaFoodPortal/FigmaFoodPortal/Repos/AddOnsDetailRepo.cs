using FoodPortal.Exceptions;
using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FoodPortal.Repos
{
    public class AddOnsDetailRepo : ICrud<AddOnsDetail, IdDTO>
    {
        #region Field
        private readonly FoodPortal4Context _context;
        #endregion

        #region Parameterized Constructor 
        public AddOnsDetailRepo(FoodPortal4Context context)
        {
            _context = context;
        }
        #endregion

        #region Repo method to add Add Ons Detail to the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>added add ons detail </returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="DuplicateRecordException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<AddOnsDetail> Add(AddOnsDetail item)
        {
            try
            {
                if (item == null)
                {
                    throw new NullException("AddOnsDetail must not be empty");
                }
                var newAddOnsDetail = _context.AddOnsDetails.FirstOrDefault(h => h.Id == item.Id);
                if (newAddOnsDetail == null)
                {
                    await _context.AddOnsDetails.AddAsync(item);
                    await _context.SaveChangesAsync();
                    return item;
                }
                throw new DuplicateRecordException("this AddOnsDetail is already exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to delete Add Ons Detail from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>deleted add ons detail</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<AddOnsDetail> Delete(IdDTO item)
        {
            try
            {
                var AddOnsDetails = await _context.AddOnsDetails.ToListAsync();
                var myAddOnsDetail = AddOnsDetails.FirstOrDefault(h => h.Id == item.IdInt);
                if (myAddOnsDetail != null)
                {
                    _context.AddOnsDetails.Remove(myAddOnsDetail);
                    await _context.SaveChangesAsync();
                    return myAddOnsDetail;
                }
                throw new NullException("this AddOnsDetail is empty");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to Get All the Add Ons Detail from the database
        /// <summary>
        /// 
        /// </summary>
        /// <returns>all the add ons details</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<List<AddOnsDetail>> GetAll()
        {
            try
            {
                var AddOnsDetails = await _context.AddOnsDetails.ToListAsync();
                if (AddOnsDetails != null)
                    return AddOnsDetails;
                throw new NullException("AddOnsDetail table is empty");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to Get the value of the Add Ons Detail by id from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>add ons detail for the id</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>

        public async Task<AddOnsDetail> GetValue(IdDTO item)
        {
            try
            {
                var AddOnsDetails = await _context.AddOnsDetails.ToListAsync();
                var AddOnsDetail = AddOnsDetails.SingleOrDefault(h => h.Id == item.IdInt);
                if (AddOnsDetail != null)
                    return AddOnsDetail;
                throw new NullException("this AddOnsDetail is empty");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to Update the Add Ons detail to the databse
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>updated add ons detail</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<AddOnsDetail> Update(AddOnsDetail item)
        {
            try
            {
                var AddOnsDetails = await _context.AddOnsDetails.ToListAsync();
                var AddOnsDetail = AddOnsDetails.SingleOrDefault(h => h.Id == item.Id);
                if (AddOnsDetail != null)
                {
                    AddOnsDetail.AddOnsId = item.AddOnsId != null ? item.AddOnsId : AddOnsDetail.AddOnsId;
                    AddOnsDetail.OrderId = item.OrderId != null ? item.OrderId : AddOnsDetail.OrderId;
                    AddOnsDetail.Quantity = item.Quantity != null ? item.Quantity : AddOnsDetail.Quantity;
                    AddOnsDetail.Cost = item.Cost != null ? item.Cost : AddOnsDetail.Cost;
                    _context.AddOnsDetails.Update(AddOnsDetail);
                    await _context.SaveChangesAsync();
                    return AddOnsDetail;
                }
                throw new NullException("this AddOnsDetail is empty");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion
    }
}
