using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class HrRcrtApplicantQuery
    {
        public decimal QueryId { get; set; }
        public decimal CompanyId { get; set; } 
        public decimal UserId { get; set; }
        public DateTime RunDate { get; set; }
        public string QueryStatement { get; set; }
        public string Status { get; set; }

        public virtual CoCompanyMaster Company { get; set; }
    }
}
