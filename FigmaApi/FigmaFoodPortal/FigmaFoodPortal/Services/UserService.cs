using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using System.Security.Cryptography;
using System.Text;
using FoodPortal.Exceptions;
#nullable disable


namespace FoodPortal.Services
{
    public class UserService : IUserService
    {
        #region Fields
        private readonly ICrud<User, UserDTO> _userRepo;
        private readonly ITokenGenerate _tokenService;
        #endregion

        #region Parameterized Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRepo"></param>
        /// <param name="tokenService"></param>
        public UserService(ICrud<User, UserDTO> userRepo, ITokenGenerate tokenService)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
        }
        #endregion

        #region Service method to the check the user password
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns>return the UserDTO</returns>
        /// <exception cref="NullException"></exception>
        public async Task<UserDTO> LogIN(UserDTO userDTO)
        {
            UserDTO user;
            var userData = await _userRepo.GetValue(userDTO);
            if (userData != null)
            {
                var hmac = new HMACSHA512(userData.HashKey);
                var userPass = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
                for (int i = 0; i < userPass.Length; i++)
                {
                    if (userPass[i] != userData.Password[i])
                        throw new NullException("user Password does not match");
                }
                user = new UserDTO();
                user.UserName = userData.UserName;
                user.Token = _tokenService.GenerateToken(userData);
                return user;
            }
            throw new NullException("this user name does not exist ");
        }
        #endregion

        #region Service method to register the user
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRegisterDTO"></param>
        /// <returns>return the UserDTO</returns>
        /// <exception cref="NullException"></exception>
        public async Task<UserDTO> Register(UserRegisterDTO userRegisterDTO)
        {
            UserDTO user = null;
            using (var hmac = new HMACSHA512())
            {
                userRegisterDTO.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(userRegisterDTO.UserPassword));
                userRegisterDTO.HashKey = hmac.Key;
                userRegisterDTO.Role = "user";
                var resultUser = await _userRepo.Add(userRegisterDTO);
                if (resultUser != null)
                {
                    user = new UserDTO();
                    user.UserName = resultUser.UserName;
                    return user;
                }
                throw new NullException("Register not happened");
            }
        }
        #endregion
    }
}
