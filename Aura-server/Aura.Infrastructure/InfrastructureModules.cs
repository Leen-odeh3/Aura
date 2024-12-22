namespace Aura.Infrastructure;
public static class InfrastructureModules
{
    public static IserviceCollection addDependancy(this IServiceCollection service)
    {
        return service;
    }
}
