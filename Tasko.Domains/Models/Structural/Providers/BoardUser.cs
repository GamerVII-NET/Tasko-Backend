namespace Tasko.Domains.Models.Structural.Providers
{

    public interface IBoardUser
    {
        Guid Id { get; set; }
        Guid BoardId { get; set; }
        Guid UserId { get; set; }
        Guid InviterId { get; set; }
        bool IsDeleted { get; set; }
        bool IsBanned { get; set; }
        DateTime Joined { get; set; }
    }

    public class BoardUser : IBoardUser
    {
        public Guid Id { get; set; }
        public Guid BoardId { get; set; }
        public Guid UserId { get; set; }
        public Guid InviterId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsBanned { get; set; }
        public DateTime Joined { get; set; }
    }
}
