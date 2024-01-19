using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class CoReportsService
    {
        public decimal ServiceId { get; set; }
        public decimal CompanyId { get; set; }
        public string SmtpServer { get; set; }
        public decimal? Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public string LastUpdUserName { get; set; }
        public string LastUpdTerminal { get; set; }
        public DateTime? LastUpdDate { get; set; }
        public string FromEmail { get; set; }
    }
}
