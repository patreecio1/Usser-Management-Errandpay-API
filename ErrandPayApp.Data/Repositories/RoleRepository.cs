using AutoMapper;
using ErrandPayApp.Core.Interfaces.Repositories;
using ErrandPayApp.Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using ErrandPayApp.Data.Entities;
using ErrandPayApp.Data.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrandPayApp.Core.Dtos;
using ErrandPayApp.Core.Models;

namespace ErrandPayApp.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IMapper mapper;
        private readonly APPContext dbContext;
        private readonly IHttpContextService httpContextService;
        private readonly IUtilityService utilityService;
        public RoleRepository(APPContext aPPContext,  IMapper mapper, IUtilityService utilityService,
            IHttpContextService httpContextService)
        {
            this.mapper = mapper;
            this.dbContext = aPPContext;
            this.httpContextService = httpContextService;
            this.utilityService = utilityService;

        }

        public async  Task<RoleDto> Create(RoleDto model)
        {
            try
            {
                Role entity = mapper.Map<Role>(model);
                entity.RoleNameHash = utilityService.ToSha256(model.RoleName);
                entity.CreatedBy = "Admin";
                entity.CreatedDate = DateTime.Now;
                entity.ModifiedDate = DateTime.Now;
                entity.ModifiedBy = "Admin";
                dbContext.Roles.Add(entity);
                await dbContext.SaveChangesAsync();

                return mapper.Map<RoleDto>(entity);
            }
            catch (Exception ex)
            {

                throw ex;
            }
         
        }

        public async Task<RoleDto[]> GetRoles()
        {
            Role[] result = await dbContext.Roles
                              .Where(x => x.IsActive).ToArrayAsync();

            if (result.Length <= 0)
                return Array.Empty<RoleDto>();

            return mapper.Map<RoleDto[]>(result);
        }


        public async Task<RoleDto> GetById(long id)
        {
            var entity = await dbContext.Roles.FindAsync(id);
            if (entity == null)
                return null;

            return mapper.Map<RoleDto>(entity);
        }

        public async Task<RoleDto> GetByRoleName(string roleName)
        {
            string rolehash = utilityService.ToSha256(roleName);

            var entity = await dbContext.Roles.FirstOrDefaultAsync(a => a.RoleName == roleName);
            if (entity == null)
                return null;

            return mapper.Map<RoleDto>(entity);
        }
    }
}
