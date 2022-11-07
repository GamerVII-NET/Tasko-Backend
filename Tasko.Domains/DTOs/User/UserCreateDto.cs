namespace Tasko.Domains.DTOs.User;

public class UserCreateDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Patronymic { get; set; } = null!;
    public string Password { get; set; } = null!;
}
