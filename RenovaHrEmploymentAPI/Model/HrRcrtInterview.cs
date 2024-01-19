using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class HrRcrtInterview
    {
        public decimal InterviewId { get; set; }
        public decimal CompanyId { get; set; }
        public decimal ApplicantId { get; set; }
        public decimal RequestId { get; set; }
        public decimal InterviewerId { get; set; }
        public DateTime InterviewFromDate { get; set; } 
        public decimal? Rating { get; set; }
        public string Location { get; set; }
        public string Notes { get; set; }
        public string Comments { get; set; }
        public byte[] Attachment { get; set; }
        public string FileName { get; set; }
        public string Status { get; set; }
        public DateTime? InterviewToDate { get; set; }

        public virtual HrRcrtApplicant Applicant { get; set; }
        public virtual CoCompanyMaster Company { get; set; }
        public virtual HrRcrtInterviewer Interviewer { get; set; }
        public virtual HrRcrtPositionRequest Request { get; set; }
    }
}
