using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLATFORM_INFRASTRUCTURE.Entities
{
    public class VacationDetailsEntity
    {
        public int Id { get; set; }
        public string CodeEmployee {  get; set; }
        public int TotalVacationDaysLeft { get; set; }
        public int DaysUsedThisYear { get; set; }
        public int AdditionalVacationDays { get; set; }
    }
}
