using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class HrRcrtTestRequest
    {
        public decimal TestId { get; set; }
        public decimal RequestId { get; set; }
        public string Status { get; set; }
        public string LastUpdUserName { get; set; } 
        public string LastUpdTerminal { get; set; }
        public DateTime? LastUpdDate { get; set; }

        public virtual HrRcrtPositionRequest Request { get; set; }
        public virtual HrRcrtTestRecord Test { get; set; }
    }
}
