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
    public class HrEducationDegreeController : ControllerBase
    {

        ILoggerService _logger;
        private ModelContext db;
        public HrEducationDegreeController()
        {

            _logger = new LoggerService();
            db = new ModelContext();


        }
        /// <summary>
        /// Get All Position requests
        /// </summary>
        /// <returns>List Of Position Requests</returns>
        [HttpGet("{DegreeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<IActionResult> GetItem(decimal DegreeId)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {

                var CompanyId = ControllerHelpers.GetCompanyId(this);
                var response = (from zc in db.HrEducationDegrees
                                where zc.DegreeId == DegreeId
                                && zc.CompanyId == CompanyId
                                select new
                                {
                                    zc.Description,
                                    zc.DegreeId
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
                var CompanyId = ControllerHelpers.GetCompanyId(this);
                var response = (from zc in db.HrEducationDegrees
                                where zc.CompanyId == CompanyId
                                select new
                                {
                                    Value = zc.DegreeId,
                                    Text = zc.Description


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
