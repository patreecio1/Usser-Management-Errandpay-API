using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ErrandPayApp.API.RequestVms;
using ErrandPayApp.Core.Dtos;

namespace ErrandPayApp.API
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<UserVm, AppUserDto>();
       
            CreateMap<RoleVm, RoleDto>();
            CreateMap<RolePermissionVm, RolePermissionDto>();
        }
    }
}
