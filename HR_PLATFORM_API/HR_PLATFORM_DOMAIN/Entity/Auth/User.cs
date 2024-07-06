using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLATFORM_DOMAIN.Entity.Auth
{
    public class User
    {
        public int Id { get; private set; }
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }

        public User(string username, string passwordHash)
        {
            Username = username;
            PasswordHash = passwordHash;
        }
        public bool CheckPassword(string password)
        {
            // Verificare hash parola
            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }
    }
}
