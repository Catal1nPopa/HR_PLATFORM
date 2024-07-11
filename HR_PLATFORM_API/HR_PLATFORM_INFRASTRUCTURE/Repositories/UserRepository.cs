﻿using HR_PLATFORM_DOMAIN.Entity.Auth;
using HR_PLATFORM_DOMAIN.Interface;
using HR_PLATFORM_INFRASTRUCTURE.DbContext;
using HR_PLATFORM_INFRASTRUCTURE.Entities;
using Microsoft.EntityFrameworkCore;

namespace HR_PLATFORM_INFRASTRUCTURE.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var userEntity = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
            if (userEntity == null)
            {
                return null;
            }

            return new User(userEntity.Username, userEntity.PasswordHash, userEntity.Role, userEntity.Salt, userEntity.FirstLogin);
        }

        public async Task AddUserAsync(User user)
        {
            var userEntity = new UserEntity
            {
                Username = user.Username,
                PasswordHash = user.PasswordHash,
                Role = user.Role,
                Salt = user.Salt,
                FirstLogin = true,
            };
            _context.Users.Add(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserPass(User user)
        {
            var userEntity = await _context.Users.SingleOrDefaultAsync(u => u.Username == user.Username);
            if (userEntity != null)
            {
                userEntity.FirstLogin = user.FirstLogin;
                userEntity.PasswordHash = user.PasswordHash;
                userEntity.Salt = user.Salt;
                await _context.SaveChangesAsync();
            }
        }
    }
    
}
