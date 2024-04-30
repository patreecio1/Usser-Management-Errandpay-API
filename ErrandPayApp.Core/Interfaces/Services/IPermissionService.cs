using ErrandPayApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrandPayApp.Core.Interfaces.Services
{
    public interface IPermissionService
    {
        Task<bool> IsPermited(PermissionEnum permission, PermissionAction permissionAction);
    }
}
