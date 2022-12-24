public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<IUser, UserRead>();
    }
}