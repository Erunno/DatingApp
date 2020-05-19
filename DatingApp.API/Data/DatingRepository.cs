using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Data
{
    public class DatingRepository : IDatingRepository
    {
        public DatingRepository(DataContext context)
        {
            Context = context;
        }

        private DataContext Context { get; }

        public void Add<T>(T entity) where T : class 
            => Context.Add(entity);

        public void Delete<T>(T entity) where T : class 
            => Context.Remove(entity);

        public async Task<User> GetUser(int id)
            => await Context.Users.Include(u => u.Photos).FirstOrDefaultAsync(u => u.Id == id);

        public async Task<IEnumerable<User>> GetUsers()
            => await Context.Users.Include(u => u.Photos).ToListAsync();

        public async Task<bool> SaveAll()
            => await Context.SaveChangesAsync() > 0;
    }
}
