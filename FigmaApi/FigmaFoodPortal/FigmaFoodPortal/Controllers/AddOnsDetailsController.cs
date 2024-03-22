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
    public class AddOnsDetailsController : ControllerBase
    {
        #region Field
        private readonly IAddOnsDetailService _addOnsDetailService;
        #endregion

        #region Parameterized constructor
        public AddOnsDetailsController(IAddOnsDetailService addOnsDetailService)
        {
            _addOnsDetailService = addOnsDetailService;
        }
        #endregion

        #region Action to add addons
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AddOnsDetail"></param>
        /// <returns>add addons</returns>
        [ProducesResponseType(typeof(List<RequestAddOnsDetail>), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        public async Task<ActionResult<List<RequestAddOnsDetail>>> Add_AddOnsDetail(List<RequestAddOnsDetail> AddOnsDetail)
        {
            try
            {
                var myAddOnsDetail = await _addOnsDetailService.Add_AddOnsDetail(AddOnsDetail);
                return Created("AddOnsDetail created Successfully", myAddOnsDetail);
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
