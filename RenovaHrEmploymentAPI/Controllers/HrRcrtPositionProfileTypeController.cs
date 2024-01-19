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
    public class HrRcrtPositionProfileTypeController : ControllerBase
    {

        ILoggerService _logger;
        private ModelContext db;
        public HrRcrtPositionProfileTypeController()
        {

            _logger = new LoggerService();
            db = new ModelContext();


        }
        /// <summary>
        /// Get All Position requests
        /// </summary>
        /// <returns>List Of Position Requests</returns>
        [HttpGet("{TypeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<IActionResult> GetType(decimal TypeId)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var CompanyId = ControllerHelpers.GetCompanyId(this);
                var response = (from zc in db.HrRcrtPositionProfileTypes
                                where zc.TypeId == TypeId && zc.CompanyId == CompanyId
                                select new
                                {
                                    zc.SpanishDescription,
                                    zc.EnglishDescription,
                                    zc.ProfileType,
                                    zc.TypeId
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
        public IActionResult GetReference(string Type)
        {
         
            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var CompanyId = ControllerHelpers.GetCompanyId(this);
                var response = (from zc in db.HrRcrtPositionProfileTypes
                                where zc.CompanyId == CompanyId && zc.ProfileType == Type
                                select new
                                {
                                    Value = zc.TypeId,
                                    Text = zc.ProfileType,
                                    zc.EnglishDescription,
                                    zc.SpanishDescription


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
