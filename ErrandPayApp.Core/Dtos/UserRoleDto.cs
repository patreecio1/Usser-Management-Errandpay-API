using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrandPayApp.Core.Dtos
{
    public class UserRoleDto : BaseModel<long>
    {
        public UserRoleDto()
        {
        }

        public int UserId { get; set; }
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public RoleDto Role { get; set; }
        public AppUserDto User { get; set; }
    }
}
