
using Aura.Domain.DTOs.Post;

namespace Aura.Domain.DTOs.Favorite;
public class FavoriteResponseDto
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public int PostId { get; set; }
    public string Username { get; set; }
    public PostResponseDto Post { get; set; } 
}