using FoodPortal.Exceptions;
using FoodPortal.Interfaces;
using FoodPortal.Models;
using FoodPortal.Models.DTO;
using FoodPortal.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FoodPortal.Repos
{
    public class UserRepo : ICrud<User, UserDTO>
    {
        #region Field
        private readonly FoodPortal4Context _context;
        #endregion

        #region Parameterized constructor
        public UserRepo(FoodPortal4Context context)
        {
            _context = context;
        }
        #endregion

        #region Repo method to add User to the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Add user </returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="DuplicateRecordException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<User> Add(User user)
        {
            try
            {
                if (user == null)
                {
                    throw new NullException("user details must not be empty");
                }
                var users = _context.Users;
                var myUser = await users.SingleOrDefaultAsync(u => u.UserName == user.UserName);
                if (myUser == null)
                {
                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();
                    return user;
                }
                throw new DuplicateRecordException("this user is already exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to delete user from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns>Delete user</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<User> Delete(UserDTO userDTO)
        {
            try
            {
                var users = _context.Users;
                var myUser = users.SingleOrDefault(u => u.UserName == userDTO.UserName);
                if (myUser != null)
                {
                    _context.Users.Remove(myUser);
                    await _context.SaveChangesAsync();
                    return myUser;
                }
                throw new NullException("this User does not exist");
            }
            catch (Exception ex) { throw new InvalidSqlException(ex.Message); }
        }
        #endregion

        #region Repo method to get the user by username from database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns>Get user by username</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<User> GetValue(UserDTO userDTO)
        {
            try
            {
                var users = await GetAll();
                if (users != null)
                {
                    var user = users.FirstOrDefault(u => u.UserName == userDTO.UserName);
                    if (user != null)
                    {
                        return user;
                    }
                }
                throw new NullException("this User does not exist");
            }
            catch (Exception ex) { throw new InvalidSqlException(ex.Message); }
        }
        #endregion

        #region Repo method to get all the user from the database
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Get all user</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<List<User>> GetAll()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                if (users != null)
                    return users;
                throw new NullException(" User does not exist");
            }
            catch (Exception ex) { throw new InvalidSqlException(ex.Message); }
        }
        #endregion

        #region Repo method to update user to the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Update user</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<User> Update(User user)
        {
            try
            {
                var users = await GetAll();
                if (users != null)
                {
                    var Newuser = users.FirstOrDefault(u => u.UserName == user.UserName);
                    if (Newuser != null)
                    {
                        _context.Users.Update(Newuser);
                        await _context.SaveChangesAsync();
                        return Newuser;
                    }
                    throw new NullException("this User does not exist");
                }
                throw new NullException("User does not be empty");
            }
            catch (Exception ex) { throw new InvalidSqlException(ex.Message); }

        }
        #endregion
    }
}
