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
    public class DeliveryOptionsController : ControllerBase
    {
        #region Field
        private readonly IDeliveryOptionService _deliveryOptionService;
        #endregion

        #region Parameterized constructor
        public DeliveryOptionsController(IDeliveryOptionService deliveryOptionService)
        {
            _deliveryOptionService = deliveryOptionService;
        }
        #endregion

        #region Actions to view all deliveryoptions
        /// <summary>
        /// 
        /// </summary>
        /// <returns>view all delivery options</returns>
        [ProducesResponseType(typeof(ViewDeliveryOption), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]
        public async Task<ActionResult<List<ViewDeliveryOption>>> View_All_DeliveryOptions()
        {
            try
            {
                var myDeliveryOptions = await _deliveryOptionService.View_All_DeliveryOptions();
                return Ok(myDeliveryOptions);
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
