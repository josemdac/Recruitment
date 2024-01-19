using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class HrRcrtUsersProfile
    {
        public decimal UserId { get; set; }
        public decimal CompanyId { get; set; }
        public string ProfileType { get; set; } 
        public decimal TypeId { get; set; }
        public string LastUpdUserName { get; set; }
        public string LastUpdTerminal { get; set; }
        public DateTime? LastUpdDate { get; set; }
        public decimal ProfileId { get; set; }
    }
}
