using DatingApp.API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Data
{
    public class Seed
    {

        public static void SeedUsers(DataContext context)
        {
            if (context.Users.Any())
                return;

            var usersJson = File.ReadAllText("Data/UserSeedData.json");
            var users = JsonConvert.DeserializeObject<List<User>>(usersJson);

            foreach (var user in users)
            {
                byte[] salt, hash;
                CreatePasswordHash(password: "bobik", out hash, out salt);

                user.PasswordHash = hash;
                user.PasswordSalt = salt;
                user.Username = user.Username.ToLower();

                context.Users.Add(user);
            }

            context.SaveChanges();
        }

        private static void CreatePasswordHash(string password, out byte[] passwdHash, out byte[] passwdSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwdSalt = hmac.Key;
                passwdHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
