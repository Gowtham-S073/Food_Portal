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
    public class AllergyDetailsController : ControllerBase
    {
        #region Field
        private readonly IAllergyDetailService _allergyDetailService;
        #endregion

        #region Parameterized constructor
        public AllergyDetailsController(IAllergyDetailService allergyDetailService)
        {
            _allergyDetailService = allergyDetailService;
        }
        #endregion

        #region Actions to add allergy details
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AllergyDetail"></param>
        /// <returns>add allergy details</returns>
        [ProducesResponseType(typeof(List<RequestAllergyDetail>), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        public async Task<ActionResult<List<RequestAllergyDetail>>> Add_AllergyDetail(List<RequestAllergyDetail> AllergyDetail)
        {
            try
            {
                var myAllergyDetail = await _allergyDetailService.Add_AllergyDetail(AllergyDetail);
                return Created("AllergyDetail created Successfully", myAllergyDetail);
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
