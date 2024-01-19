using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class HrRcrtTestResult
    {
        public decimal ResultId { get; set; }
        public decimal RegistrationId { get; set; }
        public decimal QuestionId { get; set; } 
        public decimal? AnswerValue { get; set; }
        public string Status { get; set; }
        public string LastUpdTerminal { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public decimal? AnswerIndex { get; set; }

        public virtual HrRcrtTestQuestion Question { get; set; }
        public virtual HrRcrtTestRegistration Registration { get; set; }
    }
}
