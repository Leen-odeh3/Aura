
using Aura.Domain.DTOs.Image;

namespace Aura.Domain.DTOs.User;
public class UserResponseDto
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string About { get; set; } = string.Empty;
    public ImageResponseDto Image { get; set; }
}
