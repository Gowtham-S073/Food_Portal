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
    public class AdditionalProductsController : ControllerBase
    {
        #region Field
        private readonly IAdditionalProductService _additionalProductService;
        #endregion

        #region Parameterized constructor
        public AdditionalProductsController(IAdditionalProductService additionalProductService)
        {
            _additionalProductService = additionalProductService;
        }
        #endregion

        #region Action to view by category additional products
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cat"></param>
        /// <returns>view by category additional products</returns>
        [ProducesResponseType(typeof(ViewAdditionalProduct), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]
        public async Task<ActionResult<List<ViewAdditionalProduct>>> View_by_category_AdditionalProducts(int cat)
        {
            try
            {
                var myAdditionalProducts = await _additionalProductService.View_by_category_AdditionalProducts(cat);
                return Ok(myAdditionalProducts);
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

        #region Action to view by foodtype additional products
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isveg"></param>
        /// <param name="cat"></param>
        /// <returns>view by foodtype additional products</returns>
        [ProducesResponseType(typeof(ViewAdditionalProduct), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]
        public async Task<ActionResult<List<ViewAdditionalProduct>>> View_by_foodtype_AdditionalProducts(bool isveg, int cat)
        {
            try
            {
                var myAdditionalProducts = await _additionalProductService.View_by_foodtype_AdditionalProducts(isveg, cat);
                return Ok(myAdditionalProducts);
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
