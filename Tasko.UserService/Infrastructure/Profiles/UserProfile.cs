using Tasko.Domains.Models.DTO.Role;
using Tasko.Domains.Models.DTO.User;

namespace Tasko.Service.Infrastructure.Profiles;

internal class UserProfile : Profile
{

    public UserProfile()
    {

        CreateMap<User, UserRead>();
        CreateMap<UserCreate, User>();
        CreateMap<UserUpdate, User>();
    }
}