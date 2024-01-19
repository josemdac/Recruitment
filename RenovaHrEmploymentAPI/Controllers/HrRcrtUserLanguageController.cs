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
    public class HrRcrtUserLanguageController : ControllerBase
    {

        ILoggerService _logger;
        private ModelContext db;
        public HrRcrtUserLanguageController()
        {

            _logger = new LoggerService();
            db = new ModelContext();


        }

        /// <summary>
        /// Get All Documents
        /// </summary>
        /// <returns>List Documents</returns>
        [HttpGet("{RecordId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetDoc(decimal RecordId)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var uid = ControllerHelpers.GetUserId(this);
                var doc = db.HrRcrtUserLanguages.Where(u => u.UserId == uid && u.RecordId == RecordId).FirstOrDefault();

                var resp = new HrRcrtUserLanguageUpdateDTO();
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
        [HttpDelete("{RecordId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DelDoc(decimal RecordId)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var uid = ControllerHelpers.GetUserId(this);
                var doc = db.HrRcrtUserLanguages.Where(u => u.UserId == uid && u.RecordId == RecordId).FirstOrDefault();
                db.HrRcrtUserLanguages.Remove(doc);
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
        public IActionResult GetCurrentLangs()
        {
            
            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var isoLangs = ControllerHelpers.ISOLangs;
                var uid = ControllerHelpers.GetUserId(this);
                var langs = db.SysLanguages.Where(l=>l.Status == "A").Select(l=>new { 
                    l.Culture,
                    l.Language,
                    l.LanguageId,
                    l.Status,
                }).ToList().Select(l=>new {
                    l.Culture,
                    l.Language,
                    l.LanguageId,
                    l.Status,
                    IsoLang = isoLangs.Where(i => i.culture == l.Culture).FirstOrDefault()
                });
                var docs = db.HrRcrtUserLanguages.Where(u => u.UserId == uid).ToList()
                    .Select(d => MapEmpHistList(d)).ToList();

                

                return Ok(docs.Select(d=>
                {
                    var lang = langs.Where(l => l.LanguageId == d.LanguageId).FirstOrDefault();
                    var defaultLang = langs.Where(l => l.Culture == "en").FirstOrDefault();
                    return new
                    {
                        d.LanguageId,
                        d.RecordId,
                        d.ReadingProficiency,
                        d.SpeakingProficiency,
                        d.WritingProficiency,
                        Language = lang == null?defaultLang :lang
                    };
                }).OrderBy(x=>x.Language.Culture).ToList());
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
        [HttpPut("{RecordId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(HrRcrtUserLanguageUpdateDTO userDTO, decimal RecordId)
        {
          
            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var uid = ControllerHelpers.GetUserId(this);
                var user = db.HrRcrtUserLanguages.Where(u => u.UserId == uid && u.RecordId == RecordId).FirstOrDefault();
                SafelyUpdate(user, userDTO);
                db.HrRcrtUserLanguages.Update(user);
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
        public IActionResult Create(HrRcrtUserLanguageCreateDTO userDTO)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var uid = ControllerHelpers.GetUserId(this);
                var userDoc = new HrRcrtUserLanguage
                {
                    CompanyId = ControllerHelpers.GetCompanyId(this),
                    UserId = uid
                };
                SafelyCreate(userDoc, userDTO);
                db.HrRcrtUserLanguages.Add(userDoc);
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


        private void SafelyUpdate(HrRcrtUserLanguage UserDoc, HrRcrtUserLanguageUpdateDTO DTO)
        {
            var props = DTO.GetType().GetProperties();
            foreach(var prop in props)
            {
                UserDoc.GetType().GetProperty(prop.Name).SetValue(UserDoc, prop.GetValue(DTO));
            }

        }
        private void SafelyCreate(HrRcrtUserLanguage UserDoc, HrRcrtUserLanguageCreateDTO DTO)
        {
            var props = DTO.GetType().GetProperties();
            foreach (var prop in props)
            {
                UserDoc.GetType().GetProperty(prop.Name).SetValue(UserDoc, prop.GetValue(DTO));
            }

        }

        private HrRcrtUserLanguageListItemDTO MapEmpHistList(HrRcrtUserLanguage User)
        {
            var DTO = new HrRcrtUserLanguageListItemDTO();
            var props = DTO.GetType().GetProperties();
            foreach (var prop in props)
            {
                prop.SetValue(DTO, User.GetType().GetProperty(prop.Name).GetValue(User));
            }

            return DTO;

        }


    }
}
