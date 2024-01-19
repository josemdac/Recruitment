using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class SysLanguage
    {
        public decimal LanguageId { get; set; }
        public string Language { get; set; }
        public string Status { get; set; }
        public string LastUpdUserName { get; set; }
        public string LastUpdTerminal { get; set; }
        public DateTime? LastUpdDate { get; set; }
        public string Culture { get; set; }
    }
}
