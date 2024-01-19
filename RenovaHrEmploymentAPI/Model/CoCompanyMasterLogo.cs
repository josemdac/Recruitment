using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class CoCompanyMasterLogo
    {
        public decimal CompanyId { get; set; } 
        public byte[] Logo { get; set; }
        public string LastUpdUserName { get; set; }
        public string LastUpdTerminal { get; set; }
        public DateTime? LastUpdDate { get; set; }
        public byte[] BrandedLogo { get; set; }
        public byte[] CheckLogo { get; set; }

        public virtual CoCompanyMaster Company { get; set; }
    }
}
