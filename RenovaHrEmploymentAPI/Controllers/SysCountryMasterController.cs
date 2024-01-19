using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using RenovaHrEmploymentAPI.Contracts;
using RenovaHrEmploymentAPI.Services;
using System.Linq;

namespace RenovaHrEmploymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SysCountryMasterController : ControllerBase
    {

        ILoggerService _logger;
        private ModelContext db;
        public SysCountryMasterController()
        {

            _logger = new LoggerService();
            db = new ModelContext();


        }
        /// <summary>
        /// Get All Position requests
        /// </summary>
        /// <returns>List Of Position Requests</returns>
        [HttpGet("{CountryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<IActionResult> GetItem(decimal CountryId)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {

                var response = (from zc in db.SysCountryMasters
                                where zc.CountryId == CountryId
                                select new
                                {
                                    zc.CountryCode,
                                    zc.CountryName,
                                    zc.CountryId
                                }).FirstOrDefault();
                return Ok(response);
            }
            catch (Exception e)
            {
                return ControllerHelpers.InternalError($"{location}: {e.Message} - {e.InnerException} - {e.StackTrace}");
            }

        }
        /// <summary>
        /// Get All Position requests
        /// </summary>
        /// <returns>List Of Position Requests</returns>
        [HttpGet("Dropdown")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetReference()
        {
         
            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {

                var response = (from zc in db.SysCountryMasters
                                select new
                                {
                                    Value = zc.CountryId,
                                    Text = zc.CountryName


                                }).ToList();
                return Ok(response);
            }
            catch (Exception e)
            {
                return ControllerHelpers.InternalError($"{location}: {e.Message} - {e.InnerException} - {e.StackTrace}");
            }

        }
    }
}
