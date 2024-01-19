using System;
using System.Collections.Generic;
using System.Text;

namespace RenovaHrEmploymentAPI.Model
{
    public class Session
    {
        public string LoggedOn { get; set; }
        public decimal UserId { get; set; }

        public string IpAddress { get; set; }

        public string UserAgent { get; set; }
    }
    
}
