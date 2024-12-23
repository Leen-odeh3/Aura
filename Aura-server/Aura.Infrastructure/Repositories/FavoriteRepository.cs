using Aura.Domain.Contracts;
using Aura.Domain.DTOs.Favorite;
using Aura.Domain.Entities;
using Aura.Infrastructure.Data;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Aura.Infrastructure.Repositories;
public class FavoriteRepository : IFavoriteRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public FavoriteRepository(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<FavoriteResponseDto>> GetFavoritesByPostIdAsync(int postId)
    {
        return await _dbContext.Set<Favorite>()
            .Where(f => f.PostId == postId)
            .Include(f => f.User)    
            .Include(f => f.Post)  
            .ProjectTo<FavoriteResponseDto>(_mapper.ConfigurationProvider)  
            .ToListAsync();
    }


    public async Task<FavoriteResponseDto> AddFavoriteAsync(int postId, int userId)
    {
        var favorite = new Favorite
        {
            PostId = postId,
            UserId = userId,
            DateCreated = DateTime.Now
        };

        _dbContext.Set<Favorite>().Add(favorite);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<FavoriteResponseDto>(favorite);
    }

    public async Task<FavoriteResponseDto> RemoveFavoriteAsync(int favoriteId)
    {
        var favorite = await _dbContext.Set<Favorite>()
            .FirstOrDefaultAsync(f => f.Id == favoriteId);

        if (favorite != null)
        {
            _dbContext.Set<Favorite>().Remove(favorite);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<FavoriteResponseDto>(favorite);
        }

        return null;
    }
}