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
using FoodPortal.ViewModel;

namespace FoodPortal.Controllers
{
    [Authorize(Roles = "user")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GroupSizesController : ControllerBase
    {
        #region Field
        private readonly IGroupSizeService _groupSizeService;
        #endregion

        #region Parameterized constructor
        public GroupSizesController(IGroupSizeService groupSizeService)
        {
            _groupSizeService = groupSizeService;
        }
        #endregion

        #region Actions to view all group size
        /// <summary>
        /// 
        /// </summary>
        /// <returns>view all group size</returns>
        [ProducesResponseType(typeof(ViewGroupSize), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]

        public async Task<ActionResult<List<ViewGroupSize>>> View_All_GroupSizes()
        {
            try
            {
                var myGroupSizes = await _groupSizeService.View_All_GroupSizes();
                if (myGroupSizes?.Count > 0)
                    return Ok(myGroupSizes);
                return BadRequest(new Error(10, "No GroupSizes are Existing"));
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
