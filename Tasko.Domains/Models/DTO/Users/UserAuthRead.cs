namespace Tasko.Domains.Models.DTO.User;

public interface IUserAuthRead
{
    UserRead User { get; set; }
    string Token { get; set; }
}

public class UserAuthRead : IUserAuthRead
{
    public UserRead User { get; set; }
    public string Token { get; set; }
}