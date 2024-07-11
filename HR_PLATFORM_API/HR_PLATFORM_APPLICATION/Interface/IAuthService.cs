using HR_PLATFORM_APPLICATION.Model.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLATFORM_APPLICATION.Interface
{
    public interface IAuthService
    {
        //Task<string> AuthenticateAsync(string username, string password);
        Task<AuthResult> AuthenticateAsync(string username, string password);
        Task CreateUserAsync(string username, string password, string role);
        Task ChangeUserPassword(string username, string password);
    }
}
