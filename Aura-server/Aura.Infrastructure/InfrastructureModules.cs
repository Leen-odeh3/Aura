using Aura.Domain.Contracts;
using Aura.Infrastructure.Data;
using Aura.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Aura.Infrastructure;
public static class InfrastructureModules
{
    public static IServiceCollection addDependancy(this IServiceCollection service,IConfiguration configuration)
    {

        //Repositories
        service.AddScoped<IUserRepository, UserRepository>();
        service.AddScoped<IUnitOfWork, UnitOfWork>();
        service.AddScoped<IPostRepository, PostRepository>();
        service.AddScoped<ICommentRepository, CommentRepository>();
        service.AddScoped<IFavoriteRepository, FavoriteRepository>();
        service.AddScoped<ILikeRepository, LikeRepository>();
        service.AddScoped<IFollowRepository, FollowRepository>();


        var connectionString = configuration.GetConnectionString("Default");
        service.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));


        service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
  {
      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
              .GetBytes(configuration.GetSection("AppSettings:TokenKey").Value)),
          ValidateIssuer = false,
          ValidateAudience = false
      };
  });

        return service;
    }
}
