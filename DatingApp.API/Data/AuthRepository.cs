using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        public AuthRepository(DataContext context)
        {
            Context = context;
        }

        private DataContext Context { get; }

        public async Task<User> Login(string username, string password)
        {
            var user = await Context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null) return null;

            if (!VerifyPassword(password, user))
                return null;

            return user;
        }

        private bool VerifyPassword(string password, User user)
        {
            using (var hmac = new HMACSHA512(user.PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                if (user.PasswordHash.Length != computedHash.Length)
                    return false;

                for (int i = 0; i < computedHash.Length; i++)
                    if (computedHash[i] != user.PasswordHash[i])
                        return false;

                return true;
            }
        }

        public async Task<User> Register(string username, string password)
        {
            var user = new User() { Username = username };

            byte[] passwdHash, passwdSalt;
            CreatePasswordHash(password, out passwdHash, out passwdSalt);

            user.PasswordHash = passwdHash;
            user.PasswordSalt = passwdSalt;

            await Context.Users.AddAsync(user);
            await Context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwdHash, out byte[] passwdSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwdSalt = hmac.Key;
                passwdHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string username) =>
            await Context.Users.AnyAsync(u => u.Username == username);

    }
}
