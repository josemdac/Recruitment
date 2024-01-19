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
    public class SysZipcodesController : ControllerBase
    {

        ILoggerService _logger;
        private ModelContext db;
        public SysZipcodesController()
        {

            _logger = new LoggerService();
            db = new ModelContext();


        }
        /// <summary>
        /// Get All Position requests
        /// </summary>
        /// <returns>List Of Position Requests</returns>
        [HttpGet("{Zipcode}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<IActionResult> GetZipcode(string Zipcode)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {

                var response = (from zc in db.SysZipcodes
                                where zc.Zipcode == Zipcode
                                select new
                                {
                                    zc.State,
                                    zc.City,
                                    zc.Areacode,
                                    zc.Countyansi,
                                    zc.County,
                                    zc.Timezone,
                                    zc.Stateansi,
                                    zc.StateNavigation
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

                var response = (from zc in db.SysZipcodes
                                select new
                                {
                                    Value = zc.Zipcode,
                                    Text = zc.Zipcode


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
