
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RenovaHrEmploymentAPI.Contracts;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RenovaHrEmploymentAPI.Controllers
{
    /// <summary>
    /// Endpoint used to interact with the FcnGetBusinessUnitNames in the Renova database
    /// </summary>
    
    abstract public class FcnGetController : ControllerBase
    {
        public IFcnGetRepository _FcnGetRepository;
        public ILoggerService _logger;
        public IMapper _mapper;
        
         
        private string GetControllerActionNames()
        {
            var controller = ControllerContext.ActionDescriptor.ControllerName;
            var action = ControllerContext.ActionDescriptor.ActionName;

            return $"{controller} - {action}";
        }

        private ObjectResult InternalError(string message)
        {
            _logger.LogError(message);
            return StatusCode(500, "Something went wrong. Please contact the Administrator");
        }

        protected decimal GetUserId()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            // get some claim by type
            var CompanyID = claimsIdentity.FindFirst("CompanyID").Value;
            //var CompanyID = 1.ToString();

            var UserID = claimsIdentity.FindFirst("UserID").Value;
            //WorkflowDelegatesDTO = Convert.ToDecimal(UserID);
            return Convert.ToDecimal(UserID);

        }

        protected decimal GetCompanyId()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            // get some claim by type
            var CompanyID = claimsIdentity.FindFirst("CompanyID").Value;
            //var CompanyID = 1.ToString();

            var UserID = claimsIdentity.FindFirst("UserID").Value;
            //WorkflowDelegatesDTO = Convert.ToDecimal(UserID);
            return Convert.ToDecimal(CompanyID);

        }

    }
}
