namespace Tasko.Domains.Models.Structural.Providers
{

    public interface IUserRole
    {
        Guid RoleId { get; set; }
        Guid UserId { get; set; }
    }

    public class UserRole : IUserRole
    {
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
    }
}
