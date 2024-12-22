using Microsoft.AspNetCore.Http;
namespace Aura.Domain.DTOs.User;
public class UpdateProfilePicture
{
    public IFormFile ProfilePictureImage { get; set; }
}
