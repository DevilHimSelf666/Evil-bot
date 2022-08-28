using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Evil.Domain
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            return services;
        }
    }
}