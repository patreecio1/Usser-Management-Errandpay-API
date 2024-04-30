using ErrandPayApp.Core.Dtos;
using ErrandPayApp.Core.Enums;
using ErrandPayApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrandPayApp.Core.Interfaces.Repositories
{
    public interface IUserRoleRepository
    {
        Task<UserRoleDto> Get(int userId, long roleId);
        Task<UserRoleDto> Create(UserRoleDto entity);
        Task<Page<UserRoleDto>> GetPaginated(int pageNumber, int pageSize);
        Task<UserRoleDto> GetByUserId(int userId);
        Task<UserRoleDto> GetById(long userRoleId);
        Task<int> Remove(long userRoleId);
        Task<RolePermissionDto[]> GetCurrentUserPermission();
        Task<RolePermissionDto[]> GetCurrentUserPermission(int userId);
        Task<bool> IsPermitted(PermissionEnum permission);
        Task<UserRoleDto[]> GetRolesforUser(int userId);

        Task<int> Create(int userId, long[] roleIds);
        Task<int> Remove(int userId, long userRoleId);


    }
}
