using System;
using System.Collections.Generic;
using System.Text;

namespace RenovaHrEmploymentAPI.DTO
{
    public class HrRcrtUserEmploymentHistListItemDTO
    {
        public decimal EmploymentId { get; set; }
        public string PositionTitle { get; set; }
        public string CompanyName { get; set; }
        public string SupervisorName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Telephone { get; set; }
        public string JobDescription { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? StartSalary { get; set; }
        public decimal? EndSalary { get; set; }
        public string CurrentJob { get; set; }
        public string TerminationReason { get; set; }
        public string Comments { get; set; }

    }

    public class HrRcrtUserEmploymentHistCreateDTO
    {
        public string PositionTitle { get; set; }
        public string CompanyName { get; set; }
        public string SupervisorName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Telephone { get; set; }
        public string JobDescription { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? StartSalary { get; set; }
        public decimal? EndSalary { get; set; }
        public string CurrentJob { get; set; }
        public string TerminationReason { get; set; }
        public string Comments { get; set; }
    }

    public class HrRcrtUserEmploymentHistUpdateDTO : HrRcrtUserEmploymentHistListItemDTO
    {
      
    }
}