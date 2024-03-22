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
    public class StdFoodOrderDetailsController : ControllerBase
    {
        #region Field
        private readonly IStdFoodOrderDetailService _stdFoodOrderDetailService;
        #endregion

        #region Parameterized constructor
        public StdFoodOrderDetailsController(IStdFoodOrderDetailService stdFoodOrderDetailService)
        {
            _stdFoodOrderDetailService = stdFoodOrderDetailService;
        }
        #endregion

        #region Actions to add food order details
        /// <summary>
        /// 
        /// </summary>
        /// <param name="StdFoodOrderDetails"></param>
        /// <returns>add food order details</returns>
        [ProducesResponseType(typeof(List<RequestStdFoodOrderDetail>), StatusCodes.Status200OK)] // Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Failure Response
        [HttpPost]
        public async Task<ActionResult<List<RequestStdFoodOrderDetail>>> Add_StdFoodOrderDetail(List<RequestStdFoodOrderDetail> StdFoodOrderDetails)
        {
            try
            {
                var addedStdFoodOrderDetails = await _stdFoodOrderDetailService.Add_StdFoodOrderDetail(StdFoodOrderDetails);
                return Created("StdFoodOrderDetails created Successfully", addedStdFoodOrderDetails);
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
