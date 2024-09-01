using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLATFORM_DOMAIN.Entity.Salary
{
    public class SalaryEmployee
    {
        public int CodEmployee { get; set; }
        public decimal Salary { get; set; }
        public decimal SalaryPerDay { get; set; }
        public DateTime DateTime { get; set; }
        public SalaryEmployee() { }
        public SalaryEmployee(int codeEmployee, decimal salaryEmployee, decimal salaryPerDay, DateTime dateTime)
        {
            CodEmployee = codeEmployee;
            Salary = salaryEmployee;
            SalaryPerDay = salaryPerDay;
            DateTime = dateTime;
        }
    }
}
