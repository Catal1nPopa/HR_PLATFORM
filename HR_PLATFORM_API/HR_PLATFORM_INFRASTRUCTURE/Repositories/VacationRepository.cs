using HR_PLATFORM_DOMAIN.Entity.Vacation;
using HR_PLATFORM_DOMAIN.Interface;
using HR_PLATFORM_INFRASTRUCTURE.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLATFORM_INFRASTRUCTURE.Repositories
{
    public class VacationRepository : IVacationRepository
    {
        private readonly ApplicationDbContext _context;
        public VacationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task AddVacation(Vacation vacation)
        {
            throw new NotImplementedException();
        }

        public async Task<Vacation> GetVacationByIdAsync(int codEmployee)
        {
            var vacationEntity = await _context.Vacations.FindAsync(codEmployee);
            if (vacationEntity == null)
            {
                return null;
            }

            return new Vacation(
                    vacationEntity.CodEmployee,
                    vacationEntity.StartDate,
                    vacationEntity.EndDate,
                    vacationEntity.DaysVacation,
                    vacationEntity.VacationDaysLeft,
                    vacationEntity.TypeVacation
                );
        }

        public Task<bool> UpdateVacation(int codEmployee, Vacation vacation)
        {
            throw new NotImplementedException();
        }
    }
}
