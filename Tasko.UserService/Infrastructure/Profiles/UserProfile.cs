namespace Tasko.Service.Infrastructure.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {

        CreateMap<User, UserRead>();
        CreateMap<UserCreate, User>();
        CreateMap<UserUpdate, User>();
        CreateMap<UserRead, User>();
    }
}