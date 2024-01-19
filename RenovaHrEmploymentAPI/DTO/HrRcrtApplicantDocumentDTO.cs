using System;
using System.Collections.Generic;
using System.Text;

namespace RenovaHrEmploymentAPI.DTO
{
    public class HrRcrtApplicantDocumentListItemDTO
    {
        public decimal DocumentId { get; set; }
        public decimal ApplicantId { get; set; }
        
        public string Status { get; set; }
        public string DocumentType { get; set; }

        public bool HasFile { get; set; }

    }

    public class HrRcrtApplicantDocumentCreateDTO
    {
        
        
        public string Status { get; set; }
        public byte[] Document { get; set; }
        public string DocumentType { get; set; }
    }

    public class HrRcrtApplicantDocumentUpdateDTO 
    {
        public decimal DocumentId { get; set; }
        
        
        public string Status { get; set; }
        public byte[] Document { get; set; }
        public string DocumentType { get; set; }
    }
}