using System;
using System.Collections.Generic;
using System.Text;

namespace RenovaHrEmploymentAPI.DTO
{
    public class HrRcrtUserLanguageListItemDTO
    {
        public decimal RecordId { get; set; }
        public decimal LanguageId { get; set; }
        public string SpeakingProficiency { get; set; }
        public string ReadingProficiency { get; set; }
        public string WritingProficiency { get; set; }

    }

    public class HrRcrtUserLanguageCreateDTO
    {

        public decimal LanguageId { get; set; }
        public string SpeakingProficiency { get; set; }
        public string ReadingProficiency { get; set; }
        public string WritingProficiency { get; set; }
    }

    public class HrRcrtUserLanguageUpdateDTO : HrRcrtUserLanguageListItemDTO
    {
      
    }
}