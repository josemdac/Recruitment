using System;
using System.Collections.Generic;
using System.Text;

namespace RenovaHrEmploymentAPI.DTO
{
    public class HrRcrtPositionRequestListStateDTO
    {
        public string KeyWord { get; set; } 
        public string Location { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public string Lang { get; set; }

        public IList<string>? JobTypes { get; set; }
        public IList<SortDescriptor>? Sort { get; set; }

    }

    public class SortDescriptor
    {
        public string Field { get; set; }
        public string Dir { get; set; }
    }
}