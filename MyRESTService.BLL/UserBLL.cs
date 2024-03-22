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
    public class UserBLL : IUserBLL
    {
        private readonly IUserData _userData;
        private readonly IMapper _mapper;

        public UserBLL(IUserData userData, IMapper mapper)
        {
            _userData = userData;
            _mapper = mapper;
        }
        public async Task<Task> ChangePassword(string username, string newPassword)
        {

            await _userData.ChangePassword(username, newPassword);
            return Task.CompletedTask;
        }

        public async Task<Task> Delete(string username)
        {
            await _userData.Delete(username);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            var users = await _userData.GetAll();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public Task<IEnumerable<UserDTO>> GetAllWithRoles()
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> GetByUsername(string username)
        {
            var user = await _userData.GetByUsername(username);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetUserWithRoles(string username)
        {
            var userWithRole = await _userData.GetUserWithRoles(username);  
            return _mapper.Map<UserDTO>(userWithRole);
        }

        public async Task<Task> Insert(UserCreateDTO entity)
        {
            var dataInsert = _mapper.Map<User>(entity);
            await _userData.Insert(dataInsert);
            return Task.CompletedTask;  
        }

        public async Task<UserDTO> Login(string username, string password)
        {
            var user = await _userData.Login(username, password);
            return _mapper.Map<UserDTO>(user);
        }

        public Task<UserDTO> LoginMVC(LoginDTO loginDTO)
        {
            throw new NotImplementedException();
        }
    }
}
