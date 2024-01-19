using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class HrRcrtUserDocument
    {
        public decimal DocumentId { get; set; }
        public decimal UserId { get; set; }
        public decimal CompanyId { get; set; }
        public string DocumentTitle { get; set; }
        public string DocumentName { get; set; } 
        public string DocumentType { get; set; }
        public string DocumentFormat { get; set; }
        public byte[] Document { get; set; }
        public string Status { get; set; }

        public virtual CoCompanyMaster Company { get; set; }
        public virtual HrRcrtUser User { get; set; }
    }
}
