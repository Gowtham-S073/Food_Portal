using FoodPortal.Exceptions;
using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FoodPortal.Repos
{
    public class TrackStatusRepo : ICrud<TrackStatus, IdDTO>
    {
        #region Field
        private readonly FoodPortal4Context _context;
        #endregion

        #region Parameterized constructor
        public TrackStatusRepo(FoodPortal4Context context)
        {
            _context = context;
        }
        #endregion

        #region Repo method to add Track status to the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Added Track status</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="DuplicateRecordException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<TrackStatus> Add(TrackStatus item)
        {
            try
            {
                if (item == null)
                {
                    throw new NullException("TrackStatus must not be empty");
                }
                var newTrackStatus = _context.TrackStatuses.FirstOrDefault(h => h.Id == item.Id);
                if (newTrackStatus == null)
                {
                    await _context.TrackStatuses.AddAsync(item);
                    await _context.SaveChangesAsync();
                    return item;
                }

                throw new DuplicateRecordException("this TrackStatus is already exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to delete Track status from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Delete Track status</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<TrackStatus> Delete(IdDTO item)
        {
            try
            {
                var TrackStatuss = await _context.TrackStatuses.ToListAsync();
                var myTrackStatus = TrackStatuss.FirstOrDefault(h => h.Id == item.IdInt);
                if (myTrackStatus != null)
                {
                    _context.TrackStatuses.Remove(myTrackStatus);
                    await _context.SaveChangesAsync();
                    return myTrackStatus;
                }
                throw new NullException("this TrackStatus does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to get all the Track status from the database
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Get all Track status </returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<List<TrackStatus>> GetAll()
        {
            try
            {
                var TrackStatuss = await _context.TrackStatuses.ToListAsync();
                if (TrackStatuss != null)
                    return TrackStatuss;
                throw new NullException("TrackStatus table is empty");

            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to get Track status by id from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Get Track status by id</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<TrackStatus> GetValue(IdDTO item)
        {
            try
            {
                var TrackStatuss = await _context.TrackStatuses.ToListAsync();
                var TrackStatus = TrackStatuss.SingleOrDefault(h => h.Id == item.IdInt);
                if (TrackStatus != null)
                    return TrackStatus;
                throw new NullException("this TrackStatus does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to update the track status to the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Update track status</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<TrackStatus> Update(TrackStatus item)
        {
            try
            {

                var TrackStatuss = await _context.TrackStatuses.ToListAsync();
                var TrackStatus = TrackStatuss.SingleOrDefault(h => h.Id == item.Id);
                if (TrackStatus != null)
                {
                    TrackStatus.OrderId = item.OrderId != null ? item.OrderId : TrackStatus.OrderId;
                    TrackStatus.Status = item.Status != null ? item.Status : TrackStatus.Status;

                    _context.TrackStatuses.Update(TrackStatus);
                    await _context.SaveChangesAsync();
                    return TrackStatus;
                }
                throw new NullException("this TrackStatus does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion
    }
}
