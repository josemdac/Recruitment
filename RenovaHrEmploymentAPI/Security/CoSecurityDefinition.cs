using System;
using System.Collections.Generic;

#nullable disable

namespace RenovaHrEmploymentAPI
{
    public partial class CoSecurityDefinition
    {
        public decimal SecurityId { get; set; }
        public decimal CompanyId { get; set; }
        public decimal? PasswordLength { get; set; }
        public decimal? ChangeEveryDays { get; set; }
        public string CanRepeatePassword { get; set; }
        public decimal? MinLowerCharacters { get; set; }
        public decimal? MinUpperCharacters { get; set; }
        public decimal? MinNumericCharacters { get; set; }
        public decimal? MinSymbolCharacters { get; set; }
        public string ReqUpperLowerCharacters { get; set; }
        public string PasswordCanBeTheUsername { get; set; }
    }
}
