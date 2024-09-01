using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLATFORM_APPLICATION.Model.Employee
{
    public class EmployeeModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public decimal PhoneNumber { get; set; }
        public string Department { get; set; }
        public string Function { get; set; }
        public int ContractCode{ get; set; }
        public DateTime ContractDate { get; set; }
        public string Studied { get; set; }
        public string OperatorHR { get; set; }
        public int codeManager { get; set; }
        public string Grafic {  get; set; }
        public bool StatutEmployee { get; set; }
    }
}
