using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLATFORM_DOMAIN.Entity.Employee
{
    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string Email { get;  set; }
        public int CodEmployee { get;  set; }
        public decimal PhoneNumber { get;  set; }
        public string Department { get;  set; }
        public string Function { get;  set; }
        public int ContractCode{ get;  set; }
        public DateTime ContractDate { get;  set; }
        public string Studied { get;  set; }
        public string OperatorHR { get;  set; }
        public int CodeManager { get; set; }
        public string Grafic { get; set; }
        public bool StatutEmployee { get; set; }

        public Employee() { }
        //public Employee(string firstName, string lastName, DateTime birthday, string address, string email, decimal phoneNumber, string department, string function, int contractCode, DateTime contractDate, string studied, string operatorHR, int codEmployee, bool statutEmployee)
        //{
        //    FirstName = firstName;
        //    LastName = lastName;
        //    Birthday = birthday;
        //    Address = address;
        //    Email = email;
        //    CodEmployee = codEmployee;
        //    PhoneNumber = phoneNumber;
        //    Department = department;
        //    Function = function;
        //    ContractCode = contractCode;
        //    ContractDate = contractDate;
        //    Studied = studied;
        //    OperatorHR = operatorHR;
        //    StatutEmployee = statutEmployee;
        //}
        public Employee(string firstName, string lastName, DateTime birthday, string address, string email, decimal phoneNumber, string department, string function, int contractCode, DateTime contractDate, string studied, string operatorHR, int codeManager, bool statutEmployee, string grafic)
        {
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
            Address = address;
            Email = email;
            PhoneNumber = phoneNumber;
            Department = department;
            Function = function;
            ContractCode = contractCode;
            ContractDate = contractDate;
            Studied = studied;
            OperatorHR = operatorHR;
            CodeManager = codeManager;
            StatutEmployee = statutEmployee;
            Grafic = grafic;
        }
    }
}
