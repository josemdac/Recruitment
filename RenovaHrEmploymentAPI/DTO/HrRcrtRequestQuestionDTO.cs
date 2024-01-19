using System;
using System.Collections.Generic;
using System.Text;

namespace RenovaHrEmploymentAPI.DTO
{
    public class HrRcrtRequestQuestionDTO
    {
        public decimal QuestionId { get; set; }
        public decimal RequestId { get; set; }
        public string QuestionType { get; set; }
        public string Question { get; set; }
        public string Status { get; set; }

        public HrRcrtRequestQuestnAnswerDTO[] Answers { get; set; }
    }


}
