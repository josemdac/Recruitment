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
    public class HrRcrtUserDocumentController : ControllerBase
    {

        ILoggerService _logger;
        private ModelContext db;
        public HrRcrtUserDocumentController()
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
                var uid = ControllerHelpers.GetUserId(this);
                var doc = db.HrRcrtUserDocuments.Where(u => u.UserId == uid && u.DocumentId == DocumentId).FirstOrDefault();

                var resp = new HrRcrtUserDocumentUpdateDTO();
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
                var uid = ControllerHelpers.GetUserId(this);
                var doc = db.HrRcrtUserDocuments.Where(u => u.UserId == uid && u.DocumentId == DocumentId).FirstOrDefault();
                db.HrRcrtUserDocuments.Remove(doc);
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
                var uid = ControllerHelpers.GetUserId(this);
                var docs = db.HrRcrtUserDocuments.Where(u => u.UserId == uid).ToList().Select(d => MapDocList(d));
                

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
        public IActionResult Update(HrRcrtUserDocumentUpdateDTO userDTO, decimal DocumentId)
        {
          
            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var uid = ControllerHelpers.GetUserId(this);
                var user = db.HrRcrtUserDocuments.Where(u => u.UserId == uid && u.DocumentId == DocumentId).FirstOrDefault();
                SafelyUpdate(user, userDTO);
                db.HrRcrtUserDocuments.Update(user);
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
        public IActionResult Create(HrRcrtUserDocumentCreateDTO userDTO)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var uid = ControllerHelpers.GetUserId(this);
                var userDoc = new HrRcrtUserDocument
                {
                    CompanyId = ControllerHelpers.GetCompanyId(this),
                    UserId = uid
                };
                SafelyCreate(userDoc, userDTO);
                db.HrRcrtUserDocuments.Add(userDoc);
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


        private void SafelyUpdate(HrRcrtUserDocument UserDoc, HrRcrtUserDocumentUpdateDTO DTO)
        {
            var props = DTO.GetType().GetProperties();
            foreach(var prop in props)
            {
                UserDoc.GetType().GetProperty(prop.Name).SetValue(UserDoc, prop.GetValue(DTO));
            }

        }
        private void SafelyCreate(HrRcrtUserDocument UserDoc, HrRcrtUserDocumentCreateDTO DTO)
        {
            var props = DTO.GetType().GetProperties();
            foreach (var prop in props)
            {
                UserDoc.GetType().GetProperty(prop.Name).SetValue(UserDoc, prop.GetValue(DTO));
            }

        }

        private HrRcrtUserDocumentListItemDTO MapDocList(HrRcrtUserDocument User)
        {
            var DTO = new HrRcrtUserDocumentListItemDTO();
            var props = DTO.GetType().GetProperties();
            foreach (var prop in props)
            {
                prop.SetValue(DTO, User.GetType().GetProperty(prop.Name).GetValue(User));
            }

            return DTO;

        }


    }
}
