using Aura.Domain.DTOs.Image;
using Aura.Domain.DTOs.User;
namespace Aura.Domain.DTOs.Post;
public class PostResponseDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public bool IsPrivate { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime DateUpdated { get; set; }
    public ImageResponseDto Image { get; set; } 
    public UserResponseDto User { get; set; } 
    public int NrOfReposts { get; set; }
}