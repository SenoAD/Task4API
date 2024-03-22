using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyRESTService.BLL.DTOs;
using MyRESTService.BLL.Interfaces;
using MyRESTService.Data.Interfaces;
using MyRESTService.Domain.Models;

namespace MyRESTService.BLL
{
    public class RoleBLL : IRoleBLL
    {
        private readonly IRoleData _roleData;
        private readonly IMapper _mapper;

        public RoleBLL(IRoleData roleData, IMapper mapper)
        {
            _roleData = roleData;
            _mapper = mapper;
        }
        public async Task<Task> AddRole(RoleCreateDTO roleCreateDTO)
        {
           var roleDTO = _mapper.Map<Role>(roleCreateDTO);
           await _roleData.Insert(roleDTO);
           return Task.CompletedTask;
        }

        public async Task<Task> AddUserToRole(string username, int roleId)
        {
            await _roleData.AddUserToRole(username, roleId);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<RoleDTO>> GetAllRoles()
        {
            var roles = await _roleData.GetAll();
            return _mapper.Map<IEnumerable<RoleDTO>>(roles);
        }
    }
}
