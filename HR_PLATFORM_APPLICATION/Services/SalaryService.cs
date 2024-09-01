using HR_PLATFORM_APPLICATION.Interface;
using HR_PLATFORM_APPLICATION.Model.Salary;
using HR_PLATFORM_DOMAIN.Entity.Salary;
using HR_PLATFORM_DOMAIN.Interface;

namespace HR_PLATFORM_APPLICATION.Services
{
    public class SalaryService(ISalaryRepository salaryRepository) : ISalaryService
    {
        private readonly ISalaryRepository _salaryRepository = salaryRepository;
        public async Task AddEmployeeSalary(SalaryModel salary)
        {
            var salaryEmployee = new SalaryEmployee(
                salary.CodeEmployee,
                salary.SalaryEmployee,
                salary.SalaryPerDay,
                salary.DateTime);  

            await _salaryRepository.AddEmployeeSalary(salaryEmployee);
        }

        public async Task DeleteEmployeeSalary(int codeEmployee, DateTime dateTime)
        {
            await _salaryRepository.DeleteEmployeeSalary(codeEmployee, dateTime);
        }

        public async Task<SalaryModel> GetEmployeeSalary(int codeEmployee)
        {
            var salaryEmployee = await _salaryRepository.GetEmployeeSalary(codeEmployee);
            if(salaryEmployee == null)
            {
                return null;
            }

            return new SalaryModel
            {
                CodeEmployee = salaryEmployee.CodEmployee,
                SalaryEmployee = salaryEmployee.Salary,
                SalaryPerDay = salaryEmployee.SalaryPerDay,
                DateTime = salaryEmployee.DateTime,
            };
        }

        public async Task<List<SalaryModel>> GetEmployeesSalary()
        {
            var result = await _salaryRepository.GetEmployeesSalary();
            return  result.Select(v => new SalaryModel
            {
                CodeEmployee = v.CodEmployee,
                SalaryEmployee = v.Salary,
                SalaryPerDay = v.SalaryPerDay,
                DateTime = v.DateTime,
            }).ToList();
        }

        public async Task<List<SalaryHistory>> GetSalaryHistory(int codeEmployee, DateTime startDate, DateTime endDate)
        {
            var result = await _salaryRepository.GetSalaryHistory(codeEmployee, startDate, endDate);
            if(result == null)
            {
                return new List<SalaryHistory>();
            }
            return result.Select(v => new SalaryHistory
            {
                CodeEmployee = v.CodeEmployee,
                Salary_Brut = v.Salary_Brut,
                Salary_Net = v.Salary_Net,
                Data_Send = v.Data_Send,
            }).ToList();
        }

        public async Task<List<SalaryHistory>> GetSalaryHistoryAll(DateTime startDate, DateTime endDate)
        {
            var result = await _salaryRepository.GetSalaryHistoryAll(startDate, endDate);
            return result.Select(v=> new SalaryHistory
            {
                CodeEmployee = v.CodeEmployee,
                Salary_Brut= v.Salary_Brut,
                Salary_Net= v.Salary_Net,
                Data_Send= v.Data_Send,
            }).ToList();
        }
    }
}
