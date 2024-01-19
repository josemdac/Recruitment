using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class SysCultureMaster
    {
        public decimal CultureId { get; set; }
        public string CultureCode { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string LastUpdUserName { get; set; }
        public string LastUpdTerminal { get; set; }
        public DateTime? LastUpdDate { get; set; }
    }
}
