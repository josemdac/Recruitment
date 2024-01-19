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
    public class HrRcrtExternalWebsiteInfoController : ControllerBase
    {

        ILoggerService _logger;
        private ModelContext db;
        public HrRcrtExternalWebsiteInfoController()
        {

            _logger = new LoggerService();
            db = new ModelContext();


        }
        private static int MAXITEMS = 50;
        /// <summary>
        /// Get All Position requests
        /// </summary>
        /// <returns>List Of Position Requests</returns>
        [HttpGet("{PageName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual IActionResult GetInfo(string PageName)
        {
            
            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var CompanyId = ControllerHelpers.GetCompanyId(this);
                var info = db.HrRcrtExternalWebsiteInfos;
                var response = (from p in info
                                where p.CompanyId == CompanyId && p.PageName == PageName
                                select p).FirstOrDefault(); 
                return Ok(GetClean(response));
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
        [HttpPost("SaveConf/{PageName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual IActionResult SaveConf(string PageName, [FromBody] IDictionary<string, object> Data)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var CompanyId = ControllerHelpers.GetCompanyId(this);
                var info = db.HrRcrtExternalWebsiteInfos.Where(x=>CompanyId == x.CompanyId && PageName.Equals(x.PageName)).FirstOrDefault();
                if(info is null)
                {
                    info = new HrRcrtExternalWebsiteInfo
                    {
                        CompanyId = CompanyId,
                        PageName = PageName
                    };
                }

                foreach(var Prop in info.GetType().GetProperties())
                {
                    try
                    {
                        
                        if(Data.ContainsKey(Prop.Name))
                        {
                            Prop.SetValue(info, Data[Prop.Name]);
                        }
                    }catch(Exception e)
                    {

                    }
                }

                if(info.ConfigurationId==0)
                {
                    db.HrRcrtExternalWebsiteInfos.Add(info);
                }
                else
                {
                    db.HrRcrtExternalWebsiteInfos.Update(info);
                }
                
                return Ok(new { Saved = db.SaveChanges() > 0 });
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
        [HttpGet("Tm")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual IActionResult GetTimer()
        {


            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var CompanyId = ControllerHelpers.GetCompanyId(this);
                var info = db.CoCompanyMasters;
                var TimeOutMinutes = (from p in info
                                where p.CompanyId == CompanyId
                                select p.TimeoutMinutes).FirstOrDefault();
                return Ok(new { TimeOutMinutes});
            }
            catch (Exception e)
            {
                return ControllerHelpers.InternalError($"{location}: {e.Message} - {e.InnerException} - {e.StackTrace}");
            }

        }


        private HrRcrtExternalWebsiteInfo GetClean(HrRcrtExternalWebsiteInfo rcrt)
        {
            if(rcrt == null)
            {
                return null;
            }
            rcrt.Company = null;
            rcrt.CompanyId = 0;
            rcrt.ConfigurationId = 0;
            return rcrt;
        }
    }
}
