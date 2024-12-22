using Aura.Domain.Entities;

namespace Aura.Application.Abstracts;
public interface IUsersService
{
    Task<User> GetUser(int loggedInUserId);
    Task UpdateUserProfilePicture(int loggedInUserId, string profilePictureUrl);
}
