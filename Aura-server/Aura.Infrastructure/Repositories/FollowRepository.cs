using Aura.Domain.Contracts;
using Aura.Domain.DTOs.Image;
using Aura.Domain.DTOs.User;
using Aura.Domain.Entities;
using Aura.Infrastructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace Aura.Infrastructure.Repositories;

public class FollowRepository : IFollowRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    public FollowRepository(AppDbContext dbContext,IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Follow> AddFollowAsync(int followerId, int followingId)
    {
        if (followerId == followingId)
        {
            throw new InvalidOperationException("You cannot follow yourself.");
        }

        var existingFollow = await _dbContext.Follows
            .FirstOrDefaultAsync(f => f.FollowerId == followerId && f.FollowingId == followingId);

        if (existingFollow != null)
        {
            throw new InvalidOperationException("You are already following this user.");
        }

        var follow = new Follow
        {
            FollowerId = followerId,
            FollowingId = followingId
        };

        _dbContext.Follows.Add(follow);
        await _dbContext.SaveChangesAsync();

        return follow;
    }

    public async Task<Follow> RemoveFollowAsync(int followerId, int followingId)
    {
        var follow = await _dbContext.Follows
            .FirstOrDefaultAsync(f => f.FollowerId == followerId && f.FollowingId == followingId);

        if (follow != null)
        {
            _dbContext.Follows.Remove(follow);
            await _dbContext.SaveChangesAsync();
        }

        return follow;
    }

    public async Task<List<UserResponseDto>> GetFollowersAsync(int userId)
    {
        var followers = await _dbContext.Follows
            .Where(f => f.FollowingId == userId)
            .Select(f => f.Follower)
            .ToListAsync();

        return _mapper.Map<List<UserResponseDto>>(followers);
    }

    public async Task<List<UserResponseDto>> GetFollowingAsync(int userId)
    {
        var following = await _dbContext.Follows
            .Where(f => f.FollowerId == userId)
            .Select(f => f.Following)
            .ToListAsync();

        return _mapper.Map<List<UserResponseDto>>(following);
    }
}