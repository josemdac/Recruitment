using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class HrRcrtAudit
    {
        public decimal AuditId { get; set; }
        public decimal CompanyId { get; set; }
        public string UserName { get; set; }
        public DateTime ActionDate { get; set; }
        public string ActionDescription { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string Terminal { get; set; } 
    }
}
