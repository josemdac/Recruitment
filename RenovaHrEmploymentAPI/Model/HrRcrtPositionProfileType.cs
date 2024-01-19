using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class HrRcrtPositionProfileType
    {
        public HrRcrtPositionProfileType()
        {
            HrRcrtPositionRequestCompanyProfileTypeNavigations = new HashSet<HrRcrtPositionRequest>();
            HrRcrtPositionRequestEducationProfileTypeNavigations = new HashSet<HrRcrtPositionRequest>();
            HrRcrtPositionRequestExperienceProfileTypeNavigations = new HashSet<HrRcrtPositionRequest>(); 
            HrRcrtPositionRequestExpertiseProfileTypeNavigations = new HashSet<HrRcrtPositionRequest>();
            HrRcrtPositionRequestJobLevelProfileTypeNavigations = new HashSet<HrRcrtPositionRequest>();
            HrRcrtPositionRequestJobTypeProfileTypeNavigations = new HashSet<HrRcrtPositionRequest>();
            HrRcrtPositionRequestLanguageProfileTypeNavigations = new HashSet<HrRcrtPositionRequest>();
            HrRcrtPositionRequestLocationProfileTypeNavigations = new HashSet<HrRcrtPositionRequest>();
        }

        public decimal CompanyId { get; set; }
        public decimal TypeId { get; set; }
        public string ProfileType { get; set; }
        public string SpanishDescription { get; set; }
        public string EnglishDescription { get; set; }
        public string LastUpdUserName { get; set; }
        public string LastUpdTerminal { get; set; }
        public DateTime? LastUpdDate { get; set; }

        public virtual CoCompanyMaster Company { get; set; }
        public virtual ICollection<HrRcrtPositionRequest> HrRcrtPositionRequestCompanyProfileTypeNavigations { get; set; }
        public virtual ICollection<HrRcrtPositionRequest> HrRcrtPositionRequestEducationProfileTypeNavigations { get; set; }
        public virtual ICollection<HrRcrtPositionRequest> HrRcrtPositionRequestExperienceProfileTypeNavigations { get; set; }
        public virtual ICollection<HrRcrtPositionRequest> HrRcrtPositionRequestExpertiseProfileTypeNavigations { get; set; }
        public virtual ICollection<HrRcrtPositionRequest> HrRcrtPositionRequestJobLevelProfileTypeNavigations { get; set; }
        public virtual ICollection<HrRcrtPositionRequest> HrRcrtPositionRequestJobTypeProfileTypeNavigations { get; set; }
        public virtual ICollection<HrRcrtPositionRequest> HrRcrtPositionRequestLanguageProfileTypeNavigations { get; set; }
        public virtual ICollection<HrRcrtPositionRequest> HrRcrtPositionRequestLocationProfileTypeNavigations { get; set; }
    }

    public class HrRcrtPositionProfileTypeSimple
    {

        public decimal TypeId { get; set; }
        public string ProfileType { get; set; }
        public string SpanishDescription { get; set; }
        public string EnglishDescription { get; set; }
    }
}
