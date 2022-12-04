namespace Tasko.Domains.Models.DTO.Interfaces
{
    public interface IUserRead
    {
        Guid GlobalGuid { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Patronymic { get; set; }
        string Photo { get; set; }
        string? About { get; set; }
        DateTime LastOnline { get; set; }
        bool IsDeleted { get; set; }
    }
}
