using ErrandPayApp.Core.Dtos;
using ErrandPayApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrandPayApp.Core.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<RoleDto> Create(RoleDto model);
        Task<RoleDto[]> GetRoles();
        Task<RoleDto> GetById(long id);
        Task<RoleDto> GetByRoleName(string roleName);
    }
}
