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
    public class PlateSizesController : ControllerBase
    {
        #region Field
        private readonly IPlateSizeService _plateSizeService;
        #endregion

        #region Parameterized constructor
        public PlateSizesController(IPlateSizeService plateSizeService)
        {
            _plateSizeService = plateSizeService;
        }
        #endregion

        #region Actions to view all plate sizes
        /// <summary>
        /// 
        /// </summary>
        /// <returns>view all plate sizes</returns>
        [ProducesResponseType(typeof(ViewPlateSize), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]
        public async Task<ActionResult<List<ViewPlateSize>>> View_All_PlateSizes()
        {
            try
            {
                var myPlateSizes = await _plateSizeService.View_All_PlateSizes();
                return Ok(myPlateSizes);
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
