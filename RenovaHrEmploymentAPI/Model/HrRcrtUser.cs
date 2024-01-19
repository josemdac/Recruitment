using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class HrRcrtUser
    {
        public decimal UserId { get; set; }
        public decimal CompanyId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
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
        public DateTime? LastLogin { get; set; }
        public DateTime? LastPasswordChange { get; set; }
        public string EmailFormat { get; set; }
        public string EmailFrequency { get; set; }
        public decimal? FailedLoginCount { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? InactiveDate { get; set; }
        public string Comments { get; set; }
        public string Status { get; set; }
        public string RemainAnonymous { get; set; }
        public string SendFutureInformation { get; set; }
        public string SendEmailMatching { get; set; }
        public string MobileTelephone { get; set; }
        public string SocialNetworkAddress1 { get; set; }
        public string SocialNetworkAddress2 { get; set; }
        public string SocialNetworkAddress3 { get; set; }
        public string Email2 { get; set; }
        public string Address1Street { get; set; }
        public string Address2Street { get; set; }
        public string CityStreet { get; set; }
        public string StateStreet { get; set; }
        public string ZipcodeStreet { get; set; }
        public ICollection<HrRcrtUserDocument> HrRcrtUserDocuments { get; set; }
        public ICollection<HrRcrtUserEducation> HrRcrtUserEducations { get; set; }
        public ICollection<HrRcrtUserEmploymentHist> HrRcrtUserEmploymentHists { get; set; }
        public ICollection<HrRcrtUserSecurityQuestion> HrRcrtUserSecurityQuestions { get; set; }
        
    }

    public class HrRcrtUserActivation
    {
        public DateTime Created { get; set; }
        public decimal UserId { get; set; }
        public string ActType { get; set; }
    }
}
