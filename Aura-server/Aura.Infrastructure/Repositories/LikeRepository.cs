using Aura.Domain.Contracts;
using Aura.Domain.DTOs.Like;
using Aura.Domain.Entities;
using Aura.Infrastructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Aura.Infrastructure.Repositories;
public class LikeRepository : ILikeRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public LikeRepository(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<LikeResponseDto>> GetLikesByPostIdAsync(int postId)
    {
        return await _dbContext.Likes
            .Where(l => l.PostId == postId)
            .Include(l => l.User)  
            .Select(l => _mapper.Map<LikeResponseDto>(l))
            .ToListAsync();
    }


    public async Task<LikeResponseDto> AddLikeAsync(int postId, int userId)
    {
        var existingLike = await _dbContext.Likes
            .FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId);

        if (existingLike != null)
        {
            throw new InvalidOperationException("User has already liked this post.");
        }

        var like = new Like
        {
            PostId = postId,
            UserId = userId
        };

        _dbContext.Likes.Add(like);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<LikeResponseDto>(like);
    }

    public async Task<LikeResponseDto> RemoveLikeAsync(int likeId)
    {
        var like = await _dbContext.Likes
            .Include(l => l.User)
            .FirstOrDefaultAsync(l => l.Id == likeId);

        if (like == null)
        {
            return null;
        }

        _dbContext.Likes.Remove(like);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<LikeResponseDto>(like);
    }
}