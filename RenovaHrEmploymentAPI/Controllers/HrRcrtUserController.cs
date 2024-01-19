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
using System.Net.Mail;
using Newtonsoft.Json;
using System.Web;
using RenovaHrEmploymentAPI.Helpers;

namespace RenovaHrEmploymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HrRcrtUserController : ControllerBase
    {

        ILoggerService _logger;
        private ModelContext db;
        public HrRcrtUserController()
        {

            _logger = new LoggerService();
            db = new ModelContext();


        }
        private static int MAXITEMS = 50;

        /// <summary>
        /// Get All Position requests
        /// </summary>
        /// <returns>List Of Position Requests</returns>
        [HttpPost("ValidatePassword")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCurrentUser(HrRcrtUserPassValidateDTO pass)
        {
            var Password = pass.Password;

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var encPass = RenovaCry.Encryption.Encrypt(Password, "");
                var uid = ControllerHelpers.GetUserId(this);
                var user = db.HrRcrtUsers.Where(u => u.Password == encPass && u.UserId == uid).Any();


                return Ok(new { Valid = user});
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
        [HttpGet("ValidateUser/{UserName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCurrentUser(string UserName)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var CompanyId = ControllerHelpers.GetCompanyId(this);
                var Valid = !db.HrRcrtUsers.Where(u => u.UserName.Trim().ToUpper() == UserName.Trim().ToUpper() && u.CompanyId == CompanyId).Any();


                return Ok(new { Valid });
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
        [HttpGet("Current")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCurrentUser()
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var uid = ControllerHelpers.GetUserId(this);
                var user = db.HrRcrtUsers.Where(u => u.UserId == uid).FirstOrDefault();
                

                return Ok(MapData(user));
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
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(HrRcrtUserUpdateDTO userDTO)
        {
          
            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var uid = ControllerHelpers.GetUserId(this);
                var user = db.HrRcrtUsers.Where(u => u.UserId == uid).FirstOrDefault();
                SafelyUpdate(user, userDTO);
                db.HrRcrtUsers.Update(user);
                if (db.ChangeTracker.HasChanges())
                {
                    db.SaveChanges();
                }
                return Ok(user);
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
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Create(HrRcrtUserCreateDTO userDTO, string Lang)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var user = new HrRcrtUser();
                SafelyCreate(user, userDTO);

                user.Password = RenovaCry.Encryption.Encrypt(userDTO.Password, "");
                user.CompanyId = ControllerHelpers.GetCompanyId(this);
                user.Status = "I";

                db.HrRcrtUsers.Add(user);
                var Created = false;
                if (db.ChangeTracker.HasChanges())
                {
                    Created = db.SaveChanges() > 0;
                }

                if (Created)
                {
                    SendActivationEmail(user, Lang);
                }
                return Ok(new { Created });
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
        [HttpPost("Resend")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Resend(HrRcrtUserCreateDTO userDTO, string Lang)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var CompanyId = ControllerHelpers.GetCompanyId(this);
                var user = db.HrRcrtUsers.Where(u => u.UserName == userDTO.UserName && u.CompanyId == CompanyId).FirstOrDefault();
                
                if(user == null)
                {
                    return BadRequest("InvalidUser");
                }
                user.InactiveDate = DateTime.Now;
                user.Status = "I";

                db.HrRcrtUsers.Update(user);
                db.SaveChanges();

                SendActivationEmail(user, Lang);
                
                return Ok(new { Sent = true });
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
        [HttpPost("Activate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Activate()
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {

                var Token = ControllerHelpers.GetActivationToken(this);

                if (IsResetPasswordToken(Token))
                {

                    return Ok(new { ResetPassword = true, ValidToken = ValidResetToken(Token) });
                }
                var user = DecodeToken(Token);


                if (user == null)
                {
                    return BadRequest(new { Error = "InvalidUser", Activated = false });
                }
                user.InactiveDate = null;
                user.Status = "A";

                db.HrRcrtUsers.Update(user);
                db.SaveChanges();


                return Ok(new { Activated = true, Error= "" });
            }
            catch (Exception e)
            {
                ControllerHelpers.InternalError($"{location}: {e.Message} - {e.InnerException} - {e.StackTrace}");
                return BadRequest(new { Error = e.Message.Replace(" ", ""), Activated = false });
            }

        }

        /// <summary>
        /// Get All Position requests
        /// </summary>
        /// <returns>List Of Position Requests</returns>
        [HttpPost("ResetPassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ResetPassword(HrRcrtUserResetPasswordDTO userDTO, string Lang, string State)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var Token = ControllerHelpers.GetActivationToken(this);


                if ("reset".Equals(State))
                {

                    if (!ValidResetToken(Token))
                    {
                        return Ok(new { InvalidToken = true });
                    }
                    if (IsResetPasswordToken(Token) 
                        && userDTO.Password != null
                        && userDTO.ConfirmPassword == userDTO.Password)
                    {
                        var Password = RenovaCry.Encryption.Encrypt(userDTO.Password, "");
                        var User = DecodeToken(Token);
                        User.Password = Password;
                        User.LastPasswordChange = DateTime.Now;
                        var db = new ModelContext();
                        db.HrRcrtUsers.Update(User);
                        db.SaveChanges();

                        return Ok(new { ResetSuccess = true });


                    }
                    return BadRequest();

                }



                var user = db.HrRcrtUsers.Where(u => u.UserName == userDTO.UserName).FirstOrDefault();
                SendResetPasswordEmail(user, Lang);
                
                return Ok(new { Sent = true });
            }
            catch (Exception e)
            {
                ControllerHelpers.InternalError($"{location}: {e.Message} - {e.InnerException} - {e.StackTrace}");
                return BadRequest(new { Error = e.Message.Replace(" ", ""), Activated = false, Sent = false });
            }

        }

        /// <summary>
        /// Get All Position requests
        /// </summary>
        /// <returns>List Of Position Requests</returns>
        [HttpPut("ChangePassword")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ChangePassword(HrRcrtUserPassChangeDTO userDTO)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var uid = ControllerHelpers.GetUserId(this);
                var user = db.HrRcrtUsers.Where(u => u.UserId == uid).FirstOrDefault();
                if(RenovaCry.Encryption.Encrypt(userDTO.OldPassword, "") != user.Password)
                {
                    return BadRequest("WrongPassword");
                }

                user.Password = RenovaCry.Encryption.Encrypt(userDTO.NewPassword, "");
                db.HrRcrtUsers.Update(user);
                var Changed = false;
                if (db.ChangeTracker.HasChanges())
                {
                   Changed =  db.SaveChanges() > 0;
                }
                return Ok(new { Changed });
            }
            catch (Exception e)
            {
                return ControllerHelpers.InternalError($"{location}: {e.Message} - {e.InnerException} - {e.StackTrace}");
            }

        }

        private bool IsResetPasswordToken(string Token)
        {
            var data = ControllerHelpers.DecryptActivation(Token);
            var Act = JsonConvert.DeserializeObject<HrRcrtUserActivation>(data);
            return Act.ActType.Equals("PasswordReset");
        }

        private bool ValidResetToken(string Token)
        {
            var data = ControllerHelpers.DecryptActivation(Token);
            var Act = JsonConvert.DeserializeObject<HrRcrtUserActivation>(data);
            var User = DecodeToken(Token);
            return Act.ActType == "PasswordReset"
                && (User.LastPasswordChange == null || 
                    ((User.LastPasswordChange != null) && 
                    (Act.Created > User.LastPasswordChange.Value.ToUniversalTime())));

        }


        private HrRcrtUser DecodeToken(string Token)
        {
            var data = ControllerHelpers.DecryptActivation(Token);
            var Act = JsonConvert.DeserializeObject<HrRcrtUserActivation>(data);
            if((DateTime.Now - Act.Created).Hours > 24)
            {
                throw new Exception("ExpiredToken");
            }
            var user = db.HrRcrtUsers.Where(u => u.UserId == Act.UserId).FirstOrDefault();
            return user;
        }

        private void SafelyUpdate(HrRcrtUser User, HrRcrtUserUpdateDTO DTO)
        {
            var props = DTO.GetType().GetProperties();
            foreach(var prop in props)
            {
                User.GetType().GetProperty(prop.Name).SetValue(User, prop.GetValue(DTO));
            }

        }

        private void SafelyCreate(HrRcrtUser User, HrRcrtUserCreateDTO DTO)
        {
            var props = DTO.GetType().GetProperties();
            foreach (var prop in props)
            {
                User.GetType().GetProperty(prop.Name).SetValue(User, prop.GetValue(DTO));
            }
            User.CreationDate = DateTime.Now;
            User.InactiveDate = DateTime.Now;
        }

        private void SendActivationEmail(HrRcrtUser User, string Lang)
        {
            var CompanyId = ControllerHelpers.GetCompanyId(this);
            var reportsService = db.CoReportsServices.Where(x => x.CompanyId == CompanyId).FirstOrDefault(); 
            if(reportsService != null)
            {
                var Message = BuildUserActivationMessage(User, Lang);
                ControllerHelpers.SendEmail(reportsService, Message);
                
            }

        }

        private void SendResetPasswordEmail(HrRcrtUser User, string Lang)
        {
            var CompanyId = ControllerHelpers.GetCompanyId(this);
            var reportsService = db.CoReportsServices.Where(x => x.CompanyId == CompanyId).FirstOrDefault();
            if (reportsService != null)
            {
                var Message = BuildResetPasswordMessage(User, Lang);
                ControllerHelpers.SendEmail(reportsService, Message);
            }

        }


        private string GenerateActivationLink(HrRcrtUser user, string ActType="Activate")
        {
            var data = new HrRcrtUserActivation
            {
                Created = DateTime.Now.ToUniversalTime(),
                UserId = user.UserId,
                ActType = ActType

            };

            var rawJson = JsonConvert.SerializeObject(data);
            var Route = HttpContext.Request.Headers["ActivateRoute"];
            
            var url = Route + "/" + ControllerHelpers.EncryptActivation(rawJson);
            return url;
        }
        

        private MailMessage BuildUserActivationMessage(HrRcrtUser User, string Lang)
        {
            var CompanyId = ControllerHelpers.GetCompanyId(this);
            var info = db.HrRcrtExternalWebsiteInfos;
            var Tpl = (from p in info
                            where p.CompanyId == CompanyId && p.PageName == "Registration"
                            select p).FirstOrDefault();
            if(Tpl == null)
            {
                Tpl = DefaultTemplateHelper.GetTemplate(this, "Registration");
                db.HrRcrtExternalWebsiteInfos.Add(Tpl);
                db.SaveChanges();
            }
            var Subject = Lang == "en" ? Tpl.Title1English : Tpl.Title1Spanish;
            var Body = Lang == "en" ? Tpl.Text1English : Tpl.Text1Spanish;
            var Link = GenerateActivationLink(User);
            var Message = new MailMessage();
            Message.To.Add(new MailAddress(User.UserName));
            Message.Subject = Subject;
            Message.Body= Body.Replace("[ACTIVATIONLINK]", Link);
            Message.IsBodyHtml = true;
            return Message;
        }

        private MailMessage BuildResetPasswordMessage(HrRcrtUser User, string Lang)
        {
            var CompanyId = ControllerHelpers.GetCompanyId(this);
            var info = db.HrRcrtExternalWebsiteInfos;
            var Tpl = (from p in info
                       where p.CompanyId == CompanyId && p.PageName == "ResetPassword"
                       select p).FirstOrDefault();
            if (Tpl == null)
            {
                Tpl = DefaultTemplateHelper.GetTemplate(this, "ResetPassword");
                db.HrRcrtExternalWebsiteInfos.Add(Tpl);
                db.SaveChanges();
            }
            var Subject = Lang == "en" ? Tpl.Title1English : Tpl.Title1Spanish;
            var Body = Lang == "en" ? Tpl.Text1English : Tpl.Text1Spanish;
            var Link = GenerateActivationLink(User, "PasswordReset");
            var Message = new MailMessage();
            Message.To.Add(new MailAddress(User.UserName));
            Message.Subject = Subject;
            _logger.LogDebug($"Sending Link to {User.UserName}, link = "+Link);
            Message.Body = Body.Replace("[ACTIVATIONLINK]", Link);
            Message.IsBodyHtml = true;
            return Message;
        }

        private HrRcrtUserCurrentDTO MapData(HrRcrtUser User)
        {
            var DTO = new HrRcrtUserCurrentDTO();
            var props = DTO.GetType().GetProperties();
            foreach (var prop in props)
            {
                prop.SetValue(DTO, User.GetType().GetProperty(prop.Name).GetValue(User));
            }

            return DTO;

        }


    }
}
