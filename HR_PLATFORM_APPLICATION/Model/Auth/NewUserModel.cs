using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLATFORM_APPLICATION.Model.Auth
{
    public class NewUserModel
    {
        public string username {  get; set; }
        public string password { get; set; }
        public bool FirstLogin { get; set; }

        public NewUserModel(string username, string password, bool firstLogin)
        {
            this.username = username;
            this.password = password;
            FirstLogin = firstLogin;
        }
    }
}
