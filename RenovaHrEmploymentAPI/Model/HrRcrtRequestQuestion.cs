using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class HrRcrtRequestQuestion
    {
        public HrRcrtRequestQuestion()
        {
            HrRcrtRequestQuestChoices = new HashSet<HrRcrtRequestQuestChoice>();
            HrRcrtRequestQuestnAnswers = new HashSet<HrRcrtRequestQuestnAnswer>();
        }

        public decimal QuestionId { get; set; }
        public decimal CompanyId { get; set; }
        public decimal RequestId { get; set; } 
        public string QuestionType { get; set; }
        public string Question { get; set; }
        public string Status { get; set; }
        public string LastUpdUserName { get; set; }
        public string LastUpdTerminal { get; set; }
        public DateTime? LastUpdDate { get; set; }

        public virtual CoCompanyMaster Company { get; set; }
        public virtual HrRcrtPositionRequest Request { get; set; }
        public virtual ICollection<HrRcrtRequestQuestChoice> HrRcrtRequestQuestChoices { get; set; }
        public virtual ICollection<HrRcrtRequestQuestnAnswer> HrRcrtRequestQuestnAnswers { get; set; }
    }
}
