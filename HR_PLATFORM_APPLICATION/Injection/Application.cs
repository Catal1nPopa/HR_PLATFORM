using HR_PLATFORM_APPLICATION.Interface;
using HR_PLATFORM_APPLICATION.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HR_PLATFORM_APPLICATION.Injection
{
    public static class Application
    {
        public static IServiceCollection AddApplication(this IServiceCollection Services)
        {
            Services.AddScoped<IAuthService, AuthService>();
            Services.AddScoped<IEmployeeService, EmployeeService>();
            Services.AddScoped<IVacationService, VacationService>();
            Services.AddScoped<IServiceFunction, ServiceFunction>();
            Services.AddScoped<ICVService, CVService>();
            Services.AddScoped<ISalaryService, SalaryService>();
            Services.AddScoped<IReportingTimex, TimexService>();
            return Services;
        }
    }
}
