namespace Tasko.Domains.DTOs.User;

public class UserReadDto
{
    public Guid GlobalGuid { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Patronymic { get; set; } = null!;
    public string Photo { get; set; } = null!;
    public string? About { get; set; }
    public DateTime LastOnline { get; set; }
    public bool IsDeleted { get; set; }
}
