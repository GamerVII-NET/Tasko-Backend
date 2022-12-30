using AutoMapper;
using Tasko.Domains.Models.DTO.Permissions;
using Tasko.Domains.Models.Structural;

namespace Tasko.PermissionService.Infrastructure.Profiles
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<IPermissionCreate, Permission>();
            CreateMap<IPermissionUpdate, Permission>();
            CreateMap<IPermissionRead, Permission>();
        }
    }
}
