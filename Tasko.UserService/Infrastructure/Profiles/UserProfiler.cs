using Tasko.Domains.Models.DTO.User;

namespace Tasko.Service.Infrastructure.Profiles;

internal class UserProfiler : Profile
{
	public UserProfiler()
	{
		CreateMap<IUserCreate, User>();
	}
}