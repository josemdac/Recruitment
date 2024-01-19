using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class HrRcrtOfccpText
    {
        public decimal OfccpId { get; set; }
        public string OfccpVeteranSpanish { get; set; } 
        public string OfccpVeteranEnglish { get; set; }
        public string OfccpGenderSpanish { get; set; }
        public string OfccpGenderEnglish { get; set; }
        public string OfccpDisabilitySpanish { get; set; }
        public string OfccpDisabilityEnglish { get; set; }
        public string OfccpEthnicSpanish { get; set; }
        public string OfccpEthnicEnglish { get; set; }
        public string LastUpdUserName { get; set; }
        public string LastUpdTerminal { get; set; }
        public DateTime? LastUpdDate { get; set; }
    }
}
