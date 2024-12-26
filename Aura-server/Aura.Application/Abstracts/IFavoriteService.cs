
using Aura.Domain.DTOs.Favorite;

namespace Aura.Application.Abstracts;
public interface IFavoriteService
{
    Task<List<FavoriteResponseDto>> GetFavoritesByUserIdAsync(int userId);
    Task<List<FavoriteResponseDto>> GetFavoritesByPostIdAsync(int postId);
    Task<FavoriteResponseDto> AddFavoriteAsync(int postId, int userId);
    Task<FavoriteResponseDto> RemoveFavoriteAsync(int favoriteId);
}