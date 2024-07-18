﻿using System.Security.Cryptography;

namespace HR_PLATFORM_DOMAIN.Entity.Auth
{
    public class User
    {
        public int Id { get; private set; }
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }
        public byte[] Salt { get; private set; }
        public string Role { get; private set; }
        public bool FirstLogin { get; private set; }

        public User(string username)
        {
            Username = username;
        }

        public User(string username, string passwordHash, byte[] salt)
        {
            Username = username;
            PasswordHash = passwordHash;
            Salt = salt;
        }
        public User(string username, string passwordHash, string role, byte[] salt, bool firstLogin)
        {
            Username = username;
            PasswordHash = passwordHash;
            Role = role;
            Salt = salt;
            FirstLogin = firstLogin;
        }

        public User(int id, string username, string passwordHash, string role, byte[] salt, bool firstLogin)
        {
            Id = id;
            Username = username;
            PasswordHash = passwordHash;
            Role = role;
            Salt = salt;
            FirstLogin = firstLogin;
        }
        public bool CheckPassword(string password)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, Salt, 350000, HashAlgorithmName.SHA512, 64);

            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(PasswordHash));
        }
    }
}
