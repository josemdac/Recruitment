using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class HrRcrtApplicant
    {
        public decimal ApplicantId { get; set; }
        public decimal CompanyId { get; set; }
        public decimal RequestId { get; set; }
        public DateTime RequestDate { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName1 { get; set; }
        public string LastName2 { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Telephone { get; set; }
        public string OfccpDisability { get; set; }
        public string OfccpGender { get; set; }
        public string OfccpVeteran { get; set; }
        public string OfccpEthnicity { get; set; }
        public decimal? ApplicantStatus { get; set; }
        public string MobileTelephone { get; set; }
        public string SocialNetworkAddress1 { get; set; }
        public string SocialNetworkAddress2 { get; set; }
        public string SocialNetworkAddress3 { get; set; }
        public string MondayAm { get; set; }
        public string MondayPm { get; set; }
        public string TuesdayAm { get; set; }
        public string TuesdayPm { get; set; }
        public string WednesdayAm { get; set; }
        public string WednesdayPm { get; set; }
        public string ThursdayAm { get; set; }
        public string ThursdayPm { get; set; }
        public string FridayAm { get; set; }
        public string FridayPm { get; set; }
        public string SaturdayAm { get; set; }
        public string SaturdayPm { get; set; }
        public string SundayAm { get; set; }
        public string SundayPm { get; set; }
        public string Address1Street { get; set; }
        public string Address2Street { get; set; }
        public string CityStreet { get; set; }
        public string StateStreet { get; set; }
        public string ZipcodeStreet { get; set; }
        public string MorningShift { get; set; }
        public ICollection<HrRcrtInterview> HrRcrtInterviews { get; set; }
        public HrRcrtPositionRequest HrRcrtPositionRequest { get; set; }
        public CoCompanyMaster Company { get; set; }
    }
}
