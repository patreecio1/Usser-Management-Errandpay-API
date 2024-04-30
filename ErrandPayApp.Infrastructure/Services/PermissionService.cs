using ErrandPayApp.Core.Dtos;
using ErrandPayApp.Core.Enums;
using ErrandPayApp.Core.Interfaces.Repositories;
using ErrandPayApp.Core.Interfaces.Services;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrandPayApp.Infrastructure.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IHttpContextService httpContextService;
        private readonly IUserRepository userRepository;
        public PermissionService(IHttpContextService httpContextService,
            IUserRepository userRepository)
        {
            this.httpContextService = httpContextService;
            this.userRepository = userRepository;
        }

        public async Task<bool> IsPermited(PermissionEnum permission, PermissionAction permissionAction)
        {
            int userId = httpContextService.CurrentUserId();
            if (userId == 0)
                return false;

            AppUserDto user = await userRepository.GetById(userId);
            if (user == null)
                return false;
            if (!user.IsActive || !user.IsEmailConfirmed)
                return false;
            if (user.IsSuperAdmin)
                return true;

            throw new NotImplementedException();
        }
    }
}
