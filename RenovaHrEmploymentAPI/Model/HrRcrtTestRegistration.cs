using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class HrRcrtTestRegistration
    {
        public HrRcrtTestRegistration()
        {
            HrRcrtTestResults = new HashSet<HrRcrtTestResult>();
        }

        public decimal RegistrationId { get; set; } 
        public decimal TestId { get; set; }
        public decimal ApplicantId { get; set; }
        public decimal RequestId { get; set; }
        public decimal? Score { get; set; }
        public string ApplicantFullName { get; set; }
        public string Comments { get; set; }
        public string Status { get; set; }
        public string LastUpdTerminal { get; set; }
        public DateTime? RegistrationDate { get; set; }

        public virtual HrRcrtPositionRequest Request { get; set; }
        public virtual HrRcrtTestRecord Test { get; set; }
        public virtual ICollection<HrRcrtTestResult> HrRcrtTestResults { get; set; }
    }
}
