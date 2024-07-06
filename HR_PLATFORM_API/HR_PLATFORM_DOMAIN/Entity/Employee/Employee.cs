﻿using System;
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
        public decimal Salary { get;  set; }
        public DateTime ContractDate { get;  set; }
        public string Studied { get;  set; }
        public string OperatorHR { get;  set; }
        public bool StatutEmployee { get; set; }

        public Employee(string firstName, string lastName, DateTime birthday, string address, string email, int codEmployee, decimal phoneNumber, string department, string function, decimal salary, DateTime contractDate, string studied, string operatorHR, bool statutEmployee)
        {
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
            Address = address;
            Email = email;
            CodEmployee = codEmployee;
            PhoneNumber = phoneNumber;
            Department = department;
            Function = function;
            Salary = salary;
            ContractDate = contractDate;
            Studied = studied;
            OperatorHR = operatorHR;
            StatutEmployee = statutEmployee;
        }
    }
}
