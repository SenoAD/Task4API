using MyRESTService.Domain.Models;

namespace MyRESTService.Data.Interfaces
{
    public interface IRoleData : ICrudData<Role>
    {
        Task<Task> AddUserToRole(string username, int roleId);
    }
}
