using HR_PLATFORM_APPLICATION.Model.Vacation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLATFORM_APPLICATION.Interface
{
    public interface IVacationService
    {
        Task AddVacationAsync(VacationModel vacationModel);
        Task<bool> UpdateVacationAsync(int codeEmployee, VacationModel vacationModel);
    }
}
