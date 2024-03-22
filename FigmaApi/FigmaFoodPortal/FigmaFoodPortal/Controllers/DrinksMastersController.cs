using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodPortal.Models;
using FoodPortal.Interfaces;
using FoodPortal.Services;
using FoodPortal.Exceptions;
using FoodPortal.ViewModel;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace FoodPortal.Controllers
{
    [Authorize(Roles = "user")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DrinksMastersController : ControllerBase
    {
        #region Field
        private readonly IDrinksMasterService _drinksMasterService;
        #endregion

        #region Parameterized constructor
        public DrinksMastersController(IDrinksMasterService drinksMasterService)
        {

            _drinksMasterService = drinksMasterService;
        }
        #endregion

        #region Actions to view all drinks
        /// <summary>
        /// 
        /// </summary>
        /// <returns>view all drinks</returns>
        [ProducesResponseType(typeof(ViewDrinksMaster), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]

        public async Task<ActionResult<List<ViewDrinksMaster>>> View_All_DrinksMasters()
        {
            try
            {
                var DrinksMasters = await _drinksMasterService.View_All_DrinksMasters();
                return Ok(DrinksMasters);
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
