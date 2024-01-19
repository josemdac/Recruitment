using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class SysZipcode
    {
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string County { get; set; }
        public string Areacode { get; set; }
        public string Timezone { get; set; }
        public string Stateansi { get; set; }
        public string Countyansi { get; set; }
        public string LastUpdUserName { get; set; }
        public string LastUpdTerminal { get; set; }
        public DateTime? LastUpdDate { get; set; }

        public virtual SysStatesMaster StateNavigation { get; set; }
    }
}
