namespace Aura.Application.Abstracts.UserServices;
public interface IAuthenticatedUserService
{
    int GetAuthenticatedUserId();
    string GetAuthenticatedUsername();
}