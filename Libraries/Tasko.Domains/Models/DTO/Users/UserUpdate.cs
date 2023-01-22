namespace Tasko.Domains.Models.DTO.User;

public interface IUserUpdate
{
    Guid Id { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string Patronymic { get; set; }
    string Email { get; set; }
    string Password { get; set; }
    string About { get; set; }
}

public class UserUpdate : IUserUpdate
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string About { get; set; }
}