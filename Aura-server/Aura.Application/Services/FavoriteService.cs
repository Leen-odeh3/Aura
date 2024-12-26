using Aura.Application.Abstracts;
using Aura.Domain.Contracts;
using Aura.Domain.DTOs.Favorite;
using AutoMapper;
namespace Aura.Application.Services;
public class FavoriteService : IFavoriteService
{
    private readonly IFavoriteRepository _favoriteRepository;
    private readonly IMapper _mapper;
    public FavoriteService(IFavoriteRepository favoriteRepository,IMapper mapper)
    {
        _favoriteRepository = favoriteRepository;
        _mapper = mapper;
    }

    public async Task<List<FavoriteResponseDto>> GetFavoritesByUserIdAsync(int userId)
    {
        var favorites = await _favoriteRepository.GetFavoritesByUserIdAsync(userId); 
        return _mapper.Map<List<FavoriteResponseDto>>(favorites); 
    }

    public async Task<List<FavoriteResponseDto>> GetFavoritesByPostIdAsync(int postId)
    {
        return await _favoriteRepository.GetFavoritesByPostIdAsync(postId);
    }

    public async Task<FavoriteResponseDto> AddFavoriteAsync(int postId, int userId)
    {
        return await _favoriteRepository.AddFavoriteAsync(postId, userId);
    }

    public async Task<FavoriteResponseDto> RemoveFavoriteAsync(int favoriteId)
    {
        return await _favoriteRepository.RemoveFavoriteAsync(favoriteId);
    }
}