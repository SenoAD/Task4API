using MyRESTService.BLL.DTOs;
using System.Collections.Generic;

namespace MyRESTService.BLL.Interfaces
{
    public interface IRoleBLL
    {
        Task<IEnumerable<RoleDTO>> GetAllRoles();
        Task<Task> AddRole(RoleCreateDTO roleCreateDTO);
        Task<Task> AddUserToRole(string username, int roleId);

    }
}
