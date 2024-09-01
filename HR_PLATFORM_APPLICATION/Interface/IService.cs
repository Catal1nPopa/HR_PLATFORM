using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLATFORM_APPLICATION.Interface
{
    public interface IServiceFunction
    {
        Task<int> GetEmployeeManager(int codeEmployee);
    }
}
