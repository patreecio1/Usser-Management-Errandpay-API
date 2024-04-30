using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrandPayApp.Core.Dtos
{
    public class RoleDto : BaseModel<long>
    {
        public RoleDto()
        {
        }

        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
    }
}
