
using Tasko.Domains.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace Tasko.Configuration.Extensions;

public static class ApplicationExtensions
{
    public static void UseRouteHandlers(this WebApplication webApplication)
    {
        webApplication.Services.GetServices<IRouteHandler<WebApplication>>()
                               .ToList()
                               .ForEach(api => api.Initialzie(webApplication));
    }
}