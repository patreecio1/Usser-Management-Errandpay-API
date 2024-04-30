using ErrandPayApp.Core.Enums;
using Microsoft.AspNetCore.Mvc;
namespace ErrandPayApp.Infrastructure.Filters
{
    public class PermissionAttribute : TypeFilterAttribute
    {
        public PermissionAttribute(PermissionEnum permission, PermissionAction permissionAction) : base(typeof(PermissionFilter))
        {
            Arguments = new object[] { permission, permissionAction };
        }
    }
}
