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
using FoodPortal.ViewModel;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace FoodPortal.Controllers
{
    [Authorize(Roles = "user")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TimeSlotsController : ControllerBase
    {
        #region Field
        private readonly ITimeSlotService _timeSlotService;
        #endregion

        #region Parameterized constructor
        public TimeSlotsController(ITimeSlotService timeSlotService)
        {
            _timeSlotService = timeSlotService;
        }
        #endregion

        #region Actions to view all time slots
        /// <summary>
        /// 
        /// </summary>
        /// <returns>view all time slots</returns>
        [ProducesResponseType(typeof(ViewTimeSlot), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]
        public async Task<ActionResult<List<ViewTimeSlot>>> View_All_TimeSlots()
        {
            try
            {
                var myTimeSlots = await _timeSlotService.View_All_TimeSlots();
                return Ok(myTimeSlots);
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
