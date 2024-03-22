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
    public class AddOnsMastersController : ControllerBase
    {
        #region Field
        private readonly IAddOnsMasterService _addOnsMasterService;
        #endregion

        #region Parameterized constructor
        public AddOnsMastersController(IAddOnsMasterService addOnsMasterService)
        {
            _addOnsMasterService = addOnsMasterService;
        }
        #endregion

        #region Actions to view all addons
        /// <summary>
        /// 
        /// </summary>
        /// <returns>view all addons</returns>
        [ProducesResponseType(typeof(ViewAddOnsMaster), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]
        public async Task<ActionResult<List<ViewAddOnsMaster>>> View_All_AddOnsMasters()
        {
            try
            {
                var myAddOnsMasters = await _addOnsMasterService.View_All_AddOnsMasters();
                return Ok(myAddOnsMasters);
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
