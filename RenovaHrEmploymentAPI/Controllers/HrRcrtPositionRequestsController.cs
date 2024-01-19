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
using Oracle.ManagedDataAccess.Client;

namespace RenovaHrEmploymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HrRcrtPositionRequestsController : ControllerBase
    {

        ILoggerService _logger;
        private ModelContext db;
        public HrRcrtPositionRequestsController()
        {

            _logger = new LoggerService();
            db = new ModelContext();


        }
        private static int MAXITEMS = 50;
        delegate bool Del(string EnglishDesc, string SpanishDesc, string PositonDesc);
        /// <summary>
        /// Get All Position requests
        /// </summary>
        /// <returns>List Of Position Requests</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<IActionResult> GetReference(HrRcrtPositionRequestListStateDTO State)
        {

            var Skip = State.Skip;
            var Take = State.Take;
            var KeyWord = State.KeyWord;
            var Location = State.Location;

            if(Take > MAXITEMS) 
            {
                Take = MAXITEMS;
            }

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                HttpContext.Request.Headers.TryGetValue("CompanyToken", out var companyToken);
                _logger.LogDebug("COMPTOKEN" + companyToken);
                var uid = ControllerHelpers.GetUserId(this);
                //var UserName = db.HrRcrtUsers.Where(u => u.UserId == uid).Select(u => u.UserName).FirstOrDefault();
                var CompanyId = ControllerHelpers.GetCompanyId(this);
                //var Applicants = db.HrRcrtApplicants.Where(a => a.CompanyId == CompanyId && a.UserName == UserName).Select(x=>x.RequestId).ToList();
                //var locationProfiles = db.HrRcrtPositionProfileTypes.Where(x => x.ProfileType == "LO").Where(x => x.CompanyId == CompanyId).ToList()
                //    .Where(x=>(Location != null)?(x.EnglishDescription.ToUpper().Contains(Location.ToUpper()) && x.SpanishDescription.ToUpper().Contains(Location.ToUpper())) :true)
                //    .Select(x => x.TypeId).ToList();
                //var jobTypeProfiles = db.HrRcrtPositionProfileTypes.Where(x => x.ProfileType == "JT").Where(x => x.CompanyId == CompanyId).ToList()
                //    .Select(x => x.TypeId).ToList();

                //var statusA = db.HrRcrtRequestStatusMasters.Where(x => x.TypeCode == "A").Select(st => new { st.StatusId, st.TypeCode, st.SpanishDescription, st.EnglishDescription }).FirstOrDefault();
                //var requests = db.HrRcrtPositionRequests;
                //var response = (from pr in requests
                //                where pr.ExternalPostingEnd >= DateTime.Now && (pr.PostingType.Trim() == "E" || pr.PostingType.Trim() == "B")
                //                && pr.RequestStatusId == statusA.StatusId && pr.CompanyId == CompanyId 
                //                && !Applicants.Contains(pr.RequestId)
                //                orderby pr.TimesViewed
                //                select new
                //                {
                //                    pr.RequestId,
                //                    pr.LocationProfileType,
                //                    LocationProfile = pr.LocationProfileTypeNavigation,
                //                    LocSpanish = pr.LocationProfileTypeNavigation.SpanishDescription,
                //                    LocEnglish = pr.LocationProfileTypeNavigation.EnglishDescription,
                //                    pr.JobTypeProfileType,
                //                    JobTypeProfile = pr.JobTypeProfileTypeNavigation,
                //                    JobTypeEnglish = pr.JobTypeProfileTypeNavigation.EnglishDescription,
                //                    JobTypeSpanish = pr.JobTypeProfileTypeNavigation.SpanishDescription,
                //                    pr.PositionsNeeded,
                //                    pr.PositionDescription,
                //                    pr.TimesViewed

                //                }).Where(x => ((KeyWord != null)?(x.PositionDescription.ToUpper().Contains(KeyWord.ToUpper())):true) && locationProfiles.Contains((decimal)x.LocationProfileType));
                /*
                 
                 "FCN_HR_RCRT_GET_POSITION_REQST" (
                                                PN_COMPANYID    NUMBER,
                                                PS_TYPE         VARCHAR2,
                                                PS_FILTER       VARCHAR2
  ) RETURN SYS_REFCURSOR AS C SYS_REFCURSOR;
                 */

                var Filter = "";
                //var UserName = db.HrRcrtUsers.Where(u => u.UserId == uid).Select(u => u.UserName).FirstOrDefault();

                //if(UserName != null)
                //{
                //    Filter = $" AND PR.REQUEST_ID NOT IN (SELECT REQUEST_ID FROM HR_RCRT_APPLICANTS AP WHERE AP.USER_NAME = '{UserName}')";
                //}
                FcnCommand command = new FcnCommand("FCN_HR_RCRT_GET_POSITION_REQST");
                command.AddReturn("returnVal", null, OracleDbType.RefCursor);
                command.AddInput("PN_COMPANYID", CompanyId, OracleDbType.Decimal);
                command.AddInput("PS_TYPE","E", OracleDbType.Varchar2);
                command.AddInput("PS_FILTER", Filter, OracleDbType.Varchar2);
                var result = command.ExecuteAsCursor();
                return Ok(result);
                #region Query
                //if (State.JobTypes != null)
                //{
                //    if(State.JobTypes.Count() > 0)
                //    {
                //        response = response.Where(x => State.JobTypes.Contains((decimal)x.JobTypeProfileType));
                //    }

                //}
                //if(State.Sort != null)
                //{
                //    foreach(var Sort in State.Sort)
                //    {
                //        if(Sort.Field == "positionDescription")
                //        {
                //            if (Sort.Dir == "desc")
                //            {
                //                response = response.OrderByDescending(x => x.PositionDescription);
                //            }
                //            else
                //            {
                //                response = response.OrderBy(x => x.PositionDescription);
                //            }
                //        }

                //        if (Sort.Field == "locationProfile")
                //        {
                //            if (Sort.Dir == "desc")
                //            {
                //                if(State.Lang == "English")
                //                {
                //                    response = response.OrderByDescending(x => x.LocationProfile.EnglishDescription);
                //                }
                //                else
                //                {
                //                    response = response.OrderByDescending(x => x.LocationProfile.SpanishDescription);
                //                }

                //            }
                //            else
                //            {
                //                if (State.Lang == "English")
                //                {
                //                    response = response.OrderBy(x => x.LocationProfile.EnglishDescription);
                //                }
                //                else
                //                {
                //                    response = response.OrderBy(x => x.LocationProfile.SpanishDescription);
                //                }
                //            }
                //        }

                //        if (Sort.Field == "jobTypeProfile")
                //        {
                //            if (Sort.Dir == "desc")
                //            {
                //                if (State.Lang == "English")
                //                {
                //                    response = response.OrderByDescending(x => x.JobTypeEnglish);
                //                }
                //                else
                //                {
                //                    response = response.OrderByDescending(x => x.JobTypeSpanish);
                //                }

                //            }
                //            else
                //            {
                //                if (State.Lang == "English")
                //                {
                //                    response = response.OrderBy(x => x.JobTypeEnglish);
                //                }
                //                else
                //                {
                //                    response = response.OrderBy(x => x.JobTypeSpanish);
                //                }
                //            }
                //        }
                //    }
                //}
                //response.Sort(SortBy);


                //var Count = response.Count();
                // return Ok(new { Data = response, Count });

                //var result = new List<object>();
                //foreach(var i in new List<int>{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 })
                //{
                //    result.AddRange(response);
                //}
                //Count = result.Count();
                
                
                //return Ok(new { Data = response, Count });
                #endregion
            }
            catch (Exception e)
            {
                return ControllerHelpers.InternalError($"{location}: {e.Message} - {e.InnerException} - {e.StackTrace}");
            }

        }

        private static bool ProcessLocation(string EnglishDescription, string SpanishDescription, string Location)
        {
            var contains = false;
            if (Location != null && Location == null)
            {

                foreach (var w in Location.Split(" "))
                {
                    contains = (EnglishDescription.ToUpper().Contains(w.ToUpper()) ||
                    SpanishDescription.ToUpper().Contains(w.ToUpper()));
                    if (contains)
                    {
                        return contains;
                    }
                }
                return contains;
            }

            return true;
        }

        private static bool ProcessKeyword(string Description, string KeyWord)
        {
            var contains = false;
            if (KeyWord != null)
            {

                foreach (var w in KeyWord.Split(" "))
                {
                    contains = (Description.ToUpper().Contains(w.ToUpper()));
                    if (contains)
                    {
                        return contains;
                    }
                }
                return contains;
            }

            return true;
        }

        /// <summary>
        /// Get All Position requests
        /// </summary>
        /// <returns>List Of Position Requests</returns>
        [HttpGet("Top10")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<IActionResult> GetTopJobs()
        {
            var Take = 10;

            Comparison<object> SortBy = delegate (object A, object B)
            {
                var Tv1 = A.GetType().GetProperty("TimesViewed").GetValue(A);
                var Tv2 = B.GetType().GetProperty("TimesViewed").GetValue(B);
                return Convert.ToInt32(Tv1) - Convert.ToInt32(Tv2);
            };

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var CompanyId = ControllerHelpers.GetCompanyId(this);
                var statusA = db.HrRcrtRequestStatusMasters.Where(x => x.TypeCode == "A").Select(st => new { st.StatusId, st.TypeCode, st.SpanishDescription, st.EnglishDescription }).FirstOrDefault();
                var requests = db.HrRcrtPositionRequests;
                var response = (from pr in requests
                                where pr.CompanyId == CompanyId
                                && pr.ExternalPostingEnd >= DateTime.Now  

                                orderby pr.TimesViewed descending
                                select new
                                {
                                    pr.RequestId,

                                    //CompanyProfile = GetProfileType(pr.CompanyProfileType, db),
                                    //EducationProfile = GetProfileType(pr.EducationProfileType, db),
                                    //ExpertiseProfile = GetProfileType(pr.ExpertiseProfileType, db),
                                    //JobLevelProfile = GetProfileType(pr.JobLevelProfileType, db),
                                    pr.JobTypeProfileType,
                                    pr.LocationProfileType,
                                    //LanguageProfile = GetProfileType(pr.LanguageProfileType, db),
                                    //ExperienceProfile = GetProfileType(pr.ExperienceProfileType, db),
                                    //SalaryProfile = GetProfileType(pr.SalaryProfileType, db),
                                    pr.PositionsNeeded,
                                    pr.PositionDescription,
                                    pr.TimesViewed

                                }).Skip(0).Take(Take).ToList().Select(pr=> {
                                    var LocProf = GetProfileType(pr.LocationProfileType, db);
                                    var JobProf = GetProfileType(pr.JobTypeProfileType, db);
                                    return new {
                                        pr.RequestId,

                                        pr.PositionsNeeded,
                                        pr.PositionDescription,
                                        LocationEnglish = LocProf.EnglishDescription,
                                        LocationSpanish = LocProf.SpanishDescription,
                                        JobTypeSpanish = JobProf.SpanishDescription,
                                        JobTypeEnglish = JobProf.EnglishDescription,

                                        pr.TimesViewed
                                    };
                                    });

                //response.Sort(SortBy);


                var Count = response.Count();
                return Ok(new { Data = response, Count });
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
        [HttpGet("{RequestId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<IActionResult> GetItem(decimal RequestId)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var requests = db.HrRcrtPositionRequests;
                var response = (from pr in (from r in requests
                                            where r.RequestId == RequestId
                                            select r).ToList()
                                select new
                                {
                                    pr.RequestId,
                                    pr.RequestDate,
                                    CompanyProfile = GetProfileType(pr.CompanyProfileType, db),
                                    EducationProfile = GetProfileType(pr.EducationProfileType, db),
                                    ExpertiseProfile = GetProfileType(pr.ExpertiseProfileType, db),
                                    JobLevelProfile = GetProfileType(pr.JobLevelProfileType, db),
                                    JobTypeProfile = GetProfileType(pr.JobTypeProfileType, db),
                                    LocationProfile = GetProfileType(pr.LocationProfileType, db),
                                    LanguageProfile = GetProfileType(pr.LanguageProfileType, db),
                                    ExperienceProfile = GetProfileType(pr.ExperienceProfileType, db),
                                    SalaryProfile = GetProfileType(pr.SalaryProfileType, db),
                                    RequestStatus = db.HrRcrtRequestStatusMasters.Where(s => s.StatusId == pr.RequestStatusId).Select(st => new { st.TypeCode, st.SpanishDescription, st.EnglishDescription }).FirstOrDefault(),
                                    pr.PositionsNeeded,
                                    pr.PositionDescription,
                                    pr.PositionDetailsEnglish,
                                    pr.PositionDetailsSpanish


                                });//.Where(r => r.RequestStatus.TypeCode.Trim() == "A");


                return Ok(response.FirstOrDefault());
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
        [HttpGet("Header/{RequestId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<IActionResult> GetHeaderItem(decimal RequestId)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var CompanyId = ControllerHelpers.GetCompanyId(this);
                var requests = db.HrRcrtPositionRequests;
                var response = (from pr in (from r in requests
                                            where r.RequestId == RequestId
                                            select r).ToList()
                                select new
                                {
                                    pr.RequestDate,
                                    LocationProfile = GetProfileType(pr.LocationProfileType, db),
                                    RequestStatus = db.HrRcrtRequestStatusMasters.Where(s => s.StatusId == pr.RequestStatusId).Select(st => new { st.TypeCode, st.SpanishDescription, st.EnglishDescription }).FirstOrDefault(),
                                    pr.PositionDescription,
                                    


                                });


                return Ok(response.FirstOrDefault());
            }
            catch (Exception e)
            {
                return ControllerHelpers.InternalError($"{location}: {e.Message} - {e.InnerException} - {e.StackTrace}");
            }

        }

        /// <summary>
        /// Post view
        /// </summary>
        /// <returns>List Of Position Requests</returns>
        [HttpPost("PV/{RequestId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult PostView(decimal RequestId)
        {

            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var CompanyId = ControllerHelpers.GetCompanyId(this);
                var req = db.HrRcrtPositionRequests.Where(x => x.RequestId == RequestId && x.CompanyId == CompanyId).FirstOrDefault();
                if(req != null)
                {
                    req.TimesViewed += 1;
                    db.HrRcrtPositionRequests.Update(req);
                    if (db.ChangeTracker.HasChanges())
                    {
                        db.SaveChanges();
                    }
                }

                return NoContent();
            }
            catch (Exception e)
            {
                return ControllerHelpers.InternalError($"{location}: {e.Message} - {e.InnerException} - {e.StackTrace}");
            }

        }


        /// <summary>
        /// Post view
        /// </summary>
        /// <returns>List Of Position Request Questions</returns>
        [HttpGet("Questions/{RequestId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetQUestions(decimal RequestId)
        {

            var ApplicantId = ControllerHelpers.GetApplicantId(this);
            
            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {
                var CompanyId = ControllerHelpers.GetCompanyId(this);
                var questions = (from rq in db.HrRcrtRequestQuestions
                                 where rq.RequestId == RequestId
                                 && rq.Status == "A"
                                 && rq.CompanyId == CompanyId

                                 select new
                                 {
                                     rq.QuestionId,
                                     rq.Question,
                                     rq.QuestionType,
                                     Answers = (from ra in db.HrRcrtRequestQuestnAnswers
                                                where ra.QuestionId == rq.QuestionId
                                                && ra.ApplicantId == ApplicantId
                                                select new { 
                                                ra.Answer,
                                                ra.AnswerId
                                                }).ToList(),
                                     Choices = (from rc in db.HrRcrtRequestQuestChoices
                                                where rc.QuestionId == rq.QuestionId
                                                && rc.Status == "A"
                                                select new
                                                {
                                                    rc.Choice,
                                                    rc.ChoiceId
                                                }).ToList()
                                 });
                
                
                return Ok(questions.ToList());
            }
            catch (Exception e)
            {
                return ControllerHelpers.InternalError($"{location}: {e.Message} - {e.InnerException} - {e.StackTrace}");
            }

        }

        /// <summary>
        /// Post question answers
        /// </summary>
        /// <returns>List Of Position Request Questions</returns>
        [HttpPost("SaveQuestions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult SaveQUestions(HrRcrtRequestQuestionDTO[] Questions)
        {
            var Answers = new List<HrRcrtRequestQuestnAnswer>();
            var ApplicantId = ControllerHelpers.GetApplicantId(this);
            var RequestId = db.HrRcrtApplicants.Where(a => a.ApplicantId == ApplicantId).Select(a => a.RequestId).FirstOrDefault();
            var location = ControllerHelpers.GetControllerActionNames(this);
            try
            {

                var CompanyId = ControllerHelpers.GetCompanyId(this);
                var currentAnswers = (from ra in db.HrRcrtRequestQuestnAnswers
                                 where Questions.Select(a=>a.QuestionId).Contains(ra.QuestionId)
                                 && ra.ApplicantId == ApplicantId
                                 select ra);
                db.HrRcrtRequestQuestnAnswers.RemoveRange(currentAnswers);
                db.SaveChanges();
                var AnsId = db.HrRcrtRequestQuestnAnswers.Select(x => x.AnswerId).Max();
                foreach(var Q in Questions)
                {
                    var A = Q.Answers.Select((x, s) => new HrRcrtRequestQuestnAnswer
                    {
                        Answer = x.Answer,
                        QuestionId = Q.QuestionId,
                        ApplicantId = ApplicantId,
                    });

                    foreach(var Item in A)
                    {
                        AnsId = AnsId + 1;
                        Item.AnswerId = AnsId;
                    }
                    Answers.AddRange(A);
                }


                db.HrRcrtRequestQuestnAnswers.AddRange(Answers);
                db.SaveChanges();
                return NoContent();

                //return Ok(questions.ToList());
            }
            catch (Exception e)
            {
                return ControllerHelpers.InternalError($"{location}: {e.Message} - {e.InnerException} - {e.StackTrace}");
            }

        }
        private object GetCompany(decimal? id, ModelContext db)
        {
            if (id == null) { return new { }; }
            return db.CoCompanyMasters.Where(x => x.CompanyId == id).Select(x => new
            {
                x.CompanyName,
                x.Address1,
                x.Address2,
                x.Zipcode,
                x.State
            }).FirstOrDefault();
        }
        private object GetBu(decimal? id, ModelContext db)
        {
            if (id == null) { return new { }; }
            return db.CoBusinessUnitsMasters.Where(x => x.BusinessUnitId == id).Select(x => x.Description).FirstOrDefault();
        }
        private HrRcrtPositionProfileTypeSimple GetProfileType(decimal? id, ModelContext db)
        {
            if (id == null) { return new HrRcrtPositionProfileTypeSimple(); }
            return (db.HrRcrtPositionProfileTypes.Where(p => p.TypeId == id).Select(x => new
            HrRcrtPositionProfileTypeSimple {
                EnglishDescription = x.EnglishDescription,
                SpanishDescription = x.SpanishDescription,
                ProfileType = x.ProfileType
            })).FirstOrDefault();

        }
    }
}
