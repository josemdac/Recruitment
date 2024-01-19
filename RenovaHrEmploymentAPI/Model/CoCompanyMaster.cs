using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class CoCompanyMaster
    {
        public CoCompanyMaster()
        {
            HrRcrtApplicantDocuments = new HashSet<HrRcrtApplicantDocument>();
            HrRcrtApplicantQueries = new HashSet<HrRcrtApplicantQuery>(); 
            HrRcrtApplicants = new HashSet<HrRcrtApplicant>();
            HrRcrtExternalWebsiteInfos = new HashSet<HrRcrtExternalWebsiteInfo>();
            HrRcrtInterviews = new HashSet<HrRcrtInterview>();
            HrRcrtPositionProfileTypes = new HashSet<HrRcrtPositionProfileType>();
            HrRcrtPositionRequests = new HashSet<HrRcrtPositionRequest>();
            HrRcrtRequestQuestions = new HashSet<HrRcrtRequestQuestion>(); 
            HrRcrtRequestStatusMasters = new HashSet<HrRcrtRequestStatusMaster>();
            HrRcrtTestRecords = new HashSet<HrRcrtTestRecord>();
            HrRcrtUserDocuments = new HashSet<HrRcrtUserDocument>();
            HrRcrtUserEducations = new HashSet<HrRcrtUserEducation>(); 
            HrRcrtUserEmploymentHists = new HashSet<HrRcrtUserEmploymentHist>();
            HrRcrtUserSecurityQuestions = new HashSet<HrRcrtUserSecurityQuestion>();
        }

        public decimal CompanyId { get; set; }
        public string CompanyName { get; set; }
        public decimal CompanyCode { get; set; }
        public string Ein { get; set; }
        public string Status { get; set; }
        public decimal Currency { get; set; }
        public string CryCode { get; set; }
        public decimal? MarketId { get; set; }
        public string LastUpdUserName { get; set; }
        public string LastUpdTerminal { get; set; }
        public DateTime? LastUpdDate { get; set; }
        public string BusinessUnit1Name { get; set; }
        public string BusinessUnit2Name { get; set; }
        public string BusinessUnit3Name { get; set; }
        public string BusinessUnit4Name { get; set; }
        public string BusinessUnit5Name { get; set; }
        public string BusinessUnit6Name { get; set; }
        public string Type1Name { get; set; }
        public string Type2Name { get; set; }
        public string Type3Name { get; set; }
        public string UseJobCodes { get; set; }
        public string GovernmentAgency { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public decimal ProcessorId { get; set; }
        public decimal? TimeoutMinutes { get; set; }
        public string DefaultStateId { get; set; }
        public string DisabilityInsuranceId { get; set; }
        public string LaborDepartmentId { get; set; }
        public string EmployerChaufferId { get; set; }
        public string AgencyCode { get; set; }
        public string AgencyAeelaCode { get; set; }
        public string ConstantAeelaCode { get; set; }
        public string BusinessModel { get; set; }
        public string UseLdapAuthentication { get; set; }
        public string LdapServer1 { get; set; }
        public string LdapServer2 { get; set; }
        public string LdapServer3 { get; set; }
        public string LdapGroup { get; set; }
        public string LdapDomain1 { get; set; }
        public string LdapDomain2 { get; set; }
        public string LdapDomain3 { get; set; }
        public string DisabilityInsurerName { get; set; }
        public string DisabilityInsurerAddress { get; set; }
        public string DisabilityInsurerAccount { get; set; }
        public decimal? TimeZoneId { get; set; }
        public string AnalyticsDbid { get; set; }
        public string AnalyticsGenId { get; set; }
        public string AnalyticsPrId { get; set; }
        public string AnalyticsHrId { get; set; }
        public string AnalyticsTaId { get; set; }
        public decimal? CultureId { get; set; }
        public string DefaultPayFrequency { get; set; }
        public decimal? DefaultStandardHrsPeriod { get; set; }
        public decimal? DefaultStandardHrsDay { get; set; }
        public string LmsToken { get; set; }
        public decimal? DefaultStandardHrsWeek { get; set; }
        public decimal? ParentCompanyId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string StateTaxIdentification { get; set; }
        public decimal? MinimumStateSalary { get; set; }

        public virtual CoCompanyMasterLogo CoCompanyMasterLogo { get; set; }
        public virtual HrRcrtCompanyConfiguration HrRcrtCompanyConfiguration { get; set; }
        public virtual ICollection<HrRcrtApplicantDocument> HrRcrtApplicantDocuments { get; set; }
        public virtual ICollection<HrRcrtApplicantQuery> HrRcrtApplicantQueries { get; set; }
        public virtual ICollection<HrRcrtApplicant> HrRcrtApplicants { get; set; }
        public virtual ICollection<HrRcrtExternalWebsiteInfo> HrRcrtExternalWebsiteInfos { get; set; }
        public virtual ICollection<HrRcrtInterview> HrRcrtInterviews { get; set; }
        public virtual ICollection<HrRcrtPositionProfileType> HrRcrtPositionProfileTypes { get; set; }
        public virtual ICollection<HrRcrtPositionRequest> HrRcrtPositionRequests { get; set; }
        public virtual ICollection<HrRcrtRequestQuestion> HrRcrtRequestQuestions { get; set; }
        public virtual ICollection<HrRcrtRequestStatusMaster> HrRcrtRequestStatusMasters { get; set; }
        public virtual ICollection<HrRcrtTestRecord> HrRcrtTestRecords { get; set; }
        public virtual ICollection<HrRcrtUserDocument> HrRcrtUserDocuments { get; set; }
        public virtual ICollection<HrRcrtUserEducation> HrRcrtUserEducations { get; set; }
        public virtual ICollection<HrRcrtUserEmploymentHist> HrRcrtUserEmploymentHists { get; set; }
        public virtual ICollection<HrRcrtUserSecurityQuestion> HrRcrtUserSecurityQuestions { get; set; }
    }
}
