using Newtonsoft.Json;
using ErrandPayApp.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrandPayApp.Core.Models
{
    public class LoginResponseModel
    {
        public LoginResponseModel()
        {
        }
        public string TokenType { get; set; }

        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiry { get; set; }
       
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }

        public AppUserDto Profile { get; set; }
        
        public RolePermissionDto[] Permission { get; set; }
        public UserRoleDto[] Roles { get; set; }


    }
}
