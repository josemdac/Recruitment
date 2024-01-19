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
using RenovaHrEmploymentAPI.DTO;

namespace RenovaHrEmploymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoSecurityDefinitionController : ControllerBase
    {

        ILoggerService _logger;
        private ModelContext db;
        public CoSecurityDefinitionController()
        {

            _logger = new LoggerService();
            db = new ModelContext();


        }
        /// <summary>
        /// Get All Position requests
        /// </summary>
        /// <returns>List Of Position Requests</returns>
        [HttpGet("Current")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCurrent()
        {
            var CompanyId = ControllerHelpers.GetCompanyId(this);
            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {

                var response = (from cd in db.CoSecurityDefinitions
                                where cd.CompanyId == CompanyId
                                select cd).FirstOrDefault();
                return Ok(GetDTO(response));
            }
            catch (Exception e)
            {
                return ControllerHelpers.InternalError($"{location}: {e.Message} - {e.InnerException} - {e.StackTrace}");
            }

        }

        private CoSecurityDefinitionDTO GetDTO(CoSecurityDefinition Model)
        {
            var DTO = new CoSecurityDefinitionDTO();
            ControllerHelpers.MapModelDTO(Model, DTO);
            return DTO;
        }
      
    }
}
