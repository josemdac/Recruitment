using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class HrRcrtTestQuestion
    {
        public HrRcrtTestQuestion()
        {
            HrRcrtTestAnswers = new HashSet<HrRcrtTestAnswer>();
            HrRcrtTestResults = new HashSet<HrRcrtTestResult>();
        }

        public decimal QuestionId { get; set; }
        public decimal TestId { get; set; }
        public string Question { get; set; } 
        public decimal QuestionOrder { get; set; }
        public decimal QuestionValue { get; set; }
        public string Status { get; set; }
        public string LastUpdUserName { get; set; }
        public string LastUpdTerminal { get; set; }
        public DateTime? LastUpdDate { get; set; }

        public virtual HrRcrtTestRecord Test { get; set; }
        public virtual ICollection<HrRcrtTestAnswer> HrRcrtTestAnswers { get; set; }
        public virtual ICollection<HrRcrtTestResult> HrRcrtTestResults { get; set; }
    }
}
