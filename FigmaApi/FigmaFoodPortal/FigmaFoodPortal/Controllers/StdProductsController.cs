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
    public class StdProductsController : ControllerBase
    {
        #region Field
        private readonly IStdProductService _stdProductService;
        #endregion

        #region Parameterized constructor
        public StdProductsController(IStdProductService stdProductService)
        {
            _stdProductService = stdProductService;
        }
        #endregion

        #region Actions to view all products
        /// <summary>
        /// 
        /// </summary>
        /// <returns>view all products</returns>
        [ProducesResponseType(typeof(ViewStdProduct), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]
        public async Task<ActionResult<List<ViewStdProduct>>> View_All_StdProducts()
        {
            try
            {
                var myStdProducts = await _stdProductService.View_All_StdProducts();
                return Ok(myStdProducts);
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

        #region Actions to view by category products
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cat"></param>
        /// <returns>view by category products</returns>
        [ProducesResponseType(typeof(ViewStdProduct), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]
        public async Task<ActionResult<List<ViewStdProduct>>> View_by_category_StdProducts(int cat)
        {
            try
            {
                var myStdProducts = await _stdProductService.View_by_category_StdProducts(cat);
                return Ok(myStdProducts);
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
