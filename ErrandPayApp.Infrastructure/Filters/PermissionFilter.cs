using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrandPayApp.Core.Enums;
using ErrandPayApp.Core.Interfaces.Services;
using ErrandPayApp.Core.Models;
using Microsoft.AspNetCore.Mvc.Filters;
namespace ErrandPayApp.Infrastructure.Filters
{
    public class PermissionFilter : IAuthorizationFilter
    {
        private readonly PermissionAction permissionAction;
        private readonly IPermissionService permissionService;
        private readonly PermissionEnum permission;
        public PermissionFilter(PermissionEnum permission, PermissionAction permissionAction, IPermissionService permissionService)
        {
            this.permissionAction = permissionAction;
            this.permissionService = permissionService;
            this.permission = permission;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool result = permissionService.IsPermited(permission, permissionAction).Result;
            if (!result)
                context.Result = new ForbiddenObjectResult(ResponseModel<string>.Failed("",
                    "Permission not granted for the request. Please contact your administrator."));
        }
    }
}
