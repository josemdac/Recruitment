using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class HrRcrtTestAnswer
    {
        public decimal AnswerId { get; set; }
        public decimal QuestionId { get; set; }
        public string Answer { get; set; }
        public decimal AnswerValue { get; set; }
        public string Status { get; set; }
        public string LastUpdUserName { get; set; }
        public string LastUpdTerminal { get; set; }
        public DateTime? LastUpdDate { get; set; } 

        public virtual HrRcrtTestQuestion Question { get; set; }
    }
}
