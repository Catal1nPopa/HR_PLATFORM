using HR_PLATFORM_APPLICATION.Interface;
using HR_PLATFORM_APPLICATION.Services;
using HR_PLATFORM_DOMAIN.Interface;
using HR_PLATFORM_INFRASTRUCTURE.DbContext;
using HR_PLATFORM_INFRASTRUCTURE.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace HR_PLATFORM_APPLICATION.Injections
{
    public static class Application
    {
        public static IServiceCollection AddApplication(this IServiceCollection Services)
        {
            Services.AddDbContext<ApplicationDbContext>();


            Services.AddScoped<IUserRepository, UserRepository>();
            Services.AddScoped<IAuthService, AuthService>();

            Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            Services.AddScoped<IEmployeeService, EmployeeService>();

            Services.AddScoped<IVacationService, VacationService>();
            Services.AddScoped<IVacationRepository, VacationRepository>();
            return Services;
        }
    }
}
