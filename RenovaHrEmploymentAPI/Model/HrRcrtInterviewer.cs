using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class HrRcrtInterviewer
    {
        public HrRcrtInterviewer()
        {
            HrRcrtInterviews = new HashSet<HrRcrtInterview>();
        } 

        public decimal InterviewerId { get; set; }
        public decimal CompanyId { get; set; }
        public string Telephone { get; set; }
        public string Status { get; set; }
        public decimal UserId { get; set; }

        public virtual ICollection<HrRcrtInterview> HrRcrtInterviews { get; set; }
    }
}
