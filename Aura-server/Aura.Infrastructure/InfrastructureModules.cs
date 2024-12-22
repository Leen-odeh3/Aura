using Microsoft.Extensions.DependencyInjection;

namespace Aura.Infrastructure;
public static class InfrastructureModules
{
    public static IServiceCollection addDependancy(this IServiceCollection service)
    {
        return service;
    }
}
