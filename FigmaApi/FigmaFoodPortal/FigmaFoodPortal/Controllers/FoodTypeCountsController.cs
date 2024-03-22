using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodPortal.Models;
using FoodPortal.Interfaces;
using FoodPortal.Exceptions;
using FoodPortal.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using FoodPortal.RequestModel;

namespace FoodPortal.Controllers
{
    [Authorize(Roles = "user")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FoodTypeCountsController : ControllerBase
    {
        #region Field
        private readonly IFoodTypeCountService _foodTypeCountService;
        #endregion

        #region Parameterized constructor
        public FoodTypeCountsController(IFoodTypeCountService foodTypeCountService)
        {
            _foodTypeCountService = foodTypeCountService;
        }
        #endregion

        #region Actions to add foodtype
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FoodTypeCount"></param>
        /// <returns>add foodtype</returns>
        [ProducesResponseType(typeof(List<RequestFoodTypeCount>), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        public async Task<ActionResult<List<RequestFoodTypeCount>>> Add_FoodTypeCount(List<RequestFoodTypeCount> FoodTypeCount)
        {
            try
            {
                var myFoodTypeCount = await _foodTypeCountService.Add_FoodTypeCount(FoodTypeCount);
                return Created("FoodTypeCount created Successfully", myFoodTypeCount);
            }

            catch (NullException ne)
            {
                return NotFound(new Error(404, ne.Message));
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
    }
}
