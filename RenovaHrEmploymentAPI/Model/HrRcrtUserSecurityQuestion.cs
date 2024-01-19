using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class HrRcrtUserSecurityQuestion
    {
        public decimal SecurityId { get; set; }
        public decimal CompanyId { get; set; }
        public decimal UserId { get; set; } 
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Status { get; set; }
        public string LastUpdUserName { get; set; }
        public string LastUpdTerminal { get; set; }
        public DateTime? LastUpdDate { get; set; }

        public virtual CoCompanyMaster Company { get; set; }
        public virtual HrRcrtUser User { get; set; }
    }
}
