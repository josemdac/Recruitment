﻿using System;
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
    public class HrRcrtUserEmploymentHistController : ControllerBase
    {

        ILoggerService _logger;
        private ModelContext db;
        public HrRcrtUserEmploymentHistController()
        {

            _logger = new LoggerService();
            db = new ModelContext();


        }

        /// <summary>
        /// Get All Documents
        /// </summary>
        /// <returns>List Documents</returns>
        [HttpGet("{EmploymentId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetDoc(decimal EmploymentId)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var uid = ControllerHelpers.GetUserId(this);
                var doc = db.HrRcrtUserEmploymentHists.Where(u => u.UserId == uid && u.EmploymentId == EmploymentId).FirstOrDefault();

                var resp = new HrRcrtUserEmploymentHistUpdateDTO();
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
        [HttpDelete("{EmploymentId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DelDoc(decimal EmploymentId)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var uid = ControllerHelpers.GetUserId(this);
                var doc = db.HrRcrtUserEmploymentHists.Where(u => u.UserId == uid && u.EmploymentId == EmploymentId).FirstOrDefault();
                db.HrRcrtUserEmploymentHists.Remove(doc);
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
            Comparison<HrRcrtUserEmploymentHistListItemDTO> compare = delegate (HrRcrtUserEmploymentHistListItemDTO H1, HrRcrtUserEmploymentHistListItemDTO H2)
            {
                return Convert.ToInt32((H2.StartDate - H1.StartDate).Value.TotalSeconds);
            };
            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {

                var uid = ControllerHelpers.GetUserId(this);
                var docs = db.HrRcrtUserEmploymentHists.Where(u => u.UserId == uid).ToList()
                    .Select(d => MapEmpHistList(d)).ToList();

                docs.Sort(compare);

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
        [HttpPut("{EmploymentId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(HrRcrtUserEmploymentHistUpdateDTO userDTO, decimal EmploymentId)
        {
          
            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var uid = ControllerHelpers.GetUserId(this);
                var user = db.HrRcrtUserEmploymentHists.Where(u => u.UserId == uid && u.EmploymentId == EmploymentId).FirstOrDefault();
                SafelyUpdate(user, userDTO);
                db.HrRcrtUserEmploymentHists.Update(user);
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
        public IActionResult Create(HrRcrtUserEmploymentHistCreateDTO userDTO)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var uid = ControllerHelpers.GetUserId(this);
                var userDoc = new HrRcrtUserEmploymentHist
                {
                    CompanyId = ControllerHelpers.GetCompanyId(this),
                    UserId = uid
                };
                SafelyCreate(userDoc, userDTO);
                db.HrRcrtUserEmploymentHists.Add(userDoc);
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


        private void SafelyUpdate(HrRcrtUserEmploymentHist UserDoc, HrRcrtUserEmploymentHistUpdateDTO DTO)
        {
            var props = DTO.GetType().GetProperties();
            foreach(var prop in props)
            {
                UserDoc.GetType().GetProperty(prop.Name).SetValue(UserDoc, prop.GetValue(DTO));
            }

        }
        private void SafelyCreate(HrRcrtUserEmploymentHist UserDoc, HrRcrtUserEmploymentHistCreateDTO DTO)
        {
            var props = DTO.GetType().GetProperties();
            foreach (var prop in props)
            {
                UserDoc.GetType().GetProperty(prop.Name).SetValue(UserDoc, prop.GetValue(DTO));
            }

        }

        private HrRcrtUserEmploymentHistListItemDTO MapEmpHistList(HrRcrtUserEmploymentHist User)
        {
            var DTO = new HrRcrtUserEmploymentHistListItemDTO();
            var props = DTO.GetType().GetProperties();
            foreach (var prop in props)
            {
                prop.SetValue(DTO, User.GetType().GetProperty(prop.Name).GetValue(User));
            }

            return DTO;

        }


    }
}
