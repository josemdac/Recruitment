using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class HrRcrtRequestQuestnAnswer
    {
        public decimal AnswerId { get; set; }
        public decimal QuestionId { get; set; }
        public decimal ApplicantId { get; set; }
        public string Answer { get; set; }
        public string LastUpdUserName { get; set; }
        public string LastUpdTerminal { get; set; }
        public DateTime? LastUpdDate { get; set; } 

        public virtual HrRcrtRequestQuestion Question { get; set; }
    }
}
