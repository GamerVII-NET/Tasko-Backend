namespace Tasko.Domains.Models.DTO.User
{
    public interface IUserRoleCreate
    {
        Guid UserId { get; set; }
        Guid RoleId { get; set; }
    }

    public class UserRoleCreate : IUserRoleCreate
    {
        public Guid UserId  { get; set; }
        public Guid RoleId { get; set; }
    }
}
