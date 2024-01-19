using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class SysCurrency
    {
        public decimal CurrencyId { get; set; }
        public string CurrencyCode { get; set; }
        public string CountryCurrency { get; set; }
        public string Symbol { get; set; }
        public string CultureCode { get; set; }
        public decimal? CurrencyConvertion { get; set; }
        public string LastUpdUserName { get; set; }
        public string LastUpdTerminal { get; set; }
        public DateTime? LastUpdDate { get; set; }
    }
}
