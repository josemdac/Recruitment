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
using System.Text.RegularExpressions;
using System.Web;
using System.Net.Http;
using System.Net;
using Swashbuckle.Swagger.Annotations;

namespace RenovaHrEmploymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShareLinkController : ControllerBase
    {

        ILoggerService _logger;
        private ModelContext db;
        public ShareLinkController()
        {

            _logger = new LoggerService();
            db = new ModelContext();


        }
        /// <summary>
        /// Generate Company Token
        /// </summary>
        [HttpGet]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        
        
        public object Generate(string Url, string Token)
        {

            var logger = new LoggerService();
            logger.LogDebug($"Share request to {Url} token = {Token}");
            var HTML = ControllerHelpers.GetRedirectTpl();
            
            var CompanyId = ControllerHelpers.GetCompanyId(Token);
            var iconData = db.HrRcrtCompanyConfigurations.Where(x => x.CompanyId == CompanyId).Select(x => x.Favicon).FirstOrDefault();
            var DataUri = $"data:image/png;base64,{Convert.ToBase64String(iconData)}";
            var Title = GetTitle(Url, CompanyId);

            HTML = HTML.Replace("[ICONURI]", DataUri);
            HTML = HTML.Replace("[TITLE]", Title);
            HTML = HTML.Replace("[REDIRECTURL]", Url);
            //Response.ContentType = "application/xhtml+xml";
            Response.ContentType = "text/html";
            //return Ok(HTML);
            Response.Headers.Add("Accept", "text/html");
            Response.BodyWriter.WriteAsync(Encoding.UTF8.GetBytes(HTML));
            return Ok(HTML);
        }


        private string GetTitle(string Url, decimal CompanyId)
        {
            var siteName = db.HrRcrtCompanyConfigurations.Where(x => x.CompanyId == CompanyId).Select(x => x.SiteName).FirstOrDefault();
            if (Url.Contains("jobposition"))
            {
                var _pid = Url.Split("jobposition/")[1];
                var pid = Convert.ToDecimal(Regex.Replace(_pid, @"[^\d]", ""));
                return siteName + " | "+db.HrRcrtPositionRequests.Where(x => x.RequestId == pid && x.CompanyId == CompanyId).Select(x => x.PositionDescription).FirstOrDefault();
            }

            return siteName;
        }
    }
}
