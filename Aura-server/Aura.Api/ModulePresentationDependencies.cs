using Aura.Api.Middleware;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Aura.Api;
public static class ModulePresentationDependencies
{
    public static IServiceCollection AddPresentationDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            options.OperationFilter<SecurityRequirementsOperationFilter>();

            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Aura",
                Description = "An ASP.NET Core Web API for SocialMedia website",
                Contact = new OpenApiContact
                {
                    Name = "Leen Odeh",
                    Url = new Uri("https://www.linkedin.com/in/leen-odeh3/")
                },
            });
        });



        services.AddHttpClient();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddTransient<GlobalExceptionHandlingMiddleware>();

        return services;
    }
}
