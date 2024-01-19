using System;
using System.Collections.Generic;
using System.Text;

namespace RenovaHrEmploymentAPI.DTO
{
    public class HrRcrtUserDocumentListItemDTO
    {
        public decimal DocumentId { get; set; }
        public string DocumentTitle { get; set; }
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public string DocumentFormat { get; set; }
        public string Status { get; set; }

    }

    public class HrRcrtUserDocumentCreateDTO
    {
        public string DocumentTitle { get; set; }
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public string DocumentFormat { get; set; }
        public string Status { get; set; }
        public byte[] Document { get; set; }
    }

    public class HrRcrtUserDocumentUpdateDTO : HrRcrtUserDocumentListItemDTO
    {
        public byte[] Document { get; set; }
    }
}