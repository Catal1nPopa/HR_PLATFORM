using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLATFORM_APPLICATION.Model.Salary
{
    public class SalaryModel
    {
        public int CodeEmployee { get; set; }
        public decimal SalaryEmployee { get; set; }
        public decimal SalaryPerDay { get; set; }
        public DateTime DateTime { get; set; }
    }
}
