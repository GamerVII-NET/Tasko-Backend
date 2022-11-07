using AutoMapper;
using Tasko.Domains.DTOs.User;
using Tasko.Server.Context.Data.Models;

namespace Tasko.Server.Profiles.UserProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>();
        }
    }
}
