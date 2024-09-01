using HR_PLATFORM_APPLICATION.Interface;
using HR_PLATFORM_APPLICATION.Model.Vacation;
using HR_PLATFORM_DOMAIN.Entity.Vacation;
using HR_PLATFORM_DOMAIN.Interface;

namespace HR_PLATFORM_APPLICATION.Services
{
    public class VacationService : IVacationService
    {
        private readonly IVacationRepository _vacationRepository;
        public VacationService(IVacationRepository repository)
        {
            _vacationRepository = repository;
        }

        public async Task AddVacationAsync(VacationModel vacationModel)
        {
            var vacation = new Vacation(
                vacationModel.CodEmployee,
                vacationModel.StartDate,
                vacationModel.EndDate,
                vacationModel.DaysVacation,
                vacationModel.VacationDaysLeft,
                vacationModel.TypeVacation,
                vacationModel.CodeManager,
                vacationModel.Status);

            await _vacationRepository.AddVacation(vacation);
        }

        public async Task<List<VacationModel>> GetVacationsEmployees()
        {
            var vacations = await _vacationRepository.GetAllVacations();

            var vacationModels = vacations.Select(v => new VacationModel
            {
                Id = v.Id,
                CodEmployee = v.CodEmployee,
                StartDate = v.StartDate,
                EndDate = v.EndDate,
                DaysVacation = v.DaysVacation,
                VacationDaysLeft = v.VacationDaysLeft,
                TypeVacation = v.TypeVacation
            }).ToList();

            return vacationModels;
        }

        public async Task<List<VacationModel>> GetEmployeeVacations(int codeEmployee)
        {
            var vacations = await _vacationRepository.GetEmployeeVacations(codeEmployee);
            var vacationModels = vacations.Select(v => new VacationModel
            {
                Id = v.Id,
                CodEmployee = v.CodEmployee,
                StartDate = v.StartDate,
                EndDate = v.EndDate,
                DaysVacation = v.DaysVacation,
                VacationDaysLeft = v.VacationDaysLeft,
                TypeVacation = v.TypeVacation
            }).ToList();
            return vacationModels;
        }

        public async Task<bool> UpdateVacationAsync(VacationModel vacationModel)
        {
            var existingVacation = await _vacationRepository.GetVacationByIdAsync(vacationModel.Id);
            if (existingVacation == null)
            {
                return false;
            }

            if(vacationModel.StartDate != existingVacation.StartDate)
            {
                existingVacation.StartDate = vacationModel.StartDate;
            }
            if(vacationModel.EndDate != existingVacation.EndDate)
            {
                existingVacation.EndDate = vacationModel.EndDate;
            }
            if(vacationModel.DaysVacation != existingVacation.DaysVacation)
            {
                existingVacation.DaysVacation = vacationModel.DaysVacation;
            }
            if(vacationModel.TypeVacation != existingVacation.TypeVacation)
            {
                existingVacation.TypeVacation = vacationModel.TypeVacation;
            }
            if(vacationModel.VacationDaysLeft != existingVacation.VacationDaysLeft)
            {
                existingVacation.VacationDaysLeft = vacationModel.VacationDaysLeft;
            }

            return await _vacationRepository.UpdateVacation(existingVacation);
        }
    }
}
