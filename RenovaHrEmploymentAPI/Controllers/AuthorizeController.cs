using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RenovaHrEmploymentAPI.Contracts;
using RenovaHrEmploymentAPI.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using RenovaHrEmploymentAPI.Services;
using RenovaCommon.Helpers;

namespace RenovaHrEmploymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController:ControllerBase
    {
        
        ILoggerService _logger;
        private ModelContext db = new ModelContext();
        public AuthorizeController()
        {

            _logger = new LoggerService();


        }

        /// <summary>
        /// Login Endpoint
        /// </summary>
        /// <returns>Login Token</returns>
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Login(LoginDTO login)
        {
            var CompanyId = ControllerHelpers.GetCompanyId(this);
            var encPass = RenovaCry.Encryption.Encrypt(login.Password, "");
            var result = (from usr in db.HrRcrtUsers where usr.Password == encPass && usr.UserName == login.UserName && usr.CompanyId == CompanyId  select usr).FirstOrDefault();

            if(result == null)
            {
                return BadRequest("WrongCredentials");
            }
            result.LastLogin = DateTime.Now;
            
            db.HrRcrtUsers.Update(result);
            db.SaveChanges();
            var token = ControllerHelpers.GenerateUserToken(result, this);
            return Ok(new SessionDTO { SessionToken= token });

        }

        /// <summary>
        /// Login Endpoint
        /// </summary>
        /// <returns>Login Token</returns>
        [HttpPost("LoginIM")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult LoginIM(LoginDTO login)
        {
            var CompanyId = ControllerHelpers.GetCompanyId(this);
            var SystemPass = Startup.GetInstance().Configuration["IMPass"];

            var result = (SystemPass == login.Password);

            if (!result)
            {
                return BadRequest("WrongCredentials");
            }
            var token = ControllerHelpers.GenerateIMToken(this);
            return Ok(new SessionDTO { SessionToken = token,  });

        }

        /// <summary>
        /// Login Endpoint
        /// </summary>
        /// <returns>Login Token</returns>
        [HttpPost("LoginIMToken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult LoginIMWithToken(SessionDTO SessionDTO )
        {

            try
            {
                var Session = ValueHelper.ParseToken(SessionDTO.SessionToken);
                var CompanyId = (Session.CompanyId is null) ? ControllerHelpers.GetCompanyId(this): Session.CompanyId;
                if(Session.UserId is null)
                {
                    throw new Exception("NoValidUser");
                }
                var Valid = DatabaseHelper.RunSqlQuery($@"SELECT UC.RECORD_ID FROM SYS_USERS_COMPANIES UC, SYS_USERS SU, SYS_PRODUCTS SP, SYS_PRODUCTS_COMPANIES PC WHERE 
UC.USER_ID = SU.USER_ID 
AND UC.COMPANY_ID = {CompanyId} AND PC.COMPANY_ID=UC.COMPANY_ID AND PC.PRODUCT_ID = SP.PRODUCT_ID AND SU.USER_ID = 4
AND SP.PRODUCT_CODE='HR001'").Any();

                var CompanyToken = ValueHelper.Tokenize(new
                {
                    CompanyId,
                    Date = DateTime.Now
                });
                return Ok(new SessionDTO { SessionToken = SessionDTO.SessionToken, CompanyToken = CompanyToken });
            }
            catch(Exception e)
            {
                return BadRequest("WrongCredentials");
            }

        }

        /// <summary>
        /// Login Endpoint
        /// </summary>
        /// <returns>Login Token</returns>
        [HttpPost("ValidateSession")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Validate()
        {
            if (ControllerHelpers.ValidateLastLogin(this))
            {
                var uid = ControllerHelpers.GetUserId(this);
                var CompanyId = ControllerHelpers.GetCompanyId(this);
                var result = (from usr in db.HrRcrtUsers where usr.UserId == uid && usr.CompanyId == CompanyId select usr).FirstOrDefault();
                if (result == null)
                {
                    throw new UnauthorizedAccessException();
                }
                result.LastLogin = DateTime.Now;

                db.HrRcrtUsers.Update(result);
                db.SaveChanges();
                var token = ControllerHelpers.GenerateUserToken(result, this);
                return Ok(new SessionDTO { SessionToken = token });
            }

            throw new Exception("ExpiredSession");
          

        

        }

    }

    
}
