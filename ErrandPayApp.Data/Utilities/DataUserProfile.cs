using AutoMapper;
using ErrandPayApp.Core.Dtos;
using ErrandPayApp.Data.Entities;

namespace ErrandPayApp.Data.Utilities
{
    public class DataUserProfile : Profile
    {
        public DataUserProfile()
        {
            CreateMap<AppUser, AppUserDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<UserRole, UserRoleDto>().ReverseMap();
            CreateMap<ConfirmationToken, ConfirmationTokenDto>().ReverseMap();

        }
    }
}
