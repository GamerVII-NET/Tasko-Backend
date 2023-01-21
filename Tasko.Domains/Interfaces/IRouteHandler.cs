using Microsoft.AspNetCore.Builder;

namespace Tasko.Domains.Interfaces
{
    public interface IRouteHandler
    {
        public void Initialzie(WebApplication webApplication);
    }
}
