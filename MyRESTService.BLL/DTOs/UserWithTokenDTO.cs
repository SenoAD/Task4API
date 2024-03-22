using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRESTService.BLL.DTOs
{
    public class UserWithTokenDTO
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }
    }
}
