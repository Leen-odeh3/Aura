using Aura.Application.Abstracts.UserServices;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Aura.Application.Services.UserServices;
public class AuthenticatedUserService : IAuthenticatedUserService
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public int GetAuthenticatedUserId()
    {
        var userId = 0;
        if (httpContextAccessor.HttpContext != null)
        {
            var NameIdentifier = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            userId = int.Parse(NameIdentifier);
        }
        return userId;
    }

    public string GetAuthenticatedUsername()
    {
        var username = string.Empty;
        if (httpContextAccessor.HttpContext != null)
        {
            username = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
        }
        return username;
    }
}