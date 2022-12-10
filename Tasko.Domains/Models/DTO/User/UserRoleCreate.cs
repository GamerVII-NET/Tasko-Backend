namespace Tasko.Domains.Models.DTO.User
{
    public interface IUserRoleCreate
    {
        #region Properties

        Guid UserId { get; set; }
        Guid RoleId { get; set; }

        #endregion
    }

    public class UserRoleCreate : IUserRoleCreate
    {
        public Guid UserId  { get; set; }
        public Guid RoleId { get; set; }
    }
}
