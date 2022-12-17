using AutoMapper;
using Tasko.Domains.Models.DTO.User;
using Tasko.Domains.Models.Structural.Providers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<IUser, UserRead>();
    }
}