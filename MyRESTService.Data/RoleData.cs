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
    public class RoleData : IRoleData
    {
        private readonly AppDbContext _appDbContext;
        public RoleData(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Task> AddUserToRole(string username, int roleId)
        {
            var user = await _appDbContext.Users.SingleOrDefaultAsync(c=> c.Username == username);
            var role = await _appDbContext.Roles.FindAsync(roleId);

            role.Usernames.Add(user);
            await _appDbContext.SaveChangesAsync();
            return Task.CompletedTask;
        }

        public async Task Delete(int id)
        {
            var role = await _appDbContext.Roles.SingleOrDefaultAsync(c => c.RoleId == id);
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            var role = await _appDbContext.Roles.ToListAsync();
            return role;
        }

        public async Task<Role> GetById(int id)
        {
           return await _appDbContext.Roles.SingleOrDefaultAsync(c=> c.RoleId == id);
        }

        public async Task<Role> Insert(Role entity)
        {
            await _appDbContext.Roles.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Role> Update(int id, Role entity)
        {
            var role = await _appDbContext.Roles.FindAsync(id);

            role.RoleName = entity.RoleName;
            await _appDbContext.SaveChangesAsync();
            return role ;
        }
    }
}
