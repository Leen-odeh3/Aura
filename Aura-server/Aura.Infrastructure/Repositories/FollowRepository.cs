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
    private readonly AppDbContext _context;

    public FollowRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task FollowUserAsync(int followerId, int followedId)
    {
        var follow = new Follow
        {
            FollowerId = followerId,
            FollowingId = followedId,
        };

        await _context.Follows.AddAsync(follow);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsFollowingAsync(int followerId, int followedId)
    {
        return await _context.Follows
            .AnyAsync(f => f.FollowerId == followerId && f.FollowingId == followedId);
    }
}