using Tasko.Domains.Models.Structural.Interfaces;

namespace Tasko.Server.Repositories.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(string key, string issuer, string audience, IUser user);
    }
}
