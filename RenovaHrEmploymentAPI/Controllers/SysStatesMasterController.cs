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
    public class SysStatesMasterController : ControllerBase
    {

        ILoggerService _logger;
        private ModelContext db;
        public SysStatesMasterController()
        {

            _logger = new LoggerService();
            db = new ModelContext();


        }
        private static int MAXITEMS = 50;
        /// <summary>
        /// Get All Position requests
        /// </summary>
        /// <returns>List Of Position Requests</returns>
        [HttpGet("Dropdown")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual IActionResult GetReference()
        {
         
            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                
                var response = (from st in db.SysStatesMasters
                                select new
                                {
                                    Value = st.StateId,
                                    Text = st.Description


                                }).ToList();

                response.Sort(ControllerHelpers.DropdownSort);

                response.Insert(0, new { Value = "NA", Text = "N/A" });
                return Ok(response);
            }
            catch (Exception e)
            {
                return ControllerHelpers.InternalError($"{location}: {e.Message} - {e.InnerException} - {e.StackTrace}");
            }

        }
    }
}
