using AutoMapper;
using ErrandPayApp.Core.Dtos;
using ErrandPayApp.Infrastructure.Models;

namespace ErrandPayApp.Infrastructure.Utilities
{
    public class InfrastructureUserProfile : Profile
    {
        public InfrastructureUserProfile()
        {
            CreateMap<AuthUserResponseVm, AppUserDto>()
               .ForMember(x => x.Email, x => x.MapFrom(s => s.Email))
               .ForMember(x => x.FirstName, x => x.MapFrom(s => s.OtherName))
               .ForMember(x => x.Id, x => x.MapFrom(s => s.userId));
        }
    }
}
