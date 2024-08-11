using crypto;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLATFORM_APPLICATION.Model.Auth
{
    public class AuthResult
    {
        public string Token { get; set; }
        public bool IsFirstLogin { get; set; }
    }
}
