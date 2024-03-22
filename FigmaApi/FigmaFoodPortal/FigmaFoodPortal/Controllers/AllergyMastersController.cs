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
    public class AllergyMastersController : ControllerBase
    {
        #region Field
        private readonly IAllergyMasterService _AllergyMasterService;
        #endregion

        #region Parameterized constructor
        public AllergyMastersController(IAllergyMasterService AllergyMasterService)
        {
            _AllergyMasterService = AllergyMasterService;
        }
        #endregion

        #region Actions to view all allergy
        /// <summary>
        /// 
        /// </summary>
        /// <returns>view all allergy</returns>
        [ProducesResponseType(typeof(ViewAllergyMaster), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]
        public async Task<ActionResult<List<ViewAllergyMaster>>> View_All_AllergyMasters()
        {
            try
            {
                var myAllergyMasters = await _AllergyMasterService.View_All_AllergyMasters();
                if (myAllergyMasters?.Count > 0)
                    return Ok(myAllergyMasters);
                return BadRequest(new Error(10, "No AllergyMasters are Existing"));
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
