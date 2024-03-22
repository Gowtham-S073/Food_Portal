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
    public class OrdersController : ControllerBase
    {
        #region Field
        private readonly IOrderService _orderService;
        #endregion

        #region Parameterized constructor
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        #endregion

        #region Actions to add order
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Order"></param>
        /// <returns>add order</returns>
        [ProducesResponseType(typeof(RequestOrder), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        public async Task<ActionResult<RequestOrder>> Add_Order(RequestOrder Order)
        {
            try
            {

                var myOrder = await _orderService.Add_Order(Order);
                return Created("Order created Successfully", myOrder);
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

        #region Actions to get unavailable date
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns>get unavailable date</returns>
        [ProducesResponseType(typeof(AvailabilityDTO), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet("UnAvailableDate{date}")]
        public async Task<ActionResult<List<AvailabilityDTO>>> GetUnAvailableDate(DateTime date)
        {
            var availableDate = await _orderService.GetUnAvailableDate(date);
            return Ok(availableDate);
        }
        #endregion

        #region Actions to get available time slot
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns>get available time slot</returns>
        [ProducesResponseType(typeof(AvailabilityDTO), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet("AvailableTimeSlot{date}")]

        public async Task<ActionResult<List<AvailabilityDTO>>> GetAvailableTimeSlot(DateTime date)
        {
            var availableDate = await _orderService.GetAvailableTimeSlot(date);
            return Ok(availableDate);
        }
        #endregion
    }
}
