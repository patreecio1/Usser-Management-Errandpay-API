using ErrandPayApp.Core.Dtos;
using ErrandPayApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrandPayApp.Core.Interfaces.Managers
{
    public interface IUserRoleManager
    {
        Task<UserRoleDto> GetByUserRole();
        Task RemoveRole(long userRoleId);
        Task<UserRoleDto> AddUserRole(UserRoleDto entity);
        Task<Page<UserRoleDto>> Get(int pageSize, int pageNumber);
    }
}
