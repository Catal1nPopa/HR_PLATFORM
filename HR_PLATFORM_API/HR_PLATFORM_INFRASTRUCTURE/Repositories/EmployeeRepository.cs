using HR_PLATFORM_DOMAIN.Entity.Employee;
using HR_PLATFORM_DOMAIN.Interface;
using HR_PLATFORM_INFRASTRUCTURE.DbContext;
using HR_PLATFORM_INFRASTRUCTURE.Entities;
using Microsoft.EntityFrameworkCore;

namespace HR_PLATFORM_INFRASTRUCTURE.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddEmployee(Employee employee)
        {
            var employeeEntity = new EmployeeEntity
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Birthday = employee.Birthday,
                Address = employee.Address,
                Email = employee.Email,
                CodEmployee = employee.CodEmployee,
                PhoneNumber = employee.PhoneNumber,
                Department = employee.Department,
                Function = employee.Function,
                Salary = employee.Salary,
                ContractDate = employee.ContractDate,
                Studied = employee.Studied,
                OperatorHR = employee.OperatorHR,
                StatutEmployee = employee.StatutEmployee,
            };

            _context.Employees.Add(employeeEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            var employeeEntity = await _context.Employees
                .FirstOrDefaultAsync(e => e.CodEmployee == id);
            if (employeeEntity == null)
            {
                return null;
            }

            return new Employee
            (
                employeeEntity.FirstName,
                employeeEntity.LastName,
                employeeEntity.Birthday,
                employeeEntity.Address,
                employeeEntity.Email,
                employeeEntity.CodEmployee,
                employeeEntity.PhoneNumber,
                employeeEntity.Department,
                employeeEntity.Function,
                employeeEntity.Salary,
                employeeEntity.ContractDate,
                employeeEntity.Studied,
                    employeeEntity.OperatorHR,
                    employeeEntity.StatutEmployee
            );
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            //var employeeEntity = await _context.Employees.FindAsync(id);
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.CodEmployee == id);

            if (employee == null)
            {
                return false;
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateEmployeeAsync(int id, Employee employee)
        {
            var employeeEntity = await _context.Employees.FindAsync(id);
            if (employeeEntity == null)
            {
                return false;
            }

            employeeEntity.FirstName = employee.FirstName;
            employeeEntity.Address = employee.Address;
            employeeEntity.Email = employee.Email;
            employeeEntity.CodEmployee = employee.CodEmployee;
            employeeEntity.PhoneNumber = employee.PhoneNumber;
            employeeEntity.Department = employee.Department;
            employeeEntity.Function = employee.Function;
            employeeEntity.Salary = employee.Salary;
            employeeEntity.Studied = employee.Studied;
            employeeEntity.StatutEmployee = employee.StatutEmployee;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
