namespace Tasko.Domains.Models.DTO.User;

public interface IUserAuthRead
{
    IUserRead User { get; set; }
    string Token { get; set; }
}

public class UserAuthRead : IUserAuthRead
{
    public IUserRead User { get; set; }
    public string Token { get; set; }
}