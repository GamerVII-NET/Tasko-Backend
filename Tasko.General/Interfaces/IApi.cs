using Microsoft.AspNetCore.Builder;

namespace Tasko.General.Interfaces
{
    public interface IApi
    {
        public void Register(WebApplication webApplication);
    }
}
