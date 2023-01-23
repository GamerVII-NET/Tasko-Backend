using Tasko.Domains.Models.DTO.Role;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<Role, RoleRead>();
        CreateMap<RoleCreate, Role>();
        CreateMap<RoleUpdate, Role>();
    }
}