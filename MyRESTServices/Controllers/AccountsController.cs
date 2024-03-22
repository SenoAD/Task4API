using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyRESTService.BLL.DTOs;
using MyRESTService.BLL.Interfaces;
using MyRESTServices.Helpers;

namespace MyRESTServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IUserBLL _userBLL;
        public AccountsController(IOptions<AppSettings> appSettings, IUserBLL userBLL)
        {
            _userBLL = userBLL;
            _appSettings = appSettings.Value;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var loginData = await _userBLL.Login(loginDTO.Username, loginDTO.Password);
            var userWithRole = await _userBLL.GetUserWithRoles(loginDTO.Username);
            if (loginData != null)
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, loginDTO.Username));
                foreach (var role in userWithRole.Roles )
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var userWithToken = new UserWithTokenDTO
                {
                    Username = loginDTO.Username,
                    Password = loginDTO.Password,
                    Token = tokenHandler.WriteToken(token)
                };
                return Ok(userWithToken.Token);
            }
            else
            {
                return BadRequest("Invalid credentials");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userBLL.GetAll();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("GetUserWithRole")]
        public async Task<IActionResult> GetUserWithRole(string username)
        {
            var result = await _userBLL.GetUserWithRoles(username);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert(UserCreateDTO userCreateDTO)
        {

            if (userCreateDTO == null)
            {
                return BadRequest();
            }

            try
            {
                await _userBLL.Insert(userCreateDTO);
                return Ok("Insert data success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword(string username, string password)
        {
            try
            {
                await _userBLL.ChangePassword(username, password);
                return Ok("Update Password success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(string username)
        {
            try
            {
                await _userBLL.Delete(username);
                return Ok("Delete Password success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
