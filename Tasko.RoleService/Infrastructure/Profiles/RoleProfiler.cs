using AutoMapper;
using Tasko.Domains.Models.DTO.Role;
using Tasko.Domains.Models.DTO.User;
using Tasko.Domains.Models.Structural.Providers;

namespace Tasko.RoleService.Infrasructure.Profiles;


public class RoleProfile : Profile
{
    public RoleProfile()
    {

        //CreateMap<User, UserRead>();
        CreateMap<IRole, RoleRead>();
        CreateMap<RoleCreate, Role>();
    }
}