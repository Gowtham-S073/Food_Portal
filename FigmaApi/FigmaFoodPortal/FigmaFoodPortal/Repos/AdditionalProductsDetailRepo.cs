using FoodPortal.Exceptions;
using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FoodPortal.Repos
{
    public class AdditionalProductsDetailRepo : ICrud<AdditionalProductsDetail, IdDTO>
    {
        #region Field
        private readonly FoodPortal4Context _context;
        #endregion

        #region Parameterzied Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public AdditionalProductsDetailRepo(FoodPortal4Context context)
        {
            _context = context;
        }
        #endregion

        #region Repo method to add the Additional Products Detail to database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>the added Additional Products Detail</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="DuplicateRecordException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<AdditionalProductsDetail> Add(AdditionalProductsDetail item)
        {
            try
            {
                if (item == null)
                {
                    throw new NullException("AdditionalProductsDetail must not be empty");
                }
                var newAdditionalProductsDetail = _context.AdditionalProductsDetails.FirstOrDefault(h => h.Id == item.Id);
                if (newAdditionalProductsDetail == null)
                {
                    await _context.AdditionalProductsDetails.AddAsync(item);
                    await _context.SaveChangesAsync();
                    return item;
                }
                throw new DuplicateRecordException("this AdditionalProductsDetail is already exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to delete the Additional Products detail from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns> deleted additional product details from the database</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<AdditionalProductsDetail> Delete(IdDTO item)
        {
            try
            {

                var AdditionalProductsDetails = await _context.AdditionalProductsDetails.ToListAsync();
                var myAdditionalProductsDetail = AdditionalProductsDetails.FirstOrDefault(h => h.Id == item.IdInt);
                if (myAdditionalProductsDetail != null)
                {
                    _context.AdditionalProductsDetails.Remove(myAdditionalProductsDetail);
                    await _context.SaveChangesAsync();
                    return myAdditionalProductsDetail;
                }
                throw new NullException("this AdditionalProductsDetail  is empty");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to get all the Additional Product Details from the database
        /// <summary>
        /// 
        /// </summary>
        /// <returns>all the additional product details</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>

        public async Task<List<AdditionalProductsDetail>> GetAll()
        {
            try
            {
                var AdditionalProductsDetails = await _context.AdditionalProductsDetails.ToListAsync();
                if (AdditionalProductsDetails != null)
                    return AdditionalProductsDetails;
                throw new NullException("AdditionalProductsDetail table is empty");

            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to get the additional product details by id
        /// <summary>
        /// 
        /// </summary>
        /// <returns>additional product details</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>

        public async Task<AdditionalProductsDetail> GetValue(IdDTO item)
        {
            try
            {
                var AdditionalProductsDetails = await _context.AdditionalProductsDetails.ToListAsync();
                var AdditionalProductsDetail = AdditionalProductsDetails.SingleOrDefault(h => h.Id == item.IdInt);
                if (AdditionalProductsDetail != null)
                    return AdditionalProductsDetail;
                throw new NullException("this AdditionalProductsDetail  is empty");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to update additional product details 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>updated additional products details</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>

        public async Task<AdditionalProductsDetail> Update(AdditionalProductsDetail item)
        {
            try
            {
                var AdditionalProductsDetails = await _context.AdditionalProductsDetails.ToListAsync();
                var AdditionalProductsDetail = AdditionalProductsDetails.SingleOrDefault(h => h.Id == item.Id);
                if (AdditionalProductsDetail != null)
                {
                    AdditionalProductsDetail.AdditionalProductsId = item.AdditionalProductsId != null ? item.AdditionalProductsId : AdditionalProductsDetail.AdditionalProductsId;
                    AdditionalProductsDetail.OrderId = item.OrderId != null ? item.OrderId : AdditionalProductsDetail.OrderId;
                    AdditionalProductsDetail.Quantity = item.Quantity != null ? item.Quantity : AdditionalProductsDetail.Quantity;
                    AdditionalProductsDetail.Cost = item.Cost != null ? item.Cost : AdditionalProductsDetail.Cost;
                    _context.AdditionalProductsDetails.Update(AdditionalProductsDetail);
                    await _context.SaveChangesAsync();
                    return AdditionalProductsDetail;
                }
                throw new NullException("this AdditionalProductsDetail  is empty");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion
    }
}
