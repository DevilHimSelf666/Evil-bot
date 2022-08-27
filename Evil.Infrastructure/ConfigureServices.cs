using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Evil.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}