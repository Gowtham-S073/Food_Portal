using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using FoodPortal.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
#nullable disable


namespace FoodPortal.Repos
{
    public class GroupSizeRepo : ICrud<GroupSize, IdDTO>
    {
        #region Field
        private readonly FoodPortal4Context _context;
        #endregion

        #region Parameterized Constructor
        public GroupSizeRepo(FoodPortal4Context context)
        {
            _context = context;
        }
        #endregion

        #region Repo method to add group size to the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Added group size</returns>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<GroupSize> Add(GroupSize item)
        {
            try
            {
                var newGroupSize = _context.GroupSizes.SingleOrDefault(h => h.Id == item.Id);
                if (newGroupSize == null)
                {
                    await _context.GroupSizes.AddAsync(item);
                    await _context.SaveChangesAsync();
                    return item;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to delete group size from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Deletes group size</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<GroupSize> Delete(IdDTO item)
        {
            try
            {

                var GroupSizes = await _context.GroupSizes.ToListAsync();
                var myGroupSize = GroupSizes.FirstOrDefault(h => h.Id == item.IdInt);
                if (myGroupSize != null)
                {
                    _context.GroupSizes.Remove(myGroupSize);
                    await _context.SaveChangesAsync();
                    return myGroupSize;
                }
                throw new NullException("this GroupSize does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to get all the group size from the database
        /// <summary>
        /// 
        /// </summary>
        /// <returns>get group sizes</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<List<GroupSize>> GetAll()
        {
            try
            {
                var GroupSizes = await _context.GroupSizes.ToListAsync();
                if (GroupSizes != null)
                    return GroupSizes;
                throw new NullException("AdditionalCategoryMaster table is empty");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to get group size by id from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Get group size</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<GroupSize> GetValue(IdDTO item)
        {
            try
            {
                var GroupSizes = await _context.GroupSizes.ToListAsync();
                var GroupSize = GroupSizes.SingleOrDefault(h => h.Id == item.IdInt);
                if (GroupSize != null)
                    return GroupSize;
                throw new NullException("this FoodTypeCount does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to update the group size to the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Updated value of the group size </returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<GroupSize> Update(GroupSize item)
        {
            try
            {
                var GroupSizes = await _context.GroupSizes.ToListAsync();
                var GroupSize = GroupSizes.SingleOrDefault(h => h.Id == item.Id);
                if (GroupSize != null)
                {
                    GroupSize.GroupType = item.GroupType != null ? item.GroupType : GroupSize.GroupType;
                    GroupSize.MinValue = item.MinValue != 0 ? item.MinValue : GroupSize.MinValue;
                    GroupSize.MaxValue = item.MaxValue != 0 ? item.MaxValue : GroupSize.MaxValue;

                    _context.GroupSizes.Update(GroupSize);
                    await _context.SaveChangesAsync();
                    return GroupSize;
                }
                throw new NullException("this FoodTypeCount does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion
    }
}
