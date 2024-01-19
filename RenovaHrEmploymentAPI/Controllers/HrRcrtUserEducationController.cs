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
    public class HrRcrtUserEducationController : ControllerBase
    {

        ILoggerService _logger;
        private ModelContext db;
        public HrRcrtUserEducationController()
        {

            _logger = new LoggerService();
            db = new ModelContext();


        }

        /// <summary>
        /// Get All Documents
        /// </summary>
        /// <returns>List Documents</returns>
        [HttpGet("{EducationId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetDoc(decimal EducationId)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var uid = ControllerHelpers.GetUserId(this);
                var doc = db.HrRcrtUserEducations.Where(u => u.UserId == uid && u.EducationId == EducationId).FirstOrDefault();

                var resp = new HrRcrtUserEducationUpdateDTO();
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
        [HttpDelete("{EducationId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DelDoc(decimal EducationId)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var uid = ControllerHelpers.GetUserId(this);
                var doc = db.HrRcrtUserEducations.Where(u => u.UserId == uid && u.EducationId == EducationId).FirstOrDefault();
                db.HrRcrtUserEducations.Remove(doc);
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
            Comparison<HrRcrtUserEducationListItemDTO> compare = delegate (HrRcrtUserEducationListItemDTO H1, HrRcrtUserEducationListItemDTO H2)
            {
                return H1.SchoolName.CompareTo(H2.SchoolName);
            };
            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {

                var uid = ControllerHelpers.GetUserId(this);
                var docs = db.HrRcrtUserEducations.Where(u => u.UserId == uid).ToList()
                    .Select(d => MapEmpHistList(d)).ToList();

                
                docs.Sort(compare);

                return Ok(docs.Select(d=>
                {
                    return new
                    {
                        d.SchoolName,
                        d.Major,
                        d.YearsCompleted,
                        d.Graduated,
                        d.GraduatedYear,
                        d.EducationId,
                        d.Gpa,
                        CountryName = (db.SysCountryMasters.Where(c => c.CountryId == d.CountryId).Select(c => c.CountryName).FirstOrDefault()),
                        Degree = (db.HrEducationDegrees.Where(c => c.DegreeId == d.DegreeId).Select(c => c.Description).FirstOrDefault()),
                    };
                }));
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
        [HttpPut("{EducationId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(HrRcrtUserEducationUpdateDTO userDTO, decimal EducationId)
        {
          
            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var uid = ControllerHelpers.GetUserId(this);
                var user = db.HrRcrtUserEducations.Where(u => u.UserId == uid && u.EducationId == EducationId).FirstOrDefault();
                SafelyUpdate(user, userDTO);
                db.HrRcrtUserEducations.Update(user);
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
        public IActionResult Create(HrRcrtUserEducationCreateDTO userDTO)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var uid = ControllerHelpers.GetUserId(this);
                var userDoc = new HrRcrtUserEducation
                {
                    CompanyId = ControllerHelpers.GetCompanyId(this),
                    UserId = uid
                };
                SafelyCreate(userDoc, userDTO);
                db.HrRcrtUserEducations.Add(userDoc);
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


        private void SafelyUpdate(HrRcrtUserEducation UserDoc, HrRcrtUserEducationUpdateDTO DTO)
        {
            var props = DTO.GetType().GetProperties();
            foreach(var prop in props)
            {
                UserDoc.GetType().GetProperty(prop.Name).SetValue(UserDoc, prop.GetValue(DTO));
            }

        }
        private void SafelyCreate(HrRcrtUserEducation UserDoc, HrRcrtUserEducationCreateDTO DTO)
        {
            var props = DTO.GetType().GetProperties();
            foreach (var prop in props)
            {
                UserDoc.GetType().GetProperty(prop.Name).SetValue(UserDoc, prop.GetValue(DTO));
            }

        }

        private HrRcrtUserEducationListItemDTO MapEmpHistList(HrRcrtUserEducation User)
        {
            var DTO = new HrRcrtUserEducationListItemDTO();
            var props = DTO.GetType().GetProperties();
            foreach (var prop in props)
            {
                prop.SetValue(DTO, User.GetType().GetProperty(prop.Name).GetValue(User));
            }

            return DTO;

        }


    }
}
