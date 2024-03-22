using FoodPortal.Exceptions;
using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FoodPortal.Repos
{
    public class TimeSlotRepo : ICrud<TimeSlot, IdDTO>
    {
        #region Field
        private readonly FoodPortal4Context _context;
        #endregion

        #region Parameterized constructor
        public TimeSlotRepo(FoodPortal4Context context)
        {
            _context = context;
        }
        #endregion

        #region Repo method to add Time slot to the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Added Time slot</returns>
        /// <exception cref="DuplicateRecordException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<TimeSlot> Add(TimeSlot item)
        {
            try
            {
                var newTimeSlot = _context.TimeSlots.SingleOrDefault(h => h.AddTimeSlot == item.AddTimeSlot);
                if (newTimeSlot == null)
                {
                    await _context.TimeSlots.AddAsync(item);
                    await _context.SaveChangesAsync();
                    return item;
                }
                throw new DuplicateRecordException("this TimeSlot is already exist");
            }

            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to delete the Time slot from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Deleted Time slot</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<TimeSlot> Delete(IdDTO item)
        {
            try
            {

                var TimeSlots = await _context.TimeSlots.ToListAsync();
                var myTimeSlot = TimeSlots.FirstOrDefault(h => h.Id == item.IdInt);
                if (myTimeSlot != null)
                {
                    _context.TimeSlots.Remove(myTimeSlot);
                    await _context.SaveChangesAsync();
                    return myTimeSlot;
                }
                throw new NullException("this TimeSlot does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to get all the Time slot from the database
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Get All Time slot</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<List<TimeSlot>> GetAll()
        {
            try
            {
                var TimeSlots = await _context.TimeSlots.ToListAsync();
                if (TimeSlots != null)
                    return TimeSlots;
                throw new NullException("TimeSlot table is empty");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to get Time slot by id in database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Get Time slot by id</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<TimeSlot> GetValue(IdDTO item)
        {
            try
            {
                var TimeSlots = await _context.TimeSlots.ToListAsync();
                var TimeSlot = TimeSlots.SingleOrDefault(h => h.Id == item.IdInt);
                if (TimeSlot != null)
                    return TimeSlot;
                throw new NullException("this TimeSlot does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to update the Time slot to the database 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Updated Time slot </returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<TimeSlot> Update(TimeSlot item)
        {
            try
            {
                var TimeSlots = await _context.TimeSlots.ToListAsync();
                var TimeSlot = TimeSlots.SingleOrDefault(h => h.Id == item.Id);
                if (TimeSlot != null)
                {
                    TimeSlot.AddTimeSlot = item.AddTimeSlot != null ? item.AddTimeSlot : TimeSlot.AddTimeSlot;
                    TimeSlot.StartTime = item.StartTime != null ? item.StartTime : TimeSlot.StartTime;

                    _context.TimeSlots.Update(TimeSlot);
                    await _context.SaveChangesAsync();
                    return TimeSlot;
                }
                throw new NullException("this TimeSlot does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion
    }
}
