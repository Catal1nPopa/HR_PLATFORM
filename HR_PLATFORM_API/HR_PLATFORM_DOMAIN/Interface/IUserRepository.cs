using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR_PLATFORM_DOMAIN.Entity.Auth;

namespace HR_PLATFORM_DOMAIN.Interface
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsername(string username);
        Task AddUserAsync(User user);
        Task UpdateUserPass(User user);
    }
}
