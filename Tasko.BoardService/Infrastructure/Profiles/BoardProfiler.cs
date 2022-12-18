using AutoMapper;
using Tasko.Domains.Models.DTO.Board;
using Tasko.Domains.Models.DTO.User;
using Tasko.Domains.Models.Structural.Providers;

namespace Tasko.UserRoles.Infrasructure.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<IBoardCreate, Board>();
            CreateMap<IBoard, BoardRead>();
        }
    }
}