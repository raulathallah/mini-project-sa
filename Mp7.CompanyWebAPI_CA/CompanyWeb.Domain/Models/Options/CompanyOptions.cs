using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Options
{
    public class CompanyOptions
    {
        public const string SettingName = "CompanySettings";
        public int MaxWorkingHoursPerProject { get; set; }
        public int MaxDepartementProject { get; set; }
        public int MaxEmployeeProject { get; set; }
        public int MaxEmployeeAge { get; set; }
        public int MaxDepartementMemberIT { get; set; }
    }
}
