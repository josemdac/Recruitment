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
using Newtonsoft.Json;

namespace RenovaHrEmploymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyTokenController : ControllerBase
    {

        ILoggerService _logger;
        private ModelContext db;
        
        public CompanyTokenController()
        {

            _logger = new LoggerService();


        }
        /// <summary>
        /// Generate Company Token
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Generate(decimal CompanyId)
        {
            var data = new
            {
                CompanyId,
                Date = DateTime.Now
            };
            return Ok(RenovaCry.Encryption.Encrypt(JsonConvert.SerializeObject(data), ""));
        }
    }
}
