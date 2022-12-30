using Tasko.Domains.Models.Structural;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<IUser, UserRead>();
    }
}