using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRESTService.BLL.DTOs;
using MyRESTService.BLL.Interfaces;

namespace MyRESTServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleBLL _roleBLL;
        public RolesController(IRoleBLL roleBLL)
        {
            _roleBLL = roleBLL;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole(RoleCreateDTO roleCreateDTO)
        {
            var result = await _roleBLL.AddRole(roleCreateDTO);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("AddUserRole")]
        public async Task<IActionResult> AddUserRole(string username, int roleid)
        {
            var result = await _roleBLL.AddUserToRole(username, roleid);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _roleBLL.GetAllRoles();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
