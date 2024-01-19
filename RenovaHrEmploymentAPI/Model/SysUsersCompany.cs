using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class SysUsersCompany
    {
        public decimal RecordId { get; set; }
        public decimal UserId { get; set; }
        public decimal CompanyId { get; set; }
        public DateTime? EffectiveFromDate { get; set; }
        public DateTime? EffectiveToDate { get; set; }
        public string BusinessUnit1CodeFilter { get; set; }
        public string BusinessUnit2CodeFilter { get; set; }
        public string BusinessUnit3CodeFilter { get; set; }
        public string BusinessUnit4CodeFilter { get; set; }
        public string BusinessUnit5CodeFilter { get; set; }
        public string BusinessUnit6CodeFilter { get; set; }
        public string StatusFilter { get; set; }
        public string PayFrequencyFilter { get; set; }
        public string SupervisorIdFilter { get; set; }
        public string TimekeeperIdFilter { get; set; }
        public string PayrollRuleIdFilter { get; set; }
        public string LocationIdFilter { get; set; }
        public string PositionIdFilter { get; set; }
        public string Type1IdFilter { get; set; }
        public string Type2IdFilter { get; set; }
        public string Type3IdFilter { get; set; }
        public string BusinessUnit1CodeIncExc { get; set; }
        public string BusinessUnit2CodeIncExc { get; set; }
        public string BusinessUnit3CodeIncExc { get; set; }
        public string BusinessUnit4CodeIncExc { get; set; }
        public string BusinessUnit5CodeIncExc { get; set; }
        public string BusinessUnit6CodeIncExc { get; set; }
        public string StatusIncExc { get; set; }
        public string PayFrequencyIncExc { get; set; }
        public string SupervisorIdIncExc { get; set; }
        public string TimekeeperIdIncExc { get; set; }
        public string PayrollRuleIdIncExc { get; set; }
        public string LocationIdIncExc { get; set; }
        public string PositionIdIncExc { get; set; }
        public string Type1IdIncExc { get; set; }
        public string Type2IdIncExc { get; set; }
        public string Type3IdIncExc { get; set; }
        public string LastUpdUserName { get; set; }
        public string LastUpdTerminal { get; set; }
        public DateTime? LastUpdDate { get; set; }
        public decimal? RoleId { get; set; }
        public string PrEditEarningAmount { get; set; }
        public string PrEditBusinessUnit { get; set; }
        public string PrEditPosition { get; set; }
        public string PrEditRate { get; set; }
        public string PrEditVoluntaryDedAmount { get; set; }
        public string PrEditStatutoryDedAmount { get; set; }
        public string PrEditEmployeerDedAmount { get; set; }
        public string PrEditState { get; set; }
        public string PrEditPayrollTaxParameters { get; set; }
        public string PrEditPayrollWithholding { get; set; }
        public string ViewRestricted { get; set; }
        public string VoidDeleteChecks { get; set; }
        public string ShowTaAmountColumn { get; set; }
        public string ByPassArchiveTimecardLock { get; set; }
        public string UseCategoryFilterTimecard { get; set; }
        public string UseLeaveFilterTimecard { get; set; }
        public string EmployeeFilter { get; set; }
        public string EmployeeIncExc { get; set; }
    }
}
