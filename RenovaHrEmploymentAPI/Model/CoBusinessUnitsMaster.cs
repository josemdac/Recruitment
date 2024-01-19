using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class CoBusinessUnitsMaster
    {
        public decimal CompanyId { get; set; }
        public decimal BusinessUnitId { get; set; }
        public decimal? BusinessUnitOrder { get; set; }
        public string BusinessUnit1Code { get; set; }
        public string BusinessUnit2Code { get; set; }
        public string BusinessUnit3Code { get; set; }
        public string BusinessUnit4Code { get; set; } 
        public string BusinessUnit5Code { get; set; }
        public string BusinessUnit6Code { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string LastUpdUserName { get; set; }
        public string LastUpdTerminal { get; set; }
        public DateTime? LastUpdDate { get; set; }
        public decimal? Type1Id { get; set; }
        public decimal? Type2Id { get; set; }
        public decimal? Type3Id { get; set; }
        public decimal? CostCenterId { get; set; }
        public string Refference { get; set; }
        public string LaborCode { get; set; }
        public decimal? LocationId { get; set; }
        public decimal? PayrollRuleId { get; set; }
        public string TaxDisabilityInsurance { get; set; }
        public decimal? TimekeeperId { get; set; }
        public decimal? PositionId { get; set; }
    }
}
