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
#nullable disable


namespace FoodPortal.Controllers
{
    [Authorize(Roles = "user")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TrackStatusController : ControllerBase
    {
        #region Field
        private readonly ITrackStatusService _trackStatusService;
        #endregion

        #region Parameterized constructor
        public TrackStatusController(ITrackStatusService trackStatusService)
        {
            _trackStatusService = trackStatusService;
        }
        #endregion

        #region Actions to add track status
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TrackStatus"></param>
        /// <returns>add track status</returns>
        [ProducesResponseType(typeof(RequestTrackStatus), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        public async Task<ActionResult<RequestTrackStatus>> Add_TrackStatus(RequestTrackStatus TrackStatus)
        {
            try
            {

                var myTrackStatus = await _trackStatusService.Add_TrackStatus(TrackStatus);
                return Created("TrackStatus created Successfully", myTrackStatus);
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

        #region Actions to get order summary
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>get order summary</returns>
        [ProducesResponseType(typeof(TrackDTO), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost("TrackId")]
        public async Task<ActionResult<TrackDTO>> Get_Order_Summary(IdDTO id)
        {
            try
            {
                var myTrackStatuss = await _trackStatusService.Get_Order_Summary(id.IdString);
                return Ok(myTrackStatuss);
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

        #region Actions to check track id
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        /// <returns>check track id</returns>
        [ProducesResponseType(typeof(IdDTO), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost("checktrackid")]
        public async Task<ActionResult<IdDTO>> check_trackid(IdDTO id, string username)
        {
            try
            {
                var myTrackStatuss = await _trackStatusService.check_trackid(id, username);
                return Ok(myTrackStatuss);
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
