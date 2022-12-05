using AutoMapper;
using Tasko.Domains.Models.DTO.Providers;
using Tasko.Domains.Models.Structural.Providers;

namespace Tasko.Server.Profiles.UserProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserRead>();
            CreateMap<UserCreate, User>();
        }
    }
}
