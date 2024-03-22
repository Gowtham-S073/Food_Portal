using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodPortal.Models;
using Microsoft.AspNetCore.Cors;
using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Exceptions;

namespace FoodPortal.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region Field
        private readonly IUserService _userService;
        #endregion

        #region Parameterized constructor
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region Actions to register
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRegisterDTO"></param>
        /// <returns>register</returns>
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        public async Task<ActionResult<UserDTO>> Register(UserRegisterDTO userRegisterDTO)
        {
            try
            {
                UserDTO user = await _userService.Register(userRegisterDTO);
                return Created("User Registered", user);
            }
            catch (NullException ne)
            {
                return BadRequest(new Error(404, ne.Message));
            }
            catch (DuplicateRecordException de)
            {
                return BadRequest(new Error(400, de.Message));
            }
            catch (InvalidSqlException ise)
            {
                return BadRequest(new Error(500, ise.Message));
            }

        }
        #endregion

        #region Actions to login
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns>login</returns>
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        public async Task<ActionResult<UserDTO>> LogIN(UserDTO userDTO)
        {
            try
            {
                UserDTO user = await _userService.LogIN(userDTO);
                return Ok(user);
            }
            catch (NullException ne)
            {
                return NotFound(new Error(404, ne.Message));
            }
            catch (InvalidSqlException ise)
            {
                return BadRequest(new Error(500, ise.Message));
            }

        }
        #endregion

    }
}
