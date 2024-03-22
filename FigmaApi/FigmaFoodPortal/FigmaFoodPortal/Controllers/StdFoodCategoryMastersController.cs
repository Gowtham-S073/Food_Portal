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
    public class StdFoodCategoryMastersController : ControllerBase
    {
        #region Field
        private readonly IStdFoodCategoryMasterService _stdFoodCategoryMasterService;
        #endregion

        #region Parameterized constructor
        public StdFoodCategoryMastersController(IStdFoodCategoryMasterService stdFoodCategoryMasterService)
        {
            _stdFoodCategoryMasterService = stdFoodCategoryMasterService;
        }
        #endregion

        #region Actions to view all food category
        /// <summary>
        /// 
        /// </summary>
        /// <returns>view all food category</returns>
        [ProducesResponseType(typeof(ViewStdFoodCategoryMaster), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]
        public async Task<ActionResult<List<ViewStdFoodCategoryMaster>>> View_All_StdFoodCategoryMasters()
        {
            try
            {
                var myStdFoodCategoryMasters = await _stdFoodCategoryMasterService.View_All_StdFoodCategoryMasters();
                return Ok(myStdFoodCategoryMasters);
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
