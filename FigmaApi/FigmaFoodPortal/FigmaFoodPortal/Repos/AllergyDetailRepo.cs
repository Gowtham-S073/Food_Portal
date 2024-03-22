using FoodPortal.Exceptions;
using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FoodPortal.Repos
{
    public class AllergyDetailRepo : ICrud<AllergyDetail, IdDTO>
    {
        #region Field
        private readonly FoodPortal4Context _context;
        #endregion

        #region Parameterized Constructor
        public AllergyDetailRepo(FoodPortal4Context context)
        {
            _context = context;
        }
        #endregion

        #region Repo method to add Allergy Detail to the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>added Allergy Detail</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="DuplicateRecordException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<AllergyDetail> Add(AllergyDetail item)
        {
            try
            {
                if (item == null)
                {
                    throw new NullException("AllergyDetail must not be empty");
                }

                var newAllergyDetail = _context.AllergyDetails.FirstOrDefault(h => h.Id == item.Id);
                if (newAllergyDetail == null)
                {
                    await _context.AllergyDetails.AddAsync(item);
                    await _context.SaveChangesAsync();
                    return item;
                }
                throw new DuplicateRecordException("this AllergyDetail is already exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to Delete Allergy Detail from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>deleted Allergy detail</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<AllergyDetail> Delete(IdDTO item)
        {
            try
            {

                var AllergyDetails = await _context.AllergyDetails.ToListAsync();
                var myAllergyDetail = AllergyDetails.FirstOrDefault(h => h.Id == item.IdInt);
                if (myAllergyDetail != null)
                {
                    _context.AllergyDetails.Remove(myAllergyDetail);
                    await _context.SaveChangesAsync();
                    return myAllergyDetail;
                }
                throw new NullException("this AllergyDetail does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to Get all the Allergy Detail from the database
        /// <summary>
        /// 
        /// </summary>
        /// <returns>allergy details</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<List<AllergyDetail>> GetAll()
        {
            try
            {
                var AllergyDetails = await _context.AllergyDetails.ToListAsync();
                if (AllergyDetails != null)
                    return AllergyDetails;
                throw new NullException("AllergyDetail table is empty");

            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to Get the Value of Allergy Detail by id from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>allergy detail</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<AllergyDetail> GetValue(IdDTO item)
        {
            try
            {
                var AllergyDetails = await _context.AllergyDetails.ToListAsync();
                var AllergyDetail = AllergyDetails.SingleOrDefault(h => h.Id == item.IdInt);
                if (AllergyDetail != null)
                    return AllergyDetail;
                throw new NullException("this AllergyDetail does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to Update the Allergy Deatil to the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>updated allergy detail</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<AllergyDetail> Update(AllergyDetail item)
        {
            try
            {
                var AllergyDetails = await _context.AllergyDetails.ToListAsync();
                var AllergyDetail = AllergyDetails.SingleOrDefault(h => h.Id == item.Id);
                if (AllergyDetail != null)
                {
                    AllergyDetail.AllergyId = item.AllergyId != null ? item.AllergyId : AllergyDetail.AllergyId;
                    AllergyDetail.OrdersId = item.OrdersId != null ? item.OrdersId : AllergyDetail.OrdersId;

                    _context.AllergyDetails.Update(AllergyDetail);
                    await _context.SaveChangesAsync();
                    return AllergyDetail;
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
