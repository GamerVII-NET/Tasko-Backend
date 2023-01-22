using Tasko.Domains.Models.DTO.Permissions;
using Tasko.Domains.Models.DTO.Role;

public class PermissionProfile : Profile
{
    public PermissionProfile()
    {

        CreateMap<IPermission, IPermissionRead>();
        CreateMap<Permission, PermissionRead>();

    }
}