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
using RenovaHrEmploymentAPI.Model;
using System.IO;
using Newtonsoft.Json;

namespace RenovaHrEmploymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SysLanguagesController : ControllerBase
    {

        ILoggerService _logger;
        private ModelContext db;
        public SysLanguagesController()
        {

            _logger = new LoggerService();
            db = new ModelContext();


        }
        /// <summary>
        /// Get All Position requests
        /// </summary>
        /// <returns>List Of Position Requests</returns>
        [HttpGet("{LanguageId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<IActionResult> GetItem(decimal LanguageId)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {

                var response = (from zc in db.SysLanguages
                                where zc.LanguageId == LanguageId
                                select new
                                {
                                    zc.Culture,
                                    zc.Status,
                                    zc.Language,
                                    zc.LanguageId
                                }).FirstOrDefault();
                return Ok(response);
            }
            catch (Exception e)
            {
                return ControllerHelpers.InternalError($"{location}: {e.Message} - {e.InnerException} - {e.StackTrace}");
            }

        }

        delegate string DelText(string culture, string defaultText);
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
                var allLangs = GetISO639Items();
                DelText GetText = delegate (string culture,string defaultText)
                {
                    var lang = allLangs.Where(l => l.culture == culture).FirstOrDefault();
                    if(lang != null)
                    {
                        var name =  lang.nativeName;
                        if(name != lang.name)
                        {
                            name = name + $" ({lang.name})";
                        }

                        return name;
                    }

                    return defaultText;
                };
                var response = (from zc in db.SysLanguages
                                select new
                                {
                                    Value = zc.LanguageId,
                                    Culture = zc.Culture,
                                    Text = zc.Language

                                }).ToList();
                return Ok(response.Select(zc => new {
                    zc.Value,
                    zc.Culture,
                    Text = GetText(zc.Culture.ToLower(),zc.Text)
                }).ToList());
            }
            catch (Exception e)
            {
                return ControllerHelpers.InternalError($"{location}: {e.Message} - {e.InnerException} - {e.StackTrace}");
            }

        }

        public static ICollection<ISO639Item> GetISO639Items()
        {
            return ControllerHelpers.ISOLangs;
        }
    }
}
