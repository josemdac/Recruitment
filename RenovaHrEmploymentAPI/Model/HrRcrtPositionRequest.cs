using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class HrRcrtPositionRequest
    {
        public HrRcrtPositionRequest()
        {
            HrRcrtApplicants = new HashSet<HrRcrtApplicant>();
            HrRcrtInterviews = new HashSet<HrRcrtInterview>();
            HrRcrtRequestQuestions = new HashSet<HrRcrtRequestQuestion>();
            HrRcrtTestRegistrations = new HashSet<HrRcrtTestRegistration>(); 
            HrRcrtTestRequests = new HashSet<HrRcrtTestRequest>();
        }

        public decimal RequestId { get; set; }
        public decimal CompanyId { get; set; }
        public string PositionDescription { get; set; }
        public DateTime RequestDate { get; set; }
        public string PostingType { get; set; }
        public decimal? CompanyProfileType { get; set; }
        public decimal? EducationProfileType { get; set; }
        public decimal? ExpertiseProfileType { get; set; }
        public decimal? JobLevelProfileType { get; set; }
        public decimal? JobTypeProfileType { get; set; }
        public decimal? LocationProfileType { get; set; }
        public decimal? LanguageProfileType { get; set; }
        public decimal? ExperienceProfileType { get; set; }
        public decimal? SalaryProfileType { get; set; }
        public DateTime? InternalPostingStart { get; set; }
        public DateTime? InternalPostingEnd { get; set; }
        public DateTime? ExternalPostingStart { get; set; }
        public DateTime? ExternalPostingEnd { get; set; }
        public string PositionDetailsEnglish { get; set; }
        public string PositionDetailsSpanish { get; set; }
        public decimal? BusinessUnitId { get; set; }
        public decimal? RequestStatusId { get; set; }
        public decimal? PositionsNeeded { get; set; }
        public decimal? TimesViewed { get; set; }
        public string LastUpdUserName { get; set; }
        public string LastUpdTerminal { get; set; }
        public DateTime? LastUpdDate { get; set; }

        public virtual CoCompanyMaster Company { get; set; }
        public virtual HrRcrtPositionProfileType CompanyProfileTypeNavigation { get; set; }
        public virtual HrRcrtPositionProfileType EducationProfileTypeNavigation { get; set; }
        public virtual HrRcrtPositionProfileType ExperienceProfileTypeNavigation { get; set; }
        public virtual HrRcrtPositionProfileType ExpertiseProfileTypeNavigation { get; set; }
        public virtual HrRcrtPositionProfileType JobLevelProfileTypeNavigation { get; set; }
        public virtual HrRcrtPositionProfileType JobTypeProfileTypeNavigation { get; set; }
        public virtual HrRcrtPositionProfileType LanguageProfileTypeNavigation { get; set; }
        public virtual HrRcrtPositionProfileType LocationProfileTypeNavigation { get; set; }
        public virtual HrRcrtRequestStatusMaster RequestStatus { get; set; }
        public virtual ICollection<HrRcrtApplicant> HrRcrtApplicants { get; set; }
        public virtual ICollection<HrRcrtInterview> HrRcrtInterviews { get; set; }
        public virtual ICollection<HrRcrtRequestQuestion> HrRcrtRequestQuestions { get; set; }
        public virtual ICollection<HrRcrtTestRegistration> HrRcrtTestRegistrations { get; set; }
        public virtual ICollection<HrRcrtTestRequest> HrRcrtTestRequests { get; set; }
    }
}
