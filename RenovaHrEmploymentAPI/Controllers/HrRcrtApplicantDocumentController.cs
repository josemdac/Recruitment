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
    public class HrRcrtApplicantDocumentController : ControllerBase
    {

        ILoggerService _logger;
        private ModelContext db;
        public HrRcrtApplicantDocumentController()
        {

            _logger = new LoggerService();
            db = new ModelContext();


        }

        /// <summary>
        /// Get All Documents
        /// </summary>
        /// <returns>List Documents</returns>
        [HttpGet("{DocumentId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetDoc(decimal DocumentId)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var aid = ControllerHelpers.GetApplicantId(this);
                var doc = db.HrRcrtApplicantDocuments.Where(u => u.ApplicantId == aid && u.DocumentId == DocumentId).FirstOrDefault();

                var resp = new HrRcrtApplicantDocumentUpdateDTO();
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
        [HttpDelete("{DocumentId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DelDoc(decimal DocumentId)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var aid = ControllerHelpers.GetApplicantId(this);
                var doc = db.HrRcrtApplicantDocuments.Where(u => u.ApplicantId == aid && u.DocumentId == DocumentId).FirstOrDefault();
                db.HrRcrtApplicantDocuments.Remove(doc);
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
        public IActionResult GetCurrentUser()
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var aid = ControllerHelpers.GetApplicantId(this);
                var docs = db.HrRcrtApplicantDocuments.Where(u => u.ApplicantId == aid).ToList().Select(d => MapDocList(d));
                

                return Ok(docs);
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
        [HttpPut("{DocumentId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(HrRcrtApplicantDocumentUpdateDTO userDTO, decimal DocumentId)
        {
          
            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var aid = ControllerHelpers.GetApplicantId(this);
                var CompanyId = ControllerHelpers.GetCompanyId(this);
                var user = db.HrRcrtApplicantDocuments.Where(u => u.ApplicantId == aid && u.DocumentId == DocumentId && u.CompanyId == CompanyId).FirstOrDefault();
                SafelyUpdate(user, userDTO);
                db.HrRcrtApplicantDocuments.Update(user);
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
        public IActionResult Create(HrRcrtApplicantDocumentCreateDTO userDTO)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var aid = ControllerHelpers.GetApplicantId(this);
                var userDoc = new HrRcrtApplicantDocument
                {
                    CompanyId = ControllerHelpers.GetCompanyId(this),
                    ApplicantId = aid
                };
                SafelyCreate(userDoc, userDTO);
                db.HrRcrtApplicantDocuments.Add(userDoc);
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


        private void SafelyUpdate(HrRcrtApplicantDocument UserDoc, HrRcrtApplicantDocumentUpdateDTO DTO)
        {
            var props = DTO.GetType().GetProperties();
            foreach(var prop in props)
            {
                UserDoc.GetType().GetProperty(prop.Name).SetValue(UserDoc, prop.GetValue(DTO));
            }

        }
        private void SafelyCreate(HrRcrtApplicantDocument UserDoc, HrRcrtApplicantDocumentCreateDTO DTO)
        {
            var props = DTO.GetType().GetProperties();
            foreach (var prop in props)
            {
                UserDoc.GetType().GetProperty(prop.Name).SetValue(UserDoc, prop.GetValue(DTO));
            }

        }

        private HrRcrtApplicantDocumentListItemDTO MapDocList(HrRcrtApplicantDocument User)
        {
            var DTO = new HrRcrtApplicantDocumentListItemDTO();
            var props = DTO.GetType().GetProperties();
            foreach (var prop in props)
            {
                if(prop.Name != "HasFile")
                {
                    prop.SetValue(DTO, User.GetType().GetProperty(prop.Name).GetValue(User));
                }
                
            }

            DTO.HasFile = (User.Document != null && User.Document.Length > 0);
          

            return DTO;

        }


    }
}
