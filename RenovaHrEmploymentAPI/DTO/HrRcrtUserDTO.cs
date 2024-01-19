using System;
using System.Collections.Generic;
using System.Text;

namespace RenovaHrEmploymentAPI.DTO
{
    public class HrRcrtUserUpdateDTO
    {
        public string UserName { get; set; }
        public string Email2 { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName1 { get; set; }
        public string LastName2 { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Address1Street { get; set; }
        public string Address2Street { get; set; }
        public string CityStreet { get; set; }
        public string StateStreet { get; set; }
        public string ZipcodeStreet { get; set; }
        public string Telephone { get; set; }
        public string EmailFormat { get; set; }
        public string EmailFrequency { get; set; }
        public string Comments { get; set; }
        public string RemainAnonymous { get; set; }
        public string SendFutureInformation { get; set; }
        public string SendEmailMatching { get; set; }
        public string MobileTelephone { get; set; }
        public string SocialNetworkAddress1 { get; set; }
        public string SocialNetworkAddress2 { get; set; }
        public string SocialNetworkAddress3 { get; set; }
    }

    public class HrRcrtUserCreateDTO
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName1 { get; set; }
        public string LastName2 { get; set; }
        public string State { get; set; }
        public string Telephone { get; set; }
        public string RemainAnonymous { get; set; }
        public string SendFutureInformation { get; set; }
        public string SendEmailMatching { get; set; }
        public string MobileTelephone { get; set; }
        public string Password { get; set; }

    }

    public class HrRcrtUserResetPasswordDTO
    {
        public string UserName { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
    public class HrRcrtUserCurrentDTO:HrRcrtUserUpdateDTO
    {
        
    }
    public class HrRcrtUserPassValidateDTO
    {
        public string Password { get; set; }
    }
    public class HrRcrtUserPassChangeDTO
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
