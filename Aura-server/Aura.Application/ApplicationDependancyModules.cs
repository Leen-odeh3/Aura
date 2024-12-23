using Aura.Application.Abstracts.FileServices;
using Aura.Application.Abstracts.UserServices;
using Aura.Application.Services.FileServices;
using Aura.Application.Services.UserServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Aura.Application;
public static class ApplicationDependancyModules
{
    public static IServiceCollection addApplicationDependancy(this IServiceCollection services)
    {
        //UserServices
        services.AddScoped<IUserAccountService, UserAccountService>();
        services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
        services.AddScoped<IUserProfileImageService, UserProfileImageService>();
        services.AddScoped<IUserRetrievalService, UserRetrievalService>();

        //FileServices

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddScoped<ICloudinaryService, CloudinaryService>();
        services.AddScoped<IFileService, FileService>();

        return services;
    }
}
