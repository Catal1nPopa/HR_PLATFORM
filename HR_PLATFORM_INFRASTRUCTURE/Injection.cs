using HR_PLATFORM_DOMAIN.Interface;
using HR_PLATFORM_INFRASTRUCTURE.DbContext;
using HR_PLATFORM_INFRASTRUCTURE.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace HR_PLATFORM_INFRASTRUCTURE
{
    public static class Injection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection Services) 
        {
            Services.AddSingleton<DapperContext>();
            Services.AddScoped<IUserRepository, UserRepository>();
            Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            Services.AddScoped<IVacationRepository, VacationRepository>();
            Services.AddScoped<ICVRepository, CVRepository>();
            Services.AddScoped<IServiceRepository, ServiceRepository>();
            Services.AddScoped<ISalaryRepository, SalaryRepository>();
            Services.AddScoped<ITimexRepository, ReportingTimex>();
            return Services;
        }
    }
}
