namespace Tasko.Domains.Models.DTO.User;

public interface IUserRead
{
    Guid Id { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string Patronymic { get; set; }
    string Photo { get; set; }
    string? About { get; set; }
    DateTime LastOnline { get; set; }
}
public class UserRead : IUserRead
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public string Photo { get; set; }
    public string? About { get; set; }
    public DateTime LastOnline { get; set; }
}