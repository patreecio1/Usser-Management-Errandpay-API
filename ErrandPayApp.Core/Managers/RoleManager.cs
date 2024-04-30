using ErrandPayApp.Core.Dtos;
using ErrandPayApp.Core.Exceptions;
using ErrandPayApp.Core.Interfaces.Managers;
using ErrandPayApp.Core.Interfaces.Repositories;
using ErrandPayApp.Core.Interfaces.Services;
using ErrandPayApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrandPayApp.Core.Managers
{
    public class RoleManager : IRoleManager
    {
        private readonly IRoleRepository roleRepository;
        private readonly IUserRoleRepository userRoleRepository;
        private readonly IHttpContextService httpContextService;
        public RoleManager(IRoleRepository roleRepository, IUserRoleRepository userRoleRepository)
        {
            this.roleRepository = roleRepository;
            this.httpContextService = httpContextService;
            this.userRoleRepository = userRoleRepository;
        }

        public async Task<RoleDto> Create(RoleDto model)
        {
            RoleDto role = await roleRepository.GetByRoleName(model.RoleName);
            if (role != null)
                throw new BadRequestException("Role already exist");

            role = await roleRepository.Create(model);
            if (role == null)
                throw new BadRequestException("Request failed. Kindly retry");

            return role;
        }

        public async Task<RoleDto[]> GetRoles()
        {
          return await  roleRepository.GetRoles();
        }
    }
}
