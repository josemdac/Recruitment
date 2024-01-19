using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class SysUser
    {
        public decimal UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public string FirstLogin { get; set; }
        public string ExpiredPassword { get; set; }
        public decimal? ExpiredDays { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? LastPasswordChange { get; set; }
        public decimal? FailedLoginCount { get; set; }
        public decimal? FailedLoginAnswerCount { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? InactiveDate { get; set; }
        public string Comments { get; set; }
        public decimal DefaultProductId { get; set; }
        public string Status { get; set; }
        public string SocialIdCodePermission { get; set; }
        public string ContactInformationPermission { get; set; }
        public string CompensationPermission { get; set; }
        public string VoluntaryDeductionPermission { get; set; }
        public string OtherCompensationPermision { get; set; }
        public string CreateNewEmployeePermission { get; set; }
        public string LastUpdUserName { get; set; }
        public string LastUpdTerminal { get; set; }
        public DateTime? LastUpdDate { get; set; }
        public decimal DefaultCompanyId { get; set; }
        public string MenuType { get; set; }
        public string DisplayTaHoursDecimals { get; set; }
        public decimal ProcessorId { get; set; }
        public string ShowHrDashboard { get; set; }
        public string ShowPayrollDashboard { get; set; }
        public string ShowTaDashboard { get; set; }
        public string NewTabForReports { get; set; }
        public string LastDevice { get; set; }
        public string LastBrowser { get; set; }
        public string LastIp { get; set; }
        public string LastHostname { get; set; }
        public string LastCity { get; set; }
        public string LastRegion { get; set; }
        public string LastCountry { get; set; }
        public string LastLocation { get; set; }
        public string LastOrg { get; set; }
        public string LastPostal { get; set; }
        public string LastTimezone { get; set; }
        public string InviteStatus { get; set; }
        public DateTime? InviteDate { get; set; }
        public string ExportExcel { get; set; }
    }
}
