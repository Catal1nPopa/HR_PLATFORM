using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLATFORM_DOMAIN.Interface
{
    public interface IServiceRepository
    {
        Task<int> GetEmployeeManager(int codeEmployee);
    }
}
