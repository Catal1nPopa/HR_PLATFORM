using HR_PLATFORM_APPLICATION.Interface;
using HR_PLATFORM_DOMAIN.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLATFORM_APPLICATION.Services
{
    public class ServiceFunction(IServiceRepository serviceRepository) : IServiceFunction
    {
        private readonly IServiceRepository _serviceRepository = serviceRepository;
        public async Task<int> GetEmployeeManager(int codeEmployee)
        {
            return await _serviceRepository.GetEmployeeManager(codeEmployee);
        }
    }
}
