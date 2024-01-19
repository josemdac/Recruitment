using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class HrRcrtExternalWebsiteInfo
    {
        public decimal ConfigurationId { get; set; }
        public decimal CompanyId { get; set; }
        public string PageName { get; set; }
        public string Title1English { get; set; }
        public string Title2English { get; set; } 
        public string Title3English { get; set; }
        public string Title4English { get; set; }
        public string Title1Spanish { get; set; }
        public string Title2Spanish { get; set; }
        public string Title3Spanish { get; set; }
        public string Title4Spanish { get; set; }
        public string Text1English { get; set; }
        public string Text2English { get; set; }
        public string Text3English { get; set; }
        public string Text4English { get; set; }
        public string Text1Spanish { get; set; }
        public string Text2Spanish { get; set; }
        public string Text3Spanish { get; set; }
        public string Text4Spanish { get; set; }
        public byte[] Image1 { get; set; }
        public byte[] Image2 { get; set; }
        public byte[] Image3 { get; set; }
        public byte[] Image4 { get; set; }
        public string LastUpdUserName { get; set; }
        public string LastUpdTerminal { get; set; }
        public DateTime? LastUpdDate { get; set; }

        public virtual CoCompanyMaster Company { get; set; }
    }
}
