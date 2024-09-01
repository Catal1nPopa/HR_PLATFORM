using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLATFORM_DOMAIN.Entity.ReportingTimex
{
    public class ReportingTime
    {
        public int CodeEmployee { get; set; }
        public DateTime TimeFirstEntry { get; set; }
        public DateTime TimeLastExit { get; set; }
        public string LocationEntry { get; set; }
        public string LocationExit { get; set; }
        public string TimeOnWork { get; set; }

        public ReportingTime() { }
        public ReportingTime(int codeEmployee, DateTime timeFirstEntry, DateTime timeLastExit, string locationEntry, string locationExit, string timeOnWork)
        {
            CodeEmployee = codeEmployee;
            TimeFirstEntry = timeFirstEntry;
            TimeLastExit = timeLastExit;
            LocationEntry = locationEntry;
            LocationExit = locationExit;
            TimeOnWork = timeOnWork;
        }
    }
}
