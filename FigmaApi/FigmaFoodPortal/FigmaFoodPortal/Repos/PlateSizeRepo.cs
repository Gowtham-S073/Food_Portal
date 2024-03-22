using FoodPortal.Exceptions;
using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FoodPortal.Repos
{
    public class PlateSizeRepo : ICrud<PlateSize, IdDTO>
    {
        #region Field
        private readonly FoodPortal4Context _context;
        #endregion

        #region Parameterized constructor
        public PlateSizeRepo(FoodPortal4Context context)
        {
            _context = context;
        }
        #endregion

        #region Repo method to add the plate size to the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>add the plate size</returns>
        /// <exception cref="DuplicateRecordException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<PlateSize> Add(PlateSize item)
        {
            try
            {
                var newPlateSize = _context.PlateSizes.SingleOrDefault(h => h.PlateType == item.PlateType);
                if (newPlateSize == null)
                {
                    await _context.PlateSizes.AddAsync(item);
                    await _context.SaveChangesAsync();
                    return item;
                }

                throw new DuplicateRecordException("this PlateSize is already exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to delete the plate size from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Deleted the plate size </returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>

        public async Task<PlateSize> Delete(IdDTO item)
        {
            try
            {

                var PlateSizes = await _context.PlateSizes.ToListAsync();
                var myPlateSize = PlateSizes.FirstOrDefault(h => h.Id == item.IdInt);
                if (myPlateSize != null)
                {
                    _context.PlateSizes.Remove(myPlateSize);
                    await _context.SaveChangesAsync();
                    return myPlateSize;
                }
                throw new NullException("this PlateSize does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to get all the plate size from the database
        /// <summary>
        /// 
        /// </summary>
        /// <returns>get plate sizes</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<List<PlateSize>> GetAll()
        {
            try
            {
                var PlateSizes = await _context.PlateSizes.ToListAsync();
                if (PlateSizes != null)
                    return PlateSizes;
                throw new NullException("PlateSize table is empty");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to get plate size from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Get plate size</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<PlateSize> GetValue(IdDTO item)
        {
            try
            {
                var PlateSizes = await _context.PlateSizes.ToListAsync();
                var PlateSize = PlateSizes.SingleOrDefault(h => h.Id == item.IdInt);
                if (PlateSize != null)
                    return PlateSize;
                throw new NullException("this PlateSize does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to update the platesize to the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>updated the plate size</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<PlateSize> Update(PlateSize item)
        {
            try
            {
                var PlateSizes = await _context.PlateSizes.ToListAsync();
                var PlateSize = PlateSizes.SingleOrDefault(h => h.Id == item.Id);
                if (PlateSize != null)
                {
                    PlateSize.PlateType = item.PlateType != null ? item.PlateType : PlateSize.PlateType;
                    PlateSize.VegPlateCost = item.VegPlateCost != null ? item.VegPlateCost : PlateSize.VegPlateCost;
                    PlateSize.NonvegPlateCost = item.NonvegPlateCost != null ? item.NonvegPlateCost : PlateSize.NonvegPlateCost;

                    _context.PlateSizes.Update(PlateSize);
                    await _context.SaveChangesAsync();
                    return PlateSize;
                }
                throw new NullException("this PlateSize does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion
    }
}
