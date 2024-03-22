using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyRESTService.Data.Interfaces;
using MyRESTService.Domain.Models;

namespace MyRESTService.Data
{
    public class UserData : IUserData
    {
        private readonly AppDbContext _appDbContext;
        public UserData(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Task> ChangePassword(string username, string newPassword)
        {
            var user = await _appDbContext.Users.FindAsync(username);
            if (user == null)
            {
                return null;
            }
            user.Password = Helpers.Md5Hash.GetMD5Hash(newPassword);
            await _appDbContext.SaveChangesAsync();
            return Task.CompletedTask;
        }

        public async Task Delete(string username)
        {
            var user = await GetByUsername(username);
            _appDbContext.Users.Remove(user);
            await _appDbContext.SaveChangesAsync(); 
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = _appDbContext.Users.ToList();
            return users;
        }

        public Task<IEnumerable<User>> GetAllWithRoles()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _appDbContext.Users.SingleOrDefaultAsync(x => x.Username == username);
        }

        public async Task<User> GetUserWithRoles(string username)
        {
            var userWithRole = await _appDbContext.Users
                                        .Include(u => u.Roles) // Include roles
                                        .SingleOrDefaultAsync(x => x.Username == username);
            return userWithRole;
        }

        public async Task<User> Insert(User entity)
        {
            entity.Password = Helpers.Md5Hash.GetMD5Hash(entity.Password);
            await _appDbContext.Users.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<User> Login(string username, string password)
        {   var login = await _appDbContext.Users.SingleOrDefaultAsync(c => c.Username == username && c.Password == Helpers.Md5Hash.GetMD5Hash(password));
            return login;
        }

        public async Task<User> Update(User entity)
        {
            var user = await _appDbContext.Users.FindAsync(entity.Username);
            if (user == null)
            {
                return null;
            }
            user.Password = Helpers.Md5Hash.GetMD5Hash(entity.Password);
            user.Email = entity.Email;
            user.FirstName = entity.FirstName;
            user.LastName = entity.LastName;
            user.Address = entity.Address;
            user.Telp = entity.Telp;
            await _appDbContext.SaveChangesAsync();
            return user;

        }
    }
}
