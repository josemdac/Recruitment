using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class HrRcrtUserEmploymentHist
    {
        public decimal EmploymentId { get; set; }
        public decimal CompanyId { get; set; }
        public decimal UserId { get; set; }
        public string PositionTitle { get; set; }
        public string CompanyName { get; set; }
        public string SupervisorName { get; set; }
        public string Address1 { get; set; } 
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Telephone { get; set; }
        public string JobDescription { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? StartSalary { get; set; }
        public decimal? EndSalary { get; set; }
        public string CurrentJob { get; set; }
        public string TerminationReason { get; set; }
        public string Comments { get; set; }
        public string LastUpdUserName { get; set; }
        public string LastUpdTerminal { get; set; }
        public DateTime? LastUpdDate { get; set; }

        public virtual CoCompanyMaster Company { get; set; }
        public virtual HrRcrtUser User { get; set; }
    }
}
