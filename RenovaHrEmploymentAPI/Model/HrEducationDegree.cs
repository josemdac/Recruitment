using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class HrEducationDegree
    {
        public decimal DegreeId { get; set; }
        public decimal CompanyId { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string LastUpdUserName { get; set; }
        public string LastUpdTerminal { get; set; }
        public DateTime? LastUpdDate { get; set; }
        public decimal? Weight { get; set; }
    }
}
