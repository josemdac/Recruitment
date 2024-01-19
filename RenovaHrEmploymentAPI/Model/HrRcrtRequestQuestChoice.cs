using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class HrRcrtRequestQuestChoice
    {
        public decimal ChoiceId { get; set; }
        public decimal QuestionId { get; set; }
        public string Choice { get; set; }
        public string Status { get; set; }
        public string LastUpdUserName { get; set; }
        public string LastUpdTerminal { get; set; }
        public DateTime? LastUpdDate { get; set; }

        public virtual HrRcrtRequestQuestion Question { get; set; }
    }
} 
