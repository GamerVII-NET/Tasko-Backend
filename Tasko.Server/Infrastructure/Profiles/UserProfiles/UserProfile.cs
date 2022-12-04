using AutoMapper;
using Tasko.Domains.Models.DTO.Interfaces;
using Tasko.Domains.Models.Structural.Interfaces;

namespace Tasko.Server.Profiles.UserProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<IUser, IUserRead>();
            CreateMap<IUserCreate, IUser>();
        }
    }
}
