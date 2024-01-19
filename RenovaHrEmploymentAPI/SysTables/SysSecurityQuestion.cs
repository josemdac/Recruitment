using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class SysSecurityQuestion
    {
        public decimal QuestionId { get; set; }
        public string Question { get; set; }
        public string Status { get; set; }
        public string LastUpdUserName { get; set; }
        public string LastUpdTerminal { get; set; }
        public DateTime? LastUpdDate { get; set; }
        public string QuestionEnglish { get; set; }
    }
}
