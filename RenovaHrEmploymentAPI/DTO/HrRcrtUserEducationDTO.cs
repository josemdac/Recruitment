using System;
using System.Collections.Generic;
using System.Text;

namespace RenovaHrEmploymentAPI.DTO
{
    public class HrRcrtUserEducationListItemDTO
    {
        public decimal EducationId { get; set; }

        public string SchoolName { get; set; }
        public decimal? YearsCompleted { get; set; }
        public string Graduated { get; set; }
        public decimal? GraduatedYear { get; set; }
        public decimal? DegreeId { get; set; }
        public string Major { get; set; }
        public decimal? Gpa { get; set; }
        public decimal? CountryId { get; set; }
        public string Comments { get; set; }

    }

    public class HrRcrtUserEducationCreateDTO
    {

        public string SchoolName { get; set; }
        public decimal? YearsCompleted { get; set; }
        public string Graduated { get; set; }
        public decimal? GraduatedYear { get; set; }
        public decimal? DegreeId { get; set; }
        public string Major { get; set; }
        public decimal? Gpa { get; set; }
        public decimal? CountryId { get; set; }
        public string Comments { get; set; }
    }

    public class HrRcrtUserEducationUpdateDTO : HrRcrtUserEducationListItemDTO
    {
      
    }
}