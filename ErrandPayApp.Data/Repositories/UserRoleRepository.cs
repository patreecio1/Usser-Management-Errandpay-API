using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using ErrandPayApp.Core.Dtos;
using ErrandPayApp.Core.Enums;
using ErrandPayApp.Core.Exceptions;
using ErrandPayApp.Core.Interfaces.Repositories;
using ErrandPayApp.Core.Interfaces.Services;
using ErrandPayApp.Core.Models;
using ErrandPayApp.Data.Entities;
using ErrandPayApp.Data.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ErrandPayApp.Data.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly IHttpContextService httpContextService;
        private readonly IMapper _mapper;
        private readonly APPContext _dbContext;
        private readonly IDapperContext dapperContext;
        private readonly IMemoryCache memoryCache;
        public UserRoleRepository(APPContext apContext, IMapper mapper,
             IHttpContextService httpContextService, IDapperContext dapperContext, IMemoryCache memoryCache)
        {
            this.httpContextService = httpContextService;
            this. _mapper = mapper;
            this._dbContext = apContext;
            this.dapperContext = dapperContext;
            this.memoryCache =memoryCache;
        }

        public async Task<UserRoleDto> Create(UserRoleDto model)
        {
            UserRole entity = await _dbContext.Set<UserRole>()
           .FirstOrDefaultAsync(x => x.RoleId == model.RoleId && x.UserId == model.UserId);

            if (entity != null)
            { throw new BadRequestException("Role already assigned to user");
            
            }

            _dbContext.Set<UserRole>().Add(new UserRole
            {
                RoleId = model.RoleId,
                UserId = model.UserId,
                CreatedDate = DateTimeOffset.Now,
                CreatedBy = "Admin"
            });
            int result = await _dbContext.SaveChangesAsync();


            return await GetById(model.Id);
        }
        public async Task<int> Create(int userId ,long[] roleIds)
        {
            try
            {
                List<UserRole> entities = new List<UserRole>();
                for (int i = 0; i < roleIds.Length; i++)
                {
                    var userole = new UserRole
                    {
                        UserId = userId,
                        RoleId = roleIds[i],
                        CreatedBy = "Admin",
                        ModifiedBy = "Admin",
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                    };
                    entities.Add(userole);
                }

                _dbContext.AddRange(entities);

                return await _dbContext.SaveChangesAsync();


            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }
        public async Task<UserRoleDto> Get(int userId, long roleId)
        {
            var role = await _dbContext.UserRoles
                .Include(a => a.User)
                .Include(a => a.Role)
                .FirstOrDefaultAsync(a => a.UserId == userId && a.RoleId == roleId);

            if (role == null)
                return null;

            return ToModel(role);
        }

        public async Task<UserRoleDto> GetById(long userRoleId)
        {
            var role = await _dbContext.UserRoles
               .Include(a => a.User)
               .Include(a => a.Role)
               .FirstOrDefaultAsync(a => a.Id == userRoleId);

            if (role == null)
                return null;

            return ToModel(role);
        }

        public async Task<UserRoleDto> GetByUserId(int userId)
        {
            var role = await _dbContext.UserRoles
              .Include(a => a.User)
              .Include(a => a.Role)
              .FirstOrDefaultAsync(a => a.UserId == userId);

            if (role == null)
                return null;

            return ToModel(role);
        }

        public async Task<RolePermissionDto[]> GetCurrentUserPermission()
        {
            int userId = httpContextService.CurrentUserId();
            string key = $"permission{userId}";

            RolePermissionDto[] permissions = Array.Empty<RolePermissionDto>();

            if (!memoryCache.TryGetValue<RolePermissionDto[]>(key, out permissions))
            {

                string query = " select rp.*, p.PermissionName from UserRoles ur " +
                    " inner join RolePermissions rp on rp.RoleId = ur.RoleId  " +
                    " inner join Permissions p on p.Id = rp.PermissionId " +
                    " where ur.UserId = @userId ";


                using (var connection = dapperContext.GetDbConnection())
                {
                    var result = await connection.QueryAsync<RolePermissionDto>(query, new { @userId = userId });
                    if (result.Count() > 0)
                    {
                        permissions = _mapper.Map<RolePermissionDto[]>(result);
                    }
                    memoryCache.Set(key, permissions, DateTimeOffset.Now.AddMinutes(3));
                }
            }

            return permissions;
        }

        public Task<RolePermissionDto[]> GetCurrentUserPermission(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Page<UserRoleDto>> GetPaginated(int pageNumber, int pageSize)
        {
            var query = _dbContext.UserRoles
              .Include(a => a.User)
              .Include(a => a.Role)
              .OrderByDescending(x => x.Id);

            UserRoleDto[] data = Array.Empty<UserRoleDto>();
            var result = await query.ToPageListAsync(pageNumber, pageSize);
            if (result.Data.Length > 0)
                data = result.Data.Select(x =>
                {
                    return ToModel(x);
                }).ToArray();

            return new Page<UserRoleDto>(data, result.PageCount, pageNumber, pageSize);
        }

        public async Task<bool> IsPermitted(PermissionEnum permission)
        {
            int userId = httpContextService.CurrentUserId();
            RolePermissionDto[] permissions = await GetCurrentUserPermission();

            string query = " select rp.*, p.PermissionName from UserRoles ur " +
                    " inner join RolePermissions rp on rp.RoleId = ur.RoleId  " +
                    " inner join Permissions p on p.Id = rp.PermissionId " +
                    " where ur.UserId = @userId and lower(p.PermissionName) = @PermissionName";


            using (var connection = dapperContext.GetDbConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<RolePermissionDto>(query, new
                {
                    @userId = userId,
                    @PermissionName = permission.ToString().ToLower()
                });
                if (result != null)
                    return true;
            }

            return false;
        }

        public async Task<int> Remove(int userId,long userRoleId)
        {
            // var e = await _dbContext.UserRoles.FindAsync(userRoleId);
            var e = await _dbContext.UserRoles.Where(x=> x.UserId ==userId && x.RoleId == userRoleId).FirstAsync();
            if (e == null)
                return 0;

            _dbContext.Remove(e);

          return   await _dbContext.SaveChangesAsync();
        }
        public async Task<int> Remove( long userRoleId)
        {
             var e = await _dbContext.UserRoles.FindAsync(userRoleId);
           // var e = await _dbContext.UserRoles.Where(x => x.UserId == userId && x.RoleId == userRoleId).FirstAsync();
            if (e == null)
                return 0;

            _dbContext.Remove(e);

            return await _dbContext.SaveChangesAsync();
        }
        public UserRoleDto ToModel(UserRole model)
        {
            UserRoleDto role = _mapper.Map<UserRoleDto>(model);
            if (model.Role != null)
                role.Role = _mapper.Map<RoleDto>(model.Role);
            if (model.User != null)
                role.User = _mapper.Map<AppUserDto>(model.User);

            return role;
        }
        public async Task<UserRoleDto[]> GetRolesforUser(int userId)
        {


            var entity = await (from userRole in _dbContext.Set<UserRole>()
                                join role in _dbContext.Set<Role>()
                                on userRole.RoleId equals role.Id

                                join appuser in _dbContext.Set<AppUser>()
                                on userRole.UserId equals appuser.Id

                                where userRole.UserId == userId


                                select new UserRoleDto
                                {
                                    RoleName = role.RoleName,
                                    Id = role.Id,

                                }).ToArrayAsync();

            if(entity.Length == 0)
                return Array.Empty<UserRoleDto>();
            return entity;
        }
    }
}
