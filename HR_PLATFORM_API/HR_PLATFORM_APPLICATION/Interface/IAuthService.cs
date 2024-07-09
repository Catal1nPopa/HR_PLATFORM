﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLATFORM_APPLICATION.Interface
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync(string username, string password);
        Task CreateUserAsync(string username, string password, string role);
    }
}
