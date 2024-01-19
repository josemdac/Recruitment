using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class HrRcrtUserLanguage
    {
        public decimal RecordId { get; set; }
        public decimal CompanyId { get; set; }
        public decimal UserId { get; set; }
        public decimal LanguageId { get; set; }
        public string SpeakingProficiency { get; set; }
        public string ReadingProficiency { get; set; }
        public string WritingProficiency { get; set; }
        public string LastUpdUserName { get; set; }
        public string LastUpdTerminal { get; set; }
        public DateTime? LastUpdDate { get; set; }
    }
}
