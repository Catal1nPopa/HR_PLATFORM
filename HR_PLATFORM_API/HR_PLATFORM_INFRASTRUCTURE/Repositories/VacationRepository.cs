using HR_PLATFORM_DOMAIN.Entity.Vacation;
using HR_PLATFORM_DOMAIN.Interface;
using HR_PLATFORM_INFRASTRUCTURE.DbContext;
using HR_PLATFORM_INFRASTRUCTURE.Entities;
using Microsoft.EntityFrameworkCore;

namespace HR_PLATFORM_INFRASTRUCTURE.Repositories
{
    public class VacationRepository : IVacationRepository
    {
        private readonly ApplicationDbContext _context;
        public VacationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddVacation(Vacation vacation)
        {

            var vacationEntity = new VacationEntity
            {
                CodEmployee = vacation.CodEmployee,
                StartDate = vacation.StartDate,
                EndDate = vacation.EndDate,
                DaysVacation = vacation.DaysVacation,
                VacationDaysLeft = vacation.VacationDaysLeft,
                TypeVacation = vacation.TypeVacation
            };

            _context.Vacations.Add(vacationEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Vacation>> GetAllVacationsByEmployee(int codEmployee)
        {
            var vacationEntities = await _context.Vacations
                .Where(v => v.CodEmployee == codEmployee)
                .ToListAsync();

            var vacations = vacationEntities.Select(v => new Vacation(
            v.CodEmployee,
            v.StartDate,
            v.EndDate,
            v.DaysVacation,
            v.VacationDaysLeft,
            v.TypeVacation
            )).ToList();

            return vacations;
        }

        public async Task<Vacation> GetVacationByIdAsync(int codEmployee)
        {
            var vacationEntity = await _context.Vacations
                .Where(v => v.CodEmployee == codEmployee)
                .OrderByDescending(v => v.Id)  
                .FirstOrDefaultAsync();

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

        public async Task<bool> UpdateVacation(int codEmployee, Vacation vacation)
        {
            var vacationEntity = await _context.Vacations
                .Where(v => v.CodEmployee == codEmployee)
                .OrderByDescending(v => v.Id)
                .FirstOrDefaultAsync();

            if(vacationEntity == null) { return false;}

            vacationEntity.StartDate = vacation.StartDate;
            vacationEntity.EndDate = vacation.EndDate;
            vacationEntity.DaysVacation = vacation.DaysVacation;
            vacationEntity.TypeVacation = vacation.TypeVacation;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
