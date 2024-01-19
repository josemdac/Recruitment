using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class HrRcrtTestRecord
    {
        public HrRcrtTestRecord()
        {
            HrRcrtTestQuestions = new HashSet<HrRcrtTestQuestion>();
            HrRcrtTestRegistrations = new HashSet<HrRcrtTestRegistration>();
            HrRcrtTestRequests = new HashSet<HrRcrtTestRequest>();
        } 

        public decimal TestId { get; set; }
        public decimal UserId { get; set; }
        public decimal CompanyId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public decimal? TimePerQuestion { get; set; }
        public string Status { get; set; }
        public string LastUpdUserName { get; set; }
        public string LastUpdTerminal { get; set; }
        public DateTime? LastUpdDate { get; set; }
        public decimal? ExpirationDays { get; set; }
        public decimal? MinimumScore { get; set; }
        public decimal? TimePerTest { get; set; }
        public string TimerBy { get; set; }

        public virtual CoCompanyMaster Company { get; set; }
        public virtual ICollection<HrRcrtTestQuestion> HrRcrtTestQuestions { get; set; }
        public virtual ICollection<HrRcrtTestRegistration> HrRcrtTestRegistrations { get; set; }
        public virtual ICollection<HrRcrtTestRequest> HrRcrtTestRequests { get; set; }
    }
}
