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
using RenovaHrEmploymentAPI.Helpers;

namespace RenovaHrEmploymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HrRcrtApplicantController : ControllerBase
    {

        ILoggerService _logger;
        private ModelContext db;
        public HrRcrtApplicantController()
        {

            _logger = new LoggerService();
            db = new ModelContext();


        }

        /// <summary>
        /// Get All Documents
        /// </summary>
        /// <returns>List Documents</returns>
        [HttpGet("{ApplicantId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetDoc(decimal ApplicantId)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var uid = ControllerHelpers.GetUserId(this);
                var UserName = db.HrRcrtUsers.Where(u => u.UserId == uid).Select(u => u.UserName).FirstOrDefault();
                var doc = db.HrRcrtApplicants.Where(u => u.UserName == UserName && u.ApplicantId == ApplicantId).FirstOrDefault();

                var resp = new HrRcrtApplicantUpdateDTO();
                ControllerHelpers.MapModelDTO(doc, resp);
                return Ok(resp);
            }
            catch (Exception e)
            {
                return ControllerHelpers.InternalError($"{location}: {e.Message} - {e.InnerException} - {e.StackTrace}");
            }

        }

        /// <summary>
        /// Get All Documents
        /// </summary>
        /// <returns>List Documents</returns>
        [HttpGet("ByRequest/{RequestId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByreq(decimal RequestId)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var CompanyId = ControllerHelpers.GetCompanyId(this);
                var uid = ControllerHelpers.GetUserId(this);
                var UserName = db.HrRcrtUsers.Where(u => u.UserId == uid && u.CompanyId == CompanyId).Select(u => u.UserName).FirstOrDefault();
                var doc = db.HrRcrtApplicants.Where(u => u.UserName == UserName && u.RequestId == RequestId).FirstOrDefault();

                var resp = new HrRcrtApplicantUpdateDTO();
                if(doc!= null)
                {
                    ControllerHelpers.MapModelDTO(doc, resp);
                }
                
                return Ok(resp);
            }
            catch (Exception e)
            {
                return ControllerHelpers.InternalError($"{location}: {e.Message} - {e.InnerException} - {e.StackTrace}");
            }

        }

        /// <summary>
        /// Get All Documents
        /// </summary>
        /// <returns>List Documents</returns>
        [HttpDelete("{ApplicantId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DelDoc(decimal ApplicantId)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var uid = ControllerHelpers.GetUserId(this);
                var UserName = db.HrRcrtUsers.Where(u => u.UserId == uid).Select(u => u.UserName).FirstOrDefault();
                var doc = db.HrRcrtApplicants.Where(u => u.UserName == UserName && u.ApplicantId == ApplicantId).FirstOrDefault();
                db.HrRcrtApplicants.Remove(doc);
                var save = db.SaveChanges();
                
                return Ok(new { Deleted = save });
            }
            catch (Exception e)
            {
                return ControllerHelpers.InternalError($"{location}: {e.Message} - {e.InnerException} - {e.StackTrace}");
            }

        }

        /// <summary>
        /// Get All Documents
        /// </summary>
        /// <returns>List Documents</returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCurrentUserApplications()
        {
            
            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {

                var uid = ControllerHelpers.GetUserId(this);
                var UserName = db.HrRcrtUsers.Where(u => u.UserId == uid).Select(u => u.UserName).FirstOrDefault();
                var docs = db.HrRcrtApplicants.Where(u => u.UserName == UserName).ToList()
                    .Select(d => MapEmpHistList(d)).ToList();
                var RequestIds = docs.Select(d => d.RequestId).ToList();
                var Positions = db.HrRcrtPositionRequests.Where(p => RequestIds.Contains(p.RequestId))
                    .Select(p => new { p.PositionDescription, 
                        p.RequestId,RequestStatus = db.HrRcrtRequestStatusMasters.Where(s => s.StatusId == p.RequestStatusId)
                        .Select(s=>new { s.EnglishDescription, s.SpanishDescription, s.TypeCode }).FirstOrDefault() }).ToList();
                var Statuses = db.HrRcrtApplicantsStatuses.Select(s => new { s.StatusId, s.Description }).ToList();

                return Ok(docs.Select(d=>
                {
                    return new
                    {
                        d.ApplicantId,
                        d.RequestDate,
                        ApplicantStatus = Statuses.Where(s => s.StatusId == d.ApplicantStatus).FirstOrDefault(),
                      Position = Positions.Where(p=>p.RequestId == d.RequestId).FirstOrDefault()
                     
                    };
                }));
            }
            catch (Exception e)
            {
                return ControllerHelpers.InternalError($"{location}: {e.Message} - {e.InnerException} - {e.StackTrace}");
            }

        }

        /// <summary>
        /// Get All Documents
        /// </summary>
        /// <returns>List Documents</returns>
        [HttpGet("IsApplied/{RequestId}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetIsApplied(decimal RequestId)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {

                var uid = ControllerHelpers.GetUserId(this);
                if(uid == 0)
                {
                    return Ok(new { Applied = false });
                }
                var UserName = db.HrRcrtUsers.Where(u => u.UserId == uid).Select(u => u.UserName).FirstOrDefault();
                var Applied = db.HrRcrtApplicants.Where(x => x.UserName == UserName && x.RequestId == RequestId).Any();

                return Ok(new { Applied });
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
        [HttpPut("{ApplicantId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(HrRcrtApplicantUpdateDTO userDTO, decimal ApplicantId)
        {
          
            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var uid = ControllerHelpers.GetUserId(this);
                var UserName = db.HrRcrtUsers.Where(u => u.UserId == uid).Select(u => u.UserName).FirstOrDefault();
                var user = db.HrRcrtApplicants.Where(u => u.UserName == UserName && u.ApplicantId == ApplicantId).FirstOrDefault();
                SafelyUpdate(user, userDTO);
                db.HrRcrtApplicants.Update(user);
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
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Create(HrRcrtApplicantCreateDTO userDTO, string Lang)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var uid = ControllerHelpers.GetUserId(this);
                var UserName = db.HrRcrtUsers.Where(u => u.UserId == uid).Select(u => u.UserName).FirstOrDefault();
                var userDoc = new HrRcrtApplicant
                {
                    CompanyId = ControllerHelpers.GetCompanyId(this),
                    UserName = UserName
                };
                SafelyCreate(userDoc, userDTO);
                SendEmails(userDoc, Lang);
                userDoc.RequestDate = DateTime.Now;
                db.HrRcrtApplicants.Add(userDoc);
                if (db.ChangeTracker.HasChanges())
                {
                    db.SaveChanges();
                }
                return Ok(userDoc);
            }
            catch (Exception e)
            {
                return ControllerHelpers.InternalError($"{location}: {e.Message} - {e.InnerException} - {e.StackTrace}");
            }

        }


        

        private void SendEmails(HrRcrtApplicant Applicant, string Lang)
        {
            var PositionDescription = db.HrRcrtPositionRequests.Where(p => p.RequestId == Applicant.RequestId).Select(x=>x.PositionDescription).FirstOrDefault();
            var CompanyId = ControllerHelpers.GetCompanyId(this);
            var reportsService = db.CoReportsServices.Where(x => x.CompanyId == CompanyId).FirstOrDefault();
            if (reportsService != null)
            {
                var HrMessage = BuildApplicationMessage(Applicant, PositionDescription, Lang, "Hr");
                var UserMessage = BuildApplicationMessage(Applicant, PositionDescription, Lang, "User");
                ControllerHelpers.SendEmail(reportsService, HrMessage);
                ControllerHelpers.SendEmail(reportsService, UserMessage);
            }

        }
        private MailMessage BuildApplicationMessage(HrRcrtApplicant User, string PositionDescription, string Lang, string To)
        {
            var CompanyId = ControllerHelpers.GetCompanyId(this);
            var info = db.HrRcrtExternalWebsiteInfos;
            var Tpls = (from p in info where p.CompanyId == CompanyId select p).ToList();
            foreach (var Tpl1 in Tpls){
                _logger.LogDebug($"{Tpl1.PageName} - {Tpl1.Title1English}");
            }
            var Tpl = (from p in Tpls
                       where p.CompanyId == CompanyId && p.PageName.Equals(To + "PositionRequest")
                       select p).FirstOrDefault();
            if (Tpl == null)
            {
                Tpl = DefaultTemplateHelper.GetTemplate(this, To + "PositionRequest");
                db.HrRcrtExternalWebsiteInfos.Add(Tpl);
                db.SaveChanges();
            }
            _logger.LogDebug($"Company {CompanyId}, {PositionDescription}, {To}");
            var Subject = Lang == "en" ? Tpl.Title1English : Tpl.Title1Spanish;
            var Body = Lang == "en" ? Tpl.Text1English : Tpl.Text1Spanish;
            var Email = User.UserName;
            if(To == "Hr")
            {
                Email = db.HrRcrtCompanyConfigurations.Where(x => x.CompanyId == CompanyId).Select(x => x.SendResumeToEmail).FirstOrDefault();
                
            }
            Body = Body.Replace("[FIRST_NAME]", User.FirstName);
            Body = Body.Replace("[LAST_NAME1]", User.LastName1);
            Body = Body.Replace("[POSITION_DESCRIPTION]", PositionDescription);
            Subject = Subject.Replace("[FIRST_NAME]", User.FirstName)
                .Replace("[LAST_NAME1]", User.LastName1)
                .Replace("[POSITION_DESCRIPTION]", PositionDescription);
            var Message = new MailMessage();
            Message.To.Add(new MailAddress(Email));
            Message.IsBodyHtml = true;
            Message.Subject = Subject;
            Message.Body = Body + GetHrSignature();
            return Message;
        }

        private string GetHrSignature()
        {
            var conf = Startup.GetInstance().Configuration;
            var sign = $"\n\n{conf["Emails:HRSignature"]}";
            return sign;
        }

        private void SafelyUpdate(HrRcrtApplicant UserDoc, HrRcrtApplicantUpdateDTO DTO)
        {
            var props = DTO.GetType().GetProperties();
            foreach(var prop in props)
            {
                UserDoc.GetType().GetProperty(prop.Name).SetValue(UserDoc, prop.GetValue(DTO));
            }

        }
        private void SafelyCreate(HrRcrtApplicant UserDoc, HrRcrtApplicantCreateDTO DTO)
        {
            var props = DTO.GetType().GetProperties();
            foreach (var prop in props)
            {
                UserDoc.GetType().GetProperty(prop.Name).SetValue(UserDoc, prop.GetValue(DTO));
            }

        }

        private HrRcrtApplicantDTO MapEmpHistList(HrRcrtApplicant User)
        {
            var DTO = new HrRcrtApplicantDTO();
            var props = DTO.GetType().GetProperties();
            foreach (var prop in props)
            {
                prop.SetValue(DTO, User.GetType().GetProperty(prop.Name).GetValue(User));
            }

            return DTO;

        }


    }
}
