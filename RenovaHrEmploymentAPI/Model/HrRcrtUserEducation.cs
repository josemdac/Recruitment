using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class HrRcrtUserEducation
    {
        public decimal EducationId { get; set; }
        public decimal CompanyId { get; set; }
        public decimal UserId { get; set; }
        public string SchoolName { get; set; }
        public decimal? YearsCompleted { get; set; }
        public string Graduated { get; set; }
        public decimal? GraduatedYear { get; set; }
        public decimal? DegreeId { get; set; } 
        public string Major { get; set; }
        public decimal? Gpa { get; set; }
        public decimal? CountryId { get; set; }
        public string Comments { get; set; }
        public string LastUpdUserName { get; set; }
        public string LastUpdTerminal { get; set; }
        public DateTime? LastUpdDate { get; set; }

        public virtual CoCompanyMaster Company { get; set; }
        public virtual HrRcrtUser User { get; set; }
    }
}
