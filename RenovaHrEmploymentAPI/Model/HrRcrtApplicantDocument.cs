using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class HrRcrtApplicantDocument
    {
        public decimal DocumentId { get; set; }
        public decimal ApplicantId { get; set; }
        public decimal CompanyId { get; set; }
        public string Status { get; set; }
        public byte[] Document { get; set; } 
        public string DocumentType { get; set; }

        public virtual CoCompanyMaster Company { get; set; }
    }
}
