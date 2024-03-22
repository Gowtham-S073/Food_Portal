using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodPortal.Models;
using FoodPortal.Exceptions;
using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using System.Runtime.Intrinsics.X86;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using FoodPortal.ViewModel;

namespace FoodPortal.Controllers
{
    [Authorize(Roles = "user")]
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class AdditionalCategoryMastersController : ControllerBase
    {
        #region Field
        private readonly IAdditionalCategoryMasterService _additionalCategoryMasterService;
        #endregion

        #region Parameterized constructor
        public AdditionalCategoryMastersController(IAdditionalCategoryMasterService additionalCategoryMasterService)
        {
            _additionalCategoryMasterService = additionalCategoryMasterService;
        }
        #endregion

        #region Action to view all additional category
        /// <summary>
        /// 
        /// </summary>
        /// <returns>View All Additional category</returns>
        [ProducesResponseType(typeof(ViewAdditionalCategoryMaster), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]
        public async Task<ActionResult<List<ViewAdditionalCategoryMaster>>> View_All_AdditionalCategoryMasters()
        {
            try
            {
                var myAdditionalCategoryMasters = await _additionalCategoryMasterService.View_All_AdditionalCategoryMasters();
                return Ok(myAdditionalCategoryMasters);
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
