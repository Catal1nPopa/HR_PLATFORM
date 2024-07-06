using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLATFORM_INFRASTRUCTURE.Entities
{
    public class EmployeeEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int CodEmployee { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal PhoneNumber { get; set; }
        public string Department { get; set; }
        public string Function { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Salary { get; set; }
        public DateTime ContractDate { get; set; }
        public string Studied { get; set; }
        public string OperatorHR { get; set; }
        public bool StatutEmployee { get; set; }
    }
}
