using Tasko.Domains.Models.Structural;

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