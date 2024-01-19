using System;
using System.Collections.Generic;
using System.Text;

namespace RenovaHrEmploymentAPI.DTO
{
    public class CoSecurityDefinitionDTO
    {
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
