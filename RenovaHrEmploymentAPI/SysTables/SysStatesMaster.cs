using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class SysStatesMaster
    {
        public SysStatesMaster()
        {
            SysZipcodes = new HashSet<SysZipcode>();
        }

        public string StateId { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string LastUpdUserName { get; set; }
        public string LastUpdTerminal { get; set; }
        public DateTime? LastUpdDate { get; set; }

        public virtual ICollection<SysZipcode> SysZipcodes { get; set; }
    }
}
