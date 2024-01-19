using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class SysCountryMaster
    {
        public decimal CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string Status { get; set; }
        public string LastUpdUserName { get; set; }
        public string LastUpdTerminal { get; set; }
        public DateTime? LastUpdDate { get; set; }
    }
}
