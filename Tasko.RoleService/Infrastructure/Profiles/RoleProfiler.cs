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