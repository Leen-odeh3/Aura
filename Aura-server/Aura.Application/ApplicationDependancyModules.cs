using Aura.Application.Abstracts;
using Aura.Application.Abstracts.FileServices;
using Aura.Application.Abstracts.UserServices;
using Aura.Application.Services;
using Aura.Application.Services.FileServices;
using Aura.Application.Services.UserServices;
using Aura.Domain.Contracts;
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
        services.AddScoped<IPostService,PostService>();
        services.AddScoped<ICommentService,CommentService>();
        services.AddScoped<ILikeService,LikeService>();
        services.AddScoped<IFavoriteService, FavoriteService>();
        services.AddScoped<IFollowService, FollowService>();
        services.AddScoped<IStoryService,StoryService>();

        //FileServices

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddScoped<ICloudinaryService, CloudinaryService>();
        services.AddScoped<IFileService, FileService>();

        return services;
    }
}
