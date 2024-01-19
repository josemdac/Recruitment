using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class HrRcrtRequestStatusMaster
    {
        public HrRcrtRequestStatusMaster()
        {
            HrRcrtPositionRequests = new HashSet<HrRcrtPositionRequest>();
        }

        public decimal StatusId { get; set; }
        public decimal CompanyId { get; set; }
        public string TypeCode { get; set; }
        public string SpanishDescription { get; set; }
        public string EnglishDescription { get; set; }
        public string LastUpdUserName { get; set; }
        public string LastUpdTerminal { get; set; } 
        public DateTime? LastUpdDate { get; set; }

        public virtual CoCompanyMaster Company { get; set; }
        public virtual ICollection<HrRcrtPositionRequest> HrRcrtPositionRequests { get; set; }
    }
}
