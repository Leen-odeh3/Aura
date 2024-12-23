
namespace Aura.Domain.DTOs.Favorite;
public class FavoriteResponseDto
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }
}