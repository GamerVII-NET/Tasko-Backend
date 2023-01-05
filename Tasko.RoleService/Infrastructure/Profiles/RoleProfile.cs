using Tasko.Domains.Models.DTO.Role;

public class RoleProfile : Profile
{
    public RoleProfile()
    {

        CreateMap<Role, RoleRead>();
        CreateMap<IRole, IRoleRead>();
        CreateMap<IRole, RoleRead>();
        CreateMap<RoleCreate, IRole>();
        CreateMap<RoleCreate, Role>();

    }
}