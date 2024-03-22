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
using System.Runtime.Intrinsics.X86;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using FoodPortal.RequestModel;

namespace FoodPortal.Controllers
{
    [Authorize(Roles = "user")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdditionalProductsDetailsController : ControllerBase
    {
        #region Field
        private readonly IAdditionalProductsDetailService _additionalProductsDetailService;
        #endregion

        #region Parameterized constructor
        public AdditionalProductsDetailsController(IAdditionalProductsDetailService additionalProductsDetailService)
        {
            _additionalProductsDetailService = additionalProductsDetailService;
        }
        #endregion

        #region Action to add additional products 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AdditionalProductsDetail"></param>
        /// <returns>add additional products </returns>
        [ProducesResponseType(typeof(RequestAdditionalProductsDetail), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        public async Task<ActionResult<List<RequestAdditionalProductsDetail>>> Add_AdditionalProductsDetail(List<RequestAdditionalProductsDetail> AdditionalProductsDetail)
        {

            try
            {
                var myAdditionalProductsDetail = await _additionalProductsDetailService.Add_AdditionalProductsDetail(AdditionalProductsDetail);
                return Created("AdditionalProductsDetail created Successfully", myAdditionalProductsDetail);
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
