using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLATFORM_APPLICATION.Model.Auth
{
    public class UsersModel
    {
        public string username {  get; set; }
        public string codeEmployee { get; set; }
        public bool firstLogin { get; set; }
    }
}
