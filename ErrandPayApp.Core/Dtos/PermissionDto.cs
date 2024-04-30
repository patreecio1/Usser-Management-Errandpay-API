using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrandPayApp.Core.Dtos
{
    public class PermissionDto : BaseModel<long>
    {
        public PermissionDto()
        {
        }

        public string PermissionName { get; set; }
        public string PermissionDisplayName { get; set; }
        public string PermissionDescription { get; set; }
    }
}
