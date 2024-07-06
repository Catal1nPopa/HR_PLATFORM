namespace HR_PLATFORM.DTOs.Employee
{
    public class UpdateEmployeeDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int CodEmployee { get; set; }
        public decimal PhoneNumber { get; set; }
        public string Department { get; set; }
        public string Function { get; set; }
        public decimal Salary { get; set; }
        public DateTime ContractDate { get; set; }
        public string Studied { get; set; }
        public string OperatorHR { get; set; }
        public bool StatutEmployee { get; set; }
    }
}
