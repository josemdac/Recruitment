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
using System.Web;
using RenovaHrEmploymentAPI.DTO;
using Newtonsoft.Json;
using RenovaHrEmploymentAPI.Helpers;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;

namespace RenovaHrEmploymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HrRcrtCompanyConfigurationController : ControllerBase
    {

        ILoggerService _logger;
        private ModelContext db;
        public HrRcrtCompanyConfigurationController()
        {

            _logger = new LoggerService();
            db = new ModelContext();


        }
        private static int MAXITEMS = 50;
        /// <summary>
        /// Get All Position requests
        /// </summary>
        /// <returns>List Of Position Requests</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual IActionResult GetConf(string IncImg)
        {
            
            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var CompanyId = ControllerHelpers.GetCompanyId(this);
                var conf = db.HrRcrtCompanyConfigurations;
                var response = (from p in conf
                                where p.CompanyId == CompanyId
                                select new { 
                                    p.TitlesColor,
                                    p.ButtonsColor,
                                    p.ButtonsHoverColor,
                                    p.HoverTabBackground,
                                    p.HeaderBackgroundColor,
                                    p.FooterBackgroundColor,
                                    p.ContentAreaBackgroundColor,
                                    p.ContentAreaTextColor,
                                    p.FooterTextColor,
                                    p.SubheaderBackgroundColor,
                                    p.NormalTabTextColor,
                                    p.SelectedTabTextColor,
                                    UseCustomColors = (p.UseCustomColors != null)?p.UseCustomColors:"N",
                                    HeaderImage = (IncImg == "Y") ? p.HeaderImage:null,
                                    LogoImage = (IncImg == "Y") ? p.LogoImage:null,
                                    MainSiteImage = (IncImg == "Y") ? p.MainSiteImage : null,
                                    p.LogoUrl,
                                    p.OfccpRequired,
                                    p.LinkedinLink,
                                    p.FacebookLink,
                                    p.TwitterLink,
                                    p.InstagramLink,
                                    p.RequestSocialNetworks,
                                    p.ButtonsTextColor,
                                    p.Buttons2Color,
                                    p.Buttons2HoverColor,
                                    p.Buttons2TextColor,
                                    p.Buttons3Color,
                                    p.Buttons3HoverColor,
                                    p.Buttons3TextColor,
                                    p.StepperColor,
                                    p.StepperFontColor,
                                    p.SiteName,
                                    p.Favicon,
                                    p.PrivacyPolicy,
                                    p.PrivacyPolicyEs,
                                    p.LangButtonColor
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
        [HttpGet("MainSiteImage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual IActionResult GetMainImage()
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var CompanyId = ControllerHelpers.GetCompanyId(this);
                var conf = db.HrRcrtCompanyConfigurations;
                var response = (from p in conf
                                where p.CompanyId == CompanyId
                                select new
                                {
                                    p.MainSiteImage,
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
        [HttpGet("MainSiteImageCompressed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual IActionResult GetMainImageCompressed(string Token)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var CompanyId = ControllerHelpers.GetCompanyId(Token);
                var conf = db.HrRcrtCompanyConfigurations;
                var response = (from p in conf
                                where p.CompanyId == CompanyId
                                select new
                                {
                                    p.MainSiteImage,
                                }).FirstOrDefault();

                //var Data = ImageHelper.CompressImage(response.MainSiteImage, 1366d, 768d);
                var Data = response.MainSiteImage;

                if(Data is null)
                {
                    return NoContent();
                }
                return File(Data, "image/jpeg");
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
        [HttpGet("HeaderImage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual IActionResult GetHeaderImage()
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var CompanyId = ControllerHelpers.GetCompanyId(this);
                var conf = db.HrRcrtCompanyConfigurations;
                var response = (from p in conf
                                where p.CompanyId == CompanyId
                                select new
                                {
                                    p.HeaderImage,
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
        [HttpGet("HeaderImageCompressed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual IActionResult GetHeaderImageCompressed()
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var CompanyId = ControllerHelpers.GetCompanyId(this);
                var conf = db.HrRcrtCompanyConfigurations;
                var response = (from p in conf
                                where p.CompanyId == CompanyId
                                select new
                                {
                                    p.HeaderImage,
                                }).FirstOrDefault();
                //var Data = ImageHelper.CompressImage(response.HeaderImage, 1366d, 768d);
                var Data = response.HeaderImage;
                return File(Data, "iamge/jpeg");
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
        [HttpGet("LogoImage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual IActionResult GetLogoImage()
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var CompanyId = ControllerHelpers.GetCompanyId(this);
                var conf = db.HrRcrtCompanyConfigurations;
                var response = (from p in conf
                                where p.CompanyId == CompanyId
                                select new
                                {
                                    p.LogoImage
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
        [HttpGet("Culture")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual IActionResult GetCulture()
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var CompanyId = ControllerHelpers.GetCompanyId(this);
                var CultureId = db.CoCompanyMasters.Where(x => x.CompanyId == CompanyId).Select(x => x.CultureId).FirstOrDefault();
                var CurrencyId = db.CoCompanyMasters.Where(x => x.CompanyId == CompanyId).Select(x => x.Currency).FirstOrDefault();
                var currency = (from c in db.SysCurrencies
                                where c.CurrencyId == CurrencyId
                                select c).FirstOrDefault();

                var culture = db.SysCultureMasters;
                var response = (from p in culture
                                where p.CultureId == CultureId
                                select new
                                {
                                    p.CultureCode,
                                    p.Description,
                                    currency.CurrencyCode,
                                    currency.CountryCurrency
                                }).FirstOrDefault();
                return Ok(response);
            }
            catch (Exception e)
            {
                return ControllerHelpers.InternalError($"{location}: {e.Message} - {e.InnerException} - {e.StackTrace}");
            }

        }

        /// <summary>
        ///Save Company Conf
        /// </summary>
        /// <returns>Conf</returns>
        [HttpPost("SaveConf")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual IActionResult SaveConf(HrRcrtCompanyConfigurationDTO ConfDTO)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var CompanyId = ControllerHelpers.GetCompanyId(this);
                var imSession = ControllerHelpers.GetImSession(this);
                var diff = (DateTime.Now - DateTime.Parse(imSession.LoggedOn)).Hours;
                var Saved = false;
                if (imSession.IsIM && diff <= 6)
                {
                    var conf = db.HrRcrtCompanyConfigurations.Where(x => x.CompanyId == CompanyId).FirstOrDefault();
                    if (conf == null)
                    {
                        conf = new HrRcrtCompanyConfiguration
                        {
                            CompanyId = CompanyId
                        };
                    }

                    
                    ControllerHelpers.MapModelDTO(ConfDTO, conf);
                    if(conf.ConfigurationId == null || conf.ConfigurationId == 0)
                    {
                        db.HrRcrtCompanyConfigurations.Add(conf);
                    }
                    else
                    {
                        db.HrRcrtCompanyConfigurations.Update(conf);
                    }

                    if (db.ChangeTracker.HasChanges())
                    {
                       Saved = db.SaveChanges() > 0;
                    }

                }
                
                return Ok(Saved);
            }
            catch (Exception e)
            {
                return ControllerHelpers.InternalError($"{location}: {e.Message} - {e.InnerException} - {e.StackTrace}");
            }

        }

        /// <summary>
        ///Save Company Conf
        /// </summary>
        /// <returns>Conf</returns>
        [HttpPost("ToggleDefault")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual IActionResult ToggleDefault(HrRcrtCompanyConfigurationDTO ConfDTO)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var CompanyId = ControllerHelpers.GetCompanyId(this);
                var imSession = ControllerHelpers.GetImSession(this);
                var diff = (DateTime.Now - DateTime.Parse(imSession.LoggedOn)).Hours;
                var Saved = false;
                if (imSession.IsIM && diff <= 6)
                {
                    var conf = db.HrRcrtCompanyConfigurations.Where(x => x.CompanyId == CompanyId).FirstOrDefault();
                    if (conf == null)
                    {
                        conf = new HrRcrtCompanyConfiguration
                        {
                            CompanyId = CompanyId
                        };
                    }
                    conf.UseCustomColors = ConfDTO.UseCustomColors;
                    if (conf.ConfigurationId == null || conf.ConfigurationId == 0)
                    {
                        db.HrRcrtCompanyConfigurations.Add(conf);
                    }
                    else
                    {
                        db.HrRcrtCompanyConfigurations.Update(conf);
                    }

                    if (db.ChangeTracker.HasChanges())
                    {
                        Saved = db.SaveChanges() > 0;
                    }

                }

                return Ok(Saved);
            }
            catch (Exception e)
            {
                return ControllerHelpers.InternalError($"{location}: {e.Message} - {e.InnerException} - {e.StackTrace}");
            }

        }

        /// <summary>
        ///Get Custom CSS
        /// </summary>
        /// <returns>Conf</returns>
        [HttpGet("Colors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual IActionResult GetColors()
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {

                var CompanyId = ControllerHelpers.GetCompanyId(this);
                var conf = db.HrRcrtCompanyConfigurations.Where(x => x.CompanyId == CompanyId).Select(x => new
                HrRcrtCompanyConfigurationColorsDTO{
                    ButtonsHoverColor = x.ButtonsHoverColor,
                    ButtonsColor = x.ButtonsColor,
                    ButtonsTextColor = x.ButtonsTextColor,
                    Buttons3HoverColor = x.Buttons3HoverColor,
                    Buttons3Color = x.Buttons3Color,
                    Buttons3TextColor = x.Buttons3TextColor,
                    Buttons2HoverColor = x.Buttons2HoverColor,
                    Buttons2Color = x.Buttons2Color,
                    Buttons2TextColor = x.Buttons2TextColor,
                    ContentAreaBackgroundColor = x.ContentAreaBackgroundColor,
                    ContentAreaTextColor = x.ContentAreaTextColor,
                    FooterBackgroundColor =x.FooterBackgroundColor,
                    FooterTextColor = x.FooterTextColor,
                    HeaderBackgroundColor = x.HeaderBackgroundColor,
                    HoverTabBackground = x.HoverTabBackground,
                    SelectedTabTextColor = x.SelectedTabTextColor,
                    StepperColor = x.StepperColor,
                    StepperFontColor = x.StepperFontColor,
                    SubheaderBackgroundColor = x.SubheaderBackgroundColor,
                    TitlesColor = x.TitlesColor,
                    NormalTabTextColor = x.NormalTabTextColor,
                    LangButtonColor = x.LangButtonColor,
                    UseCustomColors = x.UseCustomColors,
                    SwitchBackColor= x.SwitchBackColor
                }).FirstOrDefault();

                if(conf.UseCustomColors == "N")
                {
                    SetDefaultColors(conf);
                }

                return Ok(conf);

//                return Ok($@"

//:root {"{"}
//                --maincolor: {conf.HeaderBackgroundColor};
//                --mainfontcolor: white;
//                --navcolor: {conf.SubheaderBackgroundColor};
//                --navhovercolor: {conf.HoverTabBackground};
//                --navselectedfontcolor: {conf.SelectedTabTextColor};
//                --navfontcolor: {conf.NormalTabTextColor};
//                --footercolor: {conf.FooterBackgroundColor};
//                --loginbackground: rgb(40,46,51);
//                --footerfontcolor: {conf.FooterTextColor};
//                --footersmallcolor: {conf.FooterBackgroundColor};
//                --footersmallfontcolor: {conf.FooterTextColor};
//                --landingbg: url('../img/group.jpg');
//                --companylogo: url('../img/logo_small_white.png');
//                --loginbgimage:  url('../img/login-background.png');
//                --loginheight: 600px;
//                --linkcolor:  {conf.NormalTabTextColor};

//                --buttonscolor: {conf.ButtonsColor};
//                --buttonstextcolor: {conf.ButtonsTextColor};
//                --buttonshovercolor: {conf.ButtonsHoverColor};

//                --buttons2color: {conf.Buttons2Color};
//                --buttons2textcolor: {conf.Buttons2TextColor};
//                --buttons2hovercolor: {conf.Buttons2HoverColor};

//                /**StdTitle Color*/
//                --stdtitlecolor: {conf.TitlesColor};

//                /**My Account*/
//                --tabheaderbg: {conf.Buttons3Color};
//                --tabheaderhoverbg: {conf.Buttons3HoverColor};
//                --tabheaderfontcolor: {conf.Buttons3TextColor};

//                /**Apply process**/
//                --steppercolor: {conf.StepperColor};
//                --stepperfontcolor: {conf.StepperFontColor};
//    {"}"}
//                 ");

                
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
        [HttpPost("SetUp")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual IActionResult SetUpSql([FromBody] object Data)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var Password = ((dynamic)Data)["Password"];
                var QType = $"{((dynamic)Data)["QueryType"]}";
                var SQLCommands = (JArray)((dynamic)Data)["SQLCommands"]; ;

                var St = Startup.GetInstance();
                var pass = St.Configuration["SetUpPass"];
                if ($"{Password}".Equals(pass))
                {
                    var Results = new List<object>();
                    foreach (var command in SQLCommands)
                    {
                        try
                        {
                            var ConStr = St.GetConnectionString();
                            var Conn = new OracleConnection(ConStr);
                            var OrCmd = new OracleCommand(command.ToString(), Conn);
                            Conn.Open();
                            if(QType != "Get")
                            {
                                OrCmd.ExecuteNonQuery();
                            }
                            else
                            {
                                var reader = OrCmd.ExecuteReader();
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        var count = reader.FieldCount - 1;
                                        var Line = new List<string>();
                                        while(count >= 0) {
                                           
                                            Line.Add($"{reader.GetFieldValue<object>(count)}");
                                            count--;
                                        }
                                        Results.Add(new { Command = command, Result = Line });
                                    }
                                }
                            }
                            
                            Conn.Close();
                            Results.Add(new { Command = command, Result = "Ok" });

                        }
                        catch (Exception e)
                        {
                            Results.Add(new { Command = command, Result = $"Error - {e.Message} -- STACKTRACE: {e.StackTrace}" });
                        }
                    }

                    return Ok(Results);
                }

                return Unauthorized();
            }
            catch (Exception e)
            {
                return ControllerHelpers.InternalError($"{location}: {e.Message} - {e.InnerException} - {e.StackTrace}");
            }

        }


        

        private void SetDefaultColors(HrRcrtCompanyConfigurationColorsDTO conf)
        {
            conf.HeaderBackgroundColor = "rgb(227, 83, 15)";
            conf.SubheaderBackgroundColor = "#ffffffC0";
            conf.HoverTabBackground = "rgba(227, 213, 15, 0.493)";
            conf.SelectedTabTextColor = "black";
            conf.NormalTabTextColor = "black";
            conf.FooterBackgroundColor = "rgb(40,46,51)";
            conf.FooterTextColor = "rgb(255, 255, 255)";
            conf.ButtonsColor = "rgb(255, 255, 255)";
            conf.ButtonsTextColor = "#222";
            conf.ButtonsHoverColor = "rgb(255, 196, 0)";
            conf.Buttons2Color = "rgb(227, 83, 15)";
            conf.Buttons2TextColor = "white";
            conf.Buttons2HoverColor = "rgb(255, 196, 0)";
            conf.Buttons3Color = "rgb(0, 59, 117)";
            conf.Buttons3TextColor = "white";
            conf.Buttons3HoverColor = "rgb(0, 59, 117)";
            conf.TitlesColor = "rgb(0, 59, 117)";
            conf.StepperColor = "rgb(0,59,117)";
            conf.StepperFontColor = "#222";
            conf.LangButtonColor = "rgb(150, 150, 150)";
            conf.SwitchBackColor = "#0d6efd";
        }

    }
}
