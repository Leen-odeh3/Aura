using Microsoft.AspNetCore.Http;

namespace Aura.Application.Abstracts.UserServices;
public interface IUserProfileImageService
{
    Task AddProfilePictureAsync(int userId, IFormFile image);
    Task ChangeProfilePictureAsync(int userId, IFormFile image);
    Task DeleteProfilePictureAsync(int userId);
}